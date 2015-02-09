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
    public partial class BackupRestoreEvent
    {
        protected override IAccessStatement BuildPermissionInternal()
        {
            long permission = 0L;

            /*
             * Type of event subclass.
             * 1=Backup
             * 2=Restore
             * 3=BackupLog
             */
            switch (this.EventSubClass)
            {
                case 1:
                    // BACKUP DATABASE requires BACKUP DATABASE permission
                    permission = 64;
                    break;

                case 2:
                    // RESTORE DATABASE requires CREATE DATABASE permission
                    permission = 1;
                    break;

                case 3:
                    // BACKUP LOG requires BACKUP LOG permission
                    permission = 128;
                    break;

                default:
                    Type t = this.GetType();
                    Debug.Assert(false, "unhandled EventSubClass (" + this.EventSubClass + ") for " + t.Name);
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
