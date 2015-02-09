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
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Trace;
using SqlPermissions.Core.Trace.Event;

namespace SqlPermissions.Core.Trace
{
    public class TraceSource
    {
        private readonly String filename;
        private readonly SqlConnectionInfo connectionInfo;

        public TraceSource(String filename)
        {
            if (String.IsNullOrWhiteSpace(filename))
            {
                throw new ArgumentNullException("filename");
            }
            
            this.filename = filename;
        }

        public TraceSource(SqlConnectionInfo connectionInfo, String filename)
        {
            if (connectionInfo == null)
            {
                throw new ArgumentNullException("connectionInfo");
            }

            this.connectionInfo = connectionInfo;
            this.filename = filename;
        }

        public IObservable<IEventBase> GetEvents()
        {
            if (null != connectionInfo)
            {
                return GetEvents(() =>
                    {
                        var trace = new TraceServer();
                        trace.InitializeAsReader(this.connectionInfo, this.filename);
                        return trace;
                    });
            }
            else
            {
                return GetEvents(() =>
                {
                    var trace = new TraceFile();
                    trace.InitializeAsReader(filename);
                    return trace;
                });
            }
        }

        private IObservable<IEventBase> GetEvents(Func<TraceReader> getTraceReader, Action onUnsubscribe = null)
        {
            return Observable.Create<IEventBase>((o, c) =>
                {
                    var trace = getTraceReader();

                    var eventFactory = new EventFactory(trace);

                    while (!c.IsCancellationRequested
                           && trace.Read())
                    {
                        var evt = eventFactory.Build(trace);
                        o.OnNext(evt);
                    }

                    o.OnCompleted();

                    // call close on unsubscribe
                    return Task.FromResult<Action>(() =>
                    {
                        trace.Close();
                        if (null != onUnsubscribe)
                            onUnsubscribe();
                    });
                });
        }
    }
}
