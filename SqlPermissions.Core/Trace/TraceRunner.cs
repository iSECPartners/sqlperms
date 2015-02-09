// Copyright (c) 2011-2015 iSEC Partners
// Author: Peter Oehlert
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.


using System;
using System.Diagnostics.Contracts;
using System.IO;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Trace;
using SqlPermissionsEngine.Trace.Event;

namespace SqlPermissionsEngine.Trace
{
    /// <summary>Class to retrieve the events from a trace, whether live or being replayed.</summary>
    public abstract class TraceRunner : IDisposable, IObservable<IEventBase>
    {
        /// <summary>The trace reader we are going to read from.</summary>
        private readonly TraceReader _traceReader;

        /// <summary>Gets the trace reader.</summary>
        protected TraceReader TraceReader
        { get { return _traceReader; } }

        /// <summary>Indicate that this instance has been disposed.</summary>
        private volatile Boolean _isDisposed = false;

        #region Construction

        /// <summary>Initializes a new instance of the <see cref="TraceRunner"/> class.</summary>
        /// <param name="traceReader">The trace reader.</param>
        protected TraceRunner(TraceReader traceReader)
        {
            Contract.Requires(null != traceReader);

            _traceReader = traceReader;
            _runnerThread = new Thread(ThreadMain);
        }

        /// <summary>Creates from file.</summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>A trace runner from the file.</returns>
        public static TraceRunner CreateFromFile(string fileName)
        {
            return new TraceFileRunner(fileName);
        }

        /// <summary>Creates from server.</summary>
        /// <param name="connectionInfo">The connection info.</param>
        /// <param name="tdfFile">The TDF file.</param>
        /// <returns>A trace runner for a server.</returns>
        public static TraceRunner CreateFromServer(SqlConnectionInfo connectionInfo, String tdfFile)
        {
            return new TraceServerRunner(connectionInfo, tdfFile);
        }

        /// <summary>Starts this instance, depending on what kind of runner we are.</summary>
        protected abstract void Start();

        #endregion

        /// <summary>Checks whether this instance is disposed.</summary>
        private void CheckIsDisposed()
        {
            if (_isDisposed)
                EngineException.Throw();
        }

        /// <summary>subject marshaled to the thread pool</summary>
        private readonly Subject<IEventBase> _subject = new Subject<IEventBase>();
        /// <summary>Indicate whether this instance has been started yet</summary>
        private volatile Boolean _isRunning = false;

        /// <summary>Subscribes the specified observer.</summary>
        /// <param name="observer">The observer.</param>
        /// <returns></returns>
        public IDisposable Subscribe(IObserver<IEventBase> observer)
        {
            CheckIsDisposed();

            var retVal = _subject.ObserveOn(Scheduler.TaskPool).Subscribe(observer);

            return retVal;
        }

        /// <summary>Runs this instance.</summary>
        public void Run()
        {
            // start the thread if necessary
            if (!_isRunning)
                lock (_runnerThread)
                    if (!_isRunning)
                    {
                        _runnerThread.Start();
                        _isRunning = true;
                    }
        }

        /// <summary>A thread to run our read on so that it can be blocked and not effect the rest of the system.</summary>
        private readonly Thread _runnerThread;

        /// <summary>The main routine for our thread that will read from the trace.</summary>
        private void ThreadMain()
        {
            try
            {
                // start the reader via whatever process is appropriate
                Start();

                // now create an event factory from the reader
                var eventFactory = new EventFactory(_traceReader);

                // keep processing on this thread until we've either been set to the not running state
                // and we still have values to read
                Boolean wasReadSuccessful = true;
                while (_isRunning 
                    // this is potentially a blocking call if this is a TraceServer and there are no events to read
                    && (wasReadSuccessful = _traceReader.Read()))
                {
                    var evt = eventFactory.Build(_traceReader);
                    _subject.OnNext(evt);
                }
            }
            finally
            {
                // close the reader whatever its state
                _traceReader.Dispose();
                // signal that all of our sequences are done
                _subject.OnCompleted();
                // cleanup ourselves, we are one time use only
                Dispose();
            }
        }

        #region Dispose

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public void Dispose()
        {
            Dispose(true);
        }

        /// <summary>Releases unmanaged and - optionally - managed resources</summary>
        /// <param name="isManaged"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected void Dispose(bool isManaged)
        {
            // if we're already disposed, ignore
            if (_isDisposed)
                return;

            if (isManaged)
            {
                // set the thread to off and wait to see if it ends
                _isRunning = false;

                // Thread.Abort() doesn't work when the thread is in NativeCode. Could use TerminateThread, but we can just wait.
                //lock(_runnerThread)
                //    // wait for 2 seconds for it to finish
                //    if (!_runnerThread.Join(TimeSpan.FromSeconds(5d)))
                //    {
                //        // we're not done, abort the thread wherever it is
                //        _runnerThread.Abort();
                //    }

                // just join the thread if it's not us
                if (Thread.CurrentThread.ManagedThreadId != _runnerThread.ManagedThreadId)
                    _runnerThread.Join();
            }

            _isDisposed = true;
        }

        #endregion

        #region Specific initialize derived instances

        /// <summary>A TraceRunner that uses a TraceServer for it's source data.</summary>
        private sealed class TraceServerRunner : TraceRunner
        {
            /// <summary>The information to connect to sql.</summary>
            private readonly SqlConnectionInfo _sqlConnectionInfo;

            /// <summary>The path to the tdf file.</summary>
            private readonly String _tdfFile;

            /// <summary>Initializes a new instance of the <see cref="TraceServerRunner"/> class.</summary>
            /// <param name="sqlConnectionInfo">The SQL connection info.</param>
            /// <param name="tdfFile">The TDF file.</param>
            internal TraceServerRunner(SqlConnectionInfo sqlConnectionInfo, String tdfFile)
                : base(new TraceServer())
            {
                Contract.Requires(null != sqlConnectionInfo);
                Contract.Requires(!String.IsNullOrEmpty(tdfFile));

                _sqlConnectionInfo = sqlConnectionInfo;
                _tdfFile = tdfFile;
            }

            /// <summary>Starts this instance, depending on what kind of runner we are.</summary>
            protected override void Start()
            {
                Contract.Assert(TraceReader is TraceServer, "How is the reader not a trace server?");
                Contract.Assert(File.Exists(_tdfFile), "The tdf file must exist.");

                var traceServer = TraceReader as TraceServer;
                traceServer.InitializeAsReader(_sqlConnectionInfo, _tdfFile);
            }
        }

        /// <summary>A TraceRunner that uses a saved trace file for its source data.</summary>
        private sealed class TraceFileRunner : TraceRunner
        {
            /// <summary>The filename of the trace file.</summary>
            private readonly String _fileName;

            /// <summary>Initializes a new instance of the <see cref="TraceFileRunner"/> class.</summary>
            /// <param name="fileName">Name of the file.</param>
            internal TraceFileRunner(String fileName)
                : base(new TraceFile())
            {
                Contract.Requires(!String.IsNullOrEmpty(fileName), "The fileName must be present.");
                Contract.Requires(File.Exists(fileName), "The file must be present.");

                _fileName = fileName;
            }

            /// <summary>Starts this instance, depending on what kind of runner we are.</summary>
            protected override void Start()
            {
                Contract.Assert(TraceReader is TraceFile);

                var traceFile = TraceReader as TraceFile;
                traceFile.InitializeAsReader(_fileName);
            }
        }

        #endregion
    }
}
