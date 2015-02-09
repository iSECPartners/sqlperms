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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlPermissions.Core.Permissions
{
    /// <summary>Represent an translated access statement from an original sql query/event. This access statement
    /// is one instance of a permission necessary to allow a given query to succcessfully complete.
    /// </summary>
    /// <remarks>The class ends up modeling the SQL GRANT/REVOKE statements and will ultimately generate one.
    /// The logic used to translate an event into a GRANT will determine which parts of this to use.
    /// </remarks>
    public interface IAccessStatement
    {
        /// <summary>GRANT or REVOKE</summary>
        AccessType AccessType { get; }

        /// <summary>A principal to modify the permission statement.</summary>
        phPrincipal AsPrincipal { get; }

        /// <summary>The database a statement applies to, if present.</summary>
        string Database { get; }

        /// <summary>
        /// </summary>
        bool GrantOption { get; }

        /// <summary>
        /// </summary>
        IEnumerable<SqlPermission> Permissions { get; }

        /// <summary>
        /// </summary>
        IEnumerable<phPrincipal> Principals { get; }

        /// <summary>
        /// </summary>
        SecurableObject SecurableObject { get; }

        /// <summary>Keep track of the statements that generated this access statement. This way, an auditor can go back
        /// to determine the validity of the GRANT or can track what operation required a generated permission.
        /// </summary>
        IEnumerable<string> Statements { get; }

        string BuildSqlCommand();
    }
}
