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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlPermissions.Core.Utility;

namespace SqlPermissions.Core.Permissions
{
    public class SecurableObject
    {
        private static readonly string[] SecurableObjectIdentifier = new string[]
        {
            "APPLICATION ROLE",
            "ASSEMBLY",
            "ASYMMETRIC KEY",
            "AVAILABILITY GROUP",
            "CERTIFICATE",
            "CONTRACT",
            null, // Database
            "ENDPOINT",
            "FULLTEXT CATALOG",
            "FULLTEXT STOPLIST",
            "LOGIN",
            "MESSAGE TYPE",
            "OBJECT",
            "REMOTE SERVICE BINDING",
            "ROLE",
            "ROUTE",
            "SCHEMA",
            "SEARCH PROPERTY LIST",
            null, // Server   
            "SERVER ROLE",
            "SERVICE",
            "SYMMETRIC KEY",
            "TYPE",
            "USER",
            "XML SCHEMA COLLECTION"
        };

        internal SecurableObject()
        {
            this.Type = SecurableObjectType.Database;
        }

        public SecurableObjectType Type { get; internal set; }

        public string Server { get; internal set; }

        public string Database { get; internal set; }

        public string ObjectOwner { get; internal set; }

        public string ObjectName { get; internal set; }

        /// <summary>
        /// </summary>
        /// <remarks>These comments are based on information found here: http://technet.microsoft.com/en-us/library/ms189479.aspx
        /// 
        /// Object identifiers will always contain an object name (the target of the command), and may contain a parent database or schema
        /// identifier. For permission extension categories, the object is prefixed by a securable object type (e.g. ASSEMBLY or ASYMMETRIC KEY) 
        /// 
        /// </remarks>
        /// <returns></returns>
        public override string ToString()
        {
            var sb = new StringBuilder();

            if (null != SecurableObject.SecurableObjectIdentifier[(int)this.Type])
            {
                try
                {
                    sb.Append(SecurableObject.SecurableObjectIdentifier[(int)this.Type] + " :: ");
                }
                catch (IndexOutOfRangeException)
                {
                    Debug.Assert(false, "SecurableObjectType has no string value " + (int)this.Type);
                }

                if (!String.IsNullOrWhiteSpace(this.ObjectOwner))
                {
                    sb.Append("[" + this.ObjectOwner + "].");
                }

                sb.Append("[" + this.ObjectName + "]");
            }
            else
            {
                switch (Type)
                {
                    case SecurableObjectType.Server:
                        if (!String.IsNullOrWhiteSpace(Server))
                            sb.Append('[').Append(Server).Append(']');
                        break;
                    case SecurableObjectType.Database:
                        sb.Append('[').Append(Database).Append(']');
                        break;
                    default:
                        ExceptionEx.Throw<Exception>("The type ({0}) was not expectedted.", Type);
                        break; // dead code
                }
            }

            return sb.ToString();
        }
    }
}
