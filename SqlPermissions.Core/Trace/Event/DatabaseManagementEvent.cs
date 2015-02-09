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
    public partial class DatabaseManagementEvent
    {
        protected override IAccessStatement BuildPermissionInternal()
        {
            long permission = 0L;

            /*
             * Type of event subclass.
             * 1=Create
             * 2=Alter
             * 3=Drop
             * 4=Dump
             * 11=Load
             */
            switch (this.EventSubClass)
            {
                case 1:
                    // Create create requires CREATE DATABASE permission on master
                    permission = 1;
                    break;

                case 2:
                    // Alter database requires ALTER permission
                    permission = 1152921504606846976;
                    break;

                case 3:
                    // Drop database requires the CONTROL permission
                    permission = 4611686018427387904;
                    break;

                case 4:
                    // "The SQL Server Database Engine interprets DUMP DATABASE or DUMP TRANSACTION the same as BACKUP DATABASE or BACKUP LOG, respectively."
                    // http://technet.microsoft.com/en-us/library/ms187315(v=sql.90).aspx
                    // Dump database requires the X permission
                    Debug.Assert(false, "Dump not yet handled EventSubClass (" + this.EventSubClass + ") for " + this.GetType().Name);
                    permission = 0L; // $TODO
                    break;

                case 11:
                    // The LOAD statement is included for backward compatibility. This feature will be removed in a future version of Microsoft SQL Server. 
                    // Avoid using this feature in new development work, and plan to modify applications that currently use this feature. Instead, use RESTORE 
                    // (Transact-SQL) and RESTORE HEADERONLY (Transact-SQL).
                    // http://technet.microsoft.com/en-US/library/ms176032(v=sql.90).aspx                    
                    Debug.Assert(false, "Load not yet handled EventSubClass (" + this.EventSubClass + ") for " + this.GetType().Name);
                    permission = 0L; // $TODO
                    break;

                default:
                    Type t = this.GetType();
                    Debug.Assert(false, "unhandled EventSubClass (" + this.EventSubClass + ") for " + this.GetType().Name);
                    permission = 0L;
                    break;
            }

            return
                GenericAccessStatement.GenerateBaseGrant(
                    this.DatabaseName,
                    this.ObjectType ?? 0,
                    permission,
                    null,
                    null,
                    new phPrincipal(this.LoginName),
                    this.TextData);
        }

    }
}
