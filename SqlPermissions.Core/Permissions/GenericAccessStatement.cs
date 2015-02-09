// Copyright (c) 2011-2015 iSEC Partners
// Author: Peter Oehlert, Jason Bubolz
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
    /// <summary>
    /// </summary>
    internal class GenericAccessStatement : IAccessStatement
    {
        private readonly List<SqlPermission> permissions = new List<SqlPermission>();
        private readonly List<phPrincipal> principals = new List<phPrincipal>();
        private readonly List<string> statements = new List<string>();

        internal GenericAccessStatement()
        { 
            this.AccessType = AccessType.Grant;
        }

        public AccessType AccessType
        {
            get;
            internal set;
        }

        public phPrincipal AsPrincipal
        {
            get;
            internal set;
        }

        public string Database
        {
            get;
            internal set;
        }

        public bool GrantOption
        {
            get;
            internal set;
        }

        internal bool HasPermissions 
        {
            get { return this.permissions.Count > 0; }
        }

        internal bool HasPrincipals
        {
            get { return this.principals.Count > 0; }
        }

        internal bool HasStatements
        {
            get { return this.statements.Count > 0; }
        }

        public IEnumerable<SqlPermission> Permissions
        {
            get
            {
                return this.permissions.ToArray();
            }
        }

        public IEnumerable<phPrincipal> Principals
        {
            get
            {
                return this.principals.ToArray();
            }
        }

        public SecurableObject SecurableObject
        {
            get;
            internal set;
        }

        public IEnumerable<string> Statements
        {
            get
            {
                return this.statements.ToArray();
            }
        }

        internal void AddPermission(SqlPermission permission)
        {
            if (!this.permissions.Contains(permission))
            {
                this.permissions.Add(permission);
            }
        }

        internal void AddPermission(IEnumerable<SqlPermission> permissions)
        {
            this.permissions.AddRange(permissions);
        }

        internal void AddPrincipal(phPrincipal principal)
        {
            if (!this.principals.Contains(principal))
            {
                this.principals.Add(principal);
            }
        }

        internal void AddStatement(string statement)
        {
            this.statements.Add(statement); // duplicates OK?
        }

        public string BuildSqlCommand()
        {
            return this.GenerateSqlCommand();
        }

        public static IAccessStatement GenerateBaseGrant(
            string database,
            SqlPermission permission,
            string parentName,
            string objectName,
            phPrincipal principal,
            string statement)
        {
            var gas = new GenericAccessStatement
                {
                    // GRANT
                    AccessType = AccessType.Grant,
                    // Database name
                    Database = database,
                    // On a specific object
                    SecurableObject = String.IsNullOrWhiteSpace(objectName) ? null : new SecurableObject
                    {
                        Type = permission.PermissionClass,
                        Database = database,
                        ObjectOwner = parentName,
                        ObjectName = objectName
                    }
                };

            // A set of permissions
            gas.AddPermission(permission);

            // To a set of users
            gas.AddPrincipal(principal);

            // Additional meta-data
            gas.AddStatement(statement);

            return gas;
        }

        public static IAccessStatement GenerateBaseGrant(
            string database,
            int objectType,
            long permissionMask,
            string parentName,
            string objectName,
            phPrincipal principal,
            string statement)
        {
            return GenerateBaseGrant(database, SqlPermission.GetPermission(objectType, permissionMask), parentName, objectName,
                                            principal, statement);

        }

        /// <summary>Generate the correct access control SQL statement.</summary>
        /// <remarks>These comments are based on information found here: http://technet.microsoft.com/en-us/library/ms189479.aspx
        /// 
        /// All GRANT commands are based on the form:
        /// 
        /// GRANT {permission list} ON {securable object declaration} TO {principals list} [ WITH GRANT OPTION ] [ AS {principal} ]
        /// 
        /// Three classes have format changes:
        /// 
        /// * System Object Permission does not allowed [ WITH GRANT OPTION ] or [ AS {principal} ]
        /// * Server Permission does not use the ON {securable object declaration} section and require the current database to be master
        /// * Database Permission does not use the ON {securable object declaration} section and require the current database to be the target database
        /// 
        /// GenerateAccessStatement creates a GRANT format which will conform to any item in the above documentation (REVOKE is still 
        /// TBD) provided the public properties are correctly configured. Users and/or child classes must enforce the correct business 
        /// logic to create valid output.
        /// </remarks>
        /// <returns></returns>
        private string GenerateSqlCommand()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(this.AccessType.ToString().ToUpperInvariant());

            // list of permissions
            sb.Append(" ");
            sb.Append(String.Join(",", Array.ConvertAll<SqlPermission, string>(this.permissions.ToArray(), p => p.Permission)));

            // if securable, the ON clause
            if (this.SecurableObject != null)
            {
                sb.Append(" ON ");
                sb.Append(this.SecurableObject.ToString());
            }

            // if principals, the TO close
            if (this.principals.Count > 0)
            {
                sb.Append(" TO ");
                sb.Append(String.Join(",", Array.ConvertAll<phPrincipal, string>(this.principals.ToArray(), p => p.ToString())));
            }

            if (this.GrantOption)
            {
                sb.Append(" WITH GRANT OPTION ");
            }

            if (this.AsPrincipal != null)
            {
                sb.Append(" AS ");
                sb.Append(this.AsPrincipal.ToString());
            }

            return sb.ToString();
        }
    }
}
