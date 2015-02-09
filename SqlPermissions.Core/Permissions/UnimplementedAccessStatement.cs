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


using SqlPermissions.Core.Trace.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlPermissions.Core.Permissions
{
    /// <summary>Class represents an unimplemented access statement. This is used
    /// so that untranslated classes have a value.
    /// </summary>
    public class UnimplementedAccessStatement : IAccessStatement
    {
        private readonly string eventType;
        private readonly int databaseId;
        private readonly string statement;

        public UnimplementedAccessStatement(IEventBase e)
        {
            if (e == null)
            {
                throw new ArgumentNullException("e");
            }

            this.eventType = e.GetType().Name;
            this.databaseId = e.DatabaseID ?? 0;
            this.statement = (e.TextData != null) ? e.TextData.Trim() : string.Empty;
        }

        /// <summary>Required by interface, returns AccessType.Grant.</summary>
        public AccessType AccessType
        {
            get { return AccessType.Grant; }
        }

        /// <summary>Required by interface, returns null.</summary>
        public phPrincipal AsPrincipal
        {
            get { return null; }
        }

        /// <summary>Required by interface, returns null.</summary>
        public string Database
        {
            get { return null; }
        }

        /// <summary>Database ID specified in event or 0 if no ID is specified.</summary>
        public int DatabaseId
        {
            get { return this.databaseId; }
        }

        /// <summary>Name of event class (through Type reflection Name property).</summary>
        public string EventType
        {
            get { return this.eventType; }
        }

        /// <summary>Required by interface, returns false.</summary>
        public bool GrantOption
        {
            get { return false; }
        }

        /// <summary>Required by interface, returns empty array.</summary>
        public IEnumerable<SqlPermission> Permissions
        {
            get
            {
                return new SqlPermission[0];
            }
        }

        /// <summary>Required by interface, returns empty array.</summary>
        public IEnumerable<phPrincipal> Principals
        {
            get
            {
                return new phPrincipal[0];
            }
        }

        /// <summary>Required by interface, returns null.</summary>
        public SecurableObject SecurableObject
        {
            get { return null; }
        }

        /// <summary>Returns the SQL statement specified in event.</summary>
        public IEnumerable<string> Statements
        {
            get
            {
                return new string[] { this.statement };
            }
        }

        /// <summary>Returns implementation information for missing event.</summary>
        /// <returns></returns>
        public string BuildSqlCommand()
        {
            return String.Format("/* Event Unimplemented: Name=[{0}]\n\tDatabaseID=[{1}]\n\tStatement=[{2}] */", this.eventType, this.databaseId, this.statement);
        }

    }
}
