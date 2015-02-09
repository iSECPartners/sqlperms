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
    /// <summary>Base class for the code-gen'd event classes. It provides a base set of
    /// common parameters for events, as well as providing the abstract structure
    /// for Easily implementing BuildPermissions.
    /// </summary>
    /// <remarks>Derived classes should be partial, and the code-gen'd half will already inherit
    /// from this class. The code half need only implement BuildPermissionInternal or
    /// BuildPermissionsInternal depending on whether the event, when translated, will 
    /// include multiple IAcccessStatements or just one.
    /// </remarks>
    public abstract class AbstractEventBase : IEventBase
    {
        public abstract String ToString(bool isDetailed);
        public abstract Int32 Id { get; }
        public abstract String Name { get; }
        public abstract Int32? DatabaseID { get; set; }
        public abstract String LoginName { get; set; }
        public abstract String NTUserName { get; set; }
        public abstract String NTDomainName { get; set; }
        public abstract String TextData { get; set; }

        /// <summary>Builds the permissions.</summary>
        /// <remarks>Provides the method to get the permissions for a given event.</remarks>
        /// <returns></returns>
        public IEnumerable<IAccessStatement> BuildPermissions()
        {
            return BuildPermissionsInternal();
        }

        /// <summary>Builds the permission internal.</summary>
        /// <remarks>Override this to build a single permission from the event.</remarks>
        /// <returns></returns>
        protected virtual IAccessStatement BuildPermissionInternal()
        {
            return new  UnimplementedAccessStatement(this);
        }

        /// <summary>Builds the permissions internal.</summary>
        /// <remarks>Override this to build multiple permissions from the event.</remarks>
        /// <returns></returns>
        protected virtual IEnumerable<IAccessStatement> BuildPermissionsInternal()
        {
            return GetAccessStatementsList(BuildPermissionInternal());
        }

        protected IEnumerable<IAccessStatement> GetAccessStatementsList(params IAccessStatement[] accessStatemenets)
        {
            return accessStatemenets;
        }
    }
}
