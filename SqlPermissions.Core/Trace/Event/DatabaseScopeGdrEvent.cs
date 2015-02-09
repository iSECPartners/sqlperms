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
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlPermissions.Core.Trace.Event
{
    public partial class DatabaseScopeGdrEvent
    {
        protected override IAccessStatement BuildPermissionInternal()
        {
            /*
             * Type of event subclass.
             * 1=Grant
             * 2=Revoke
             * 3=Deny
             */
            if (this.EventSubClass != 1)
            {
                Debug.Assert(this.EventSubClass == 1, "DatabaseScopeGdrEvent cannot handle non-grant events");
                return base.BuildPermissionInternal();
            }

            GenericAccessStatement gas = (GenericAccessStatement)GenericAccessStatement.GenerateBaseGrant(
                this.DatabaseName,
                this.ObjectType ?? 0,
                this.Permissions ?? 0L,
                null,
                null,
                new phPrincipal(this.LoginName),
                this.TextData);
            gas.GrantOption = true;

            return gas;
        }
    }
}
