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
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;

namespace SqlTest
{
    static class TestHelperExtension
    {
        public static void GrantWithAlter(this IObjectPermission obj, ObjectPermission perm, String principal,
                                          bool grantGrant = false)
        {
            Debug.Assert(obj is IAlterable, "Not alterable object?");

            obj.Grant(new ObjectPermissionSet(perm), principal, grantGrant);
            ((IAlterable)obj).Alter();
        }

        public static void RevokeWithAlter(this IObjectPermission obj, ObjectPermission perm, String principal)
        {
            Debug.Assert(obj is IAlterable, "Not alterable object?");

            obj.Revoke(new ObjectPermissionSet(perm), principal);
            ((IAlterable)obj).Alter();
        }

        public static void DenyWithAlter(this IObjectPermission obj, ObjectPermission perm, String principal)
        {
            Debug.Assert(obj is IAlterable, "Not alterable object?");

            obj.Deny(new ObjectPermissionSet(perm), principal);
            ((IAlterable)obj).Alter();
        }

        private static readonly IDictionary<Type, Func<Object, IEnumerable<ObjectPermission>>> _gdrMap
            = new Dictionary<Type, Func<Object, IEnumerable<ObjectPermission>>>
                {
                    {typeof (Table), o => GetTablePermissions()},
                    {typeof (View), o => GetTableLikeObjectPerms()},
                    {typeof (StoredProcedure), o => GetExecutableLikeObjectPerms()},
                    {
                        typeof (UserDefinedFunction), o =>
                            {
                                Debug.Assert(o is UserDefinedFunction);
                                var udf = o as UserDefinedFunction;

                                IEnumerable<ObjectPermission> retVal = null;
                                switch (udf.FunctionType)
                                {
                                    case UserDefinedFunctionType.Inline:
                                        retVal = GetTableLikeObjectPerms();
                                        break;
                                    case UserDefinedFunctionType.Scalar:
                                        retVal = GetScalarFunctionPerms();
                                        break;
                                    case UserDefinedFunctionType.Table:
                                        retVal = GetScalarFunctionPerms();
                                        break;
                                }
                                return retVal;
                            }
                    },
                };

        public static void GrantAll(this IObjectPermission obj, String principal, bool grantGrant = false)
        {
            _gdrMap[obj.GetType()](obj).Do(p => obj.GrantWithAlter(p, principal, grantGrant));
        }

        public static void RevokeAll(this IObjectPermission obj, String principal, bool grantGrant = false)
        {
            _gdrMap[obj.GetType()](obj).Do(p => obj.RevokeWithAlter(p, principal));
        }

        public static void DenyAll(this IObjectPermission obj, String principal, bool grantGrant = false)
        {
            _gdrMap[obj.GetType()](obj).Do(p => obj.DenyWithAlter(p, principal));
        }

        private static IEnumerable<ObjectPermission> GetExecutableLikeObjectPerms()
        {
            return new[]
                {
                    ObjectPermission.Execute,
                    ObjectPermission.Alter,
                    ObjectPermission.TakeOwnership,
                    ObjectPermission.ViewDefinition,
                    ObjectPermission.Control,
                };
        }

        private static IEnumerable<ObjectPermission> GetScalarFunctionPerms()
        {
            return GetExecutableLikeObjectPerms().Concat(new[] { ObjectPermission.References });
        }
        private static IEnumerable<ObjectPermission> GetTableLikeObjectPerms()
        {
            return new[]
                {
                    ObjectPermission.Select,
                    ObjectPermission.Insert,
                    ObjectPermission.Update,
                    ObjectPermission.Delete,
                    ObjectPermission.References,
                    ObjectPermission.Alter,
                    ObjectPermission.TakeOwnership,
                    ObjectPermission.ViewDefinition,
                    ObjectPermission.Control,
                };
        }

        private static IEnumerable<ObjectPermission> GetTablePermissions()
        {
            return GetTableLikeObjectPerms().Concat(new[] { ObjectPermission.ViewChangeTracking });
        }


        public static void Do<T>(this IEnumerable<T> elems, Action<T> todo)
        {
            foreach (var elem in elems)
            {
                todo(elem);
            }
        }
    }
}
