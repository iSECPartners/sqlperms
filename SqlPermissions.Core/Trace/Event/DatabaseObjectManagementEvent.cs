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
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlPermissions.Core.Permissions;

namespace SqlPermissions.Core.Trace.Event
{
    public partial class DatabaseObjectManagementEvent
    {


        protected override IAccessStatement BuildPermissionInternal()
        {
            Debug.Assert(EventSubClass != null, "The subclass shouldn't be null.");
            return base.BuildPermissionInternal();

            //var subClass = this.EventSubClass;


            //SecurableObjectType type;
            //String operationName, objectName;
            //// http://technet.microsoft.com/en-us/library/ms190459.aspx
            //switch (subClass)
            //{
            //    case 1: // Create
            //        type = SecurableObjectType.Database;
            //        operationName = "CREATE";
            //        objectName = ((SqlObjectLongType)this.ObjectType).ToSqlString();
            //        break;
            //    case 2: // Alter
            //        operationName = "ALTER";
            //        break;
            //    case 3: // Drop
            //        operationName = "DROP";
            //        break;
            //    case 4: // Dump
            //        operationName = "DUMP";
            //        break;
            //    case 10: // Open
            //        operationName = "OPEN";
            //        break;
            //    case 11: // Load
            //        operationName = "LOAD";
            //        break;
            //    case 12: // Access
            //        operationName = "ACCESS";
            //        break;
            //    default:
            //        // TODO: should make this dump out the event to be manually processed
            //        throw new NotImplementedException("Missing switch case.");
            //}

            
            //var permissionName = String.Format(CultureInfo.InvariantCulture, "{0} {1}", operationName, objectName);

            //return BuildPermissionInternal(
            //    loginName: this.LoginName,
            //    objectName: this.DatabaseName,
            //    parentName: this.ServerName,
            //    permission: new SqlPermission(SecurableObjectType.Database, permissionName), 
            //    grantOption: false,
            //    statement: this.TextData
            //    );
        }
    }
}
