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


using SqlPermissions.Core.Permissions;
using SqlPermissions.Core.Trace;
using System;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Threading;

namespace SqlPermissions
{
    public class Eventloop : IDisposable
    {
        private IDisposable _messagePump;

        public Eventloop()
        { }

        public void Build(TraceSource tracesource)
        {

            //this._messagePump = (from evt in tracesource.GetEvents().ObserveOn(TaskPoolScheduler.Default)
            //                     // transition to task pool immediately to not block the trace
            //                     where null != evt
            //                     let permissions = evt.BuildPermissions()
            //                     from permission in permissions
            //                     select permission)
            //    .SubscribeOn(TaskPoolScheduler.Default);
        }

        public void Stop()
        {
            if (this._messagePump != null)
            {
                this._messagePump.Dispose();
                this._messagePump = null;
            }
        }

        void IDisposable.Dispose()
        {
            this.Dispose(true);
        }

        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.Stop();
            }
        }
    }
}
