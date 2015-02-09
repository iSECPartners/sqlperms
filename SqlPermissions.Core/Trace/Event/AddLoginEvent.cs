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
    public partial class AddLoginEvent
    {
        protected override IAccessStatement BuildPermissionInternal()
        {
            return base.BuildPermissionInternal();

            /*
            long permission = 0L;
            
            *
             * Type of event subclass.
             * 1=Add
             * 2=Drop
             *
            switch (this.EventSubClass)
            {
                case 1:
                    // In SQL Server and SQL Server PDW, requires ALTER ANY LOGIN permission on the server or membership 
                    // in the securityadmin fixed server role. In SQL Database, only the server-level principal login (created 
                    // by the provisioning process) or members of the loginmanager database role in the master database can 
                    // create new logins. If the CREDENTIAL option is used, also requires ALTER ANY CREDENTIAL permission on 
                    // the server.

                    // $TODO: what happens if I try and add a login to SERVER instead of DATABASE?
                    //,{new Tuple<int, long>(21075, 128), new SqlPermission("SERVER", "ALTER ANY LOGIN") } // SERVER

                    // Add login requires ALTER ANY LOGIN permission
                    permission = 64;
                    break;

                case 2:
                    // RESTORE DATABASE requires CREATE DATABASE permission
                    permission = 1;
                    break;

                default:
                    Type t = this.GetType();
                    Debug.Assert(false, "unhandled EventSubClass (" + this.EventSubClass + ") for " + t.Name);
                    permission = 0L;
                    break;
            }

            return GenericAccessStatement.GenerateBaseGrant(
                this.DatabaseName,
                this.ObjectType ?? 0,
                this.Permissions ?? 0L,
                this.ParentName,
                this.OwnerName,
                new phPrincipal(this.LoginName),
                this.TextData);
             */
        }
    }
}
