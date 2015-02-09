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


using System.Collections.Generic;
using SqlPermissions.Core.Permissions;
using System;
using System.Diagnostics.Contracts;

namespace SqlPermissions.Core.Trace.Event
{
    public interface IEventBase
    {
        String ToString(bool isDetailed);
        Int32 Id { get; }
        String Name { get; }
        Int32? DatabaseID { get; }
        String LoginName { get; }
        String NTUserName { get; }
        String NTDomainName { get; }
        String TextData { get; }

        IEnumerable<IAccessStatement> BuildPermissions();
    }

    //public static class EventBaseExtensions
    //{
    //    public static IEventBase Copy(this IEventBase evt)
    //    {
    //        return new EventBaseImpl(evt);
    //    }

    //    private class EventBaseImpl : IEventBase
    //    {
    //        public EventBaseImpl(IEventBase evt)
    //        {
    //            Contract.Requires(null != evt, "The evt must be valid.");
    //            Contract.Requires(0 < evt.Id, "The id must be positive.");
    //            Contract.Requires(!String.IsNullOrEmpty(evt.Name), "The name must be valid.");
    //            Contract.Requires(null == evt.DatabaseId || 0 < evt.DatabaseId, "The database id must be valid.");
    //            Contract.Requires(!String.IsNullOrEmpty(evt.LoginName), "The loginName must be valid.");
    //            Contract.Requires(!String.IsNullOrEmpty(evt.NtUserName), "The ntUserName must be valid.");
    //            Contract.Requires(!String.IsNullOrEmpty(evt.NtDomainName), "The ntDomainName must be valid.");

    //            _id = evt.Id;
    //            _name = evt.Name;
    //            _databaseId = evt.DatabaseId;
    //            _loginName = evt.LoginName;
    //            _ntUserName = evt.NtUserName;
    //            _ntDomainName = evt.NtDomainName;
    //        }

    //        public String ToString(Boolean isDetailed)
    //        {
    //            return "Event";
    //        }

    //        public Int32 Id
    //        {
    //            get { return _id; }
    //        }
    //        private readonly Int32 _id;

    //        public String Name
    //        {
    //            get { return _name; }
    //        }
    //        private readonly String _name;

    //        public Int32? DatabaseId
    //        {
    //            get { return _databaseId; }
    //        }
    //        private readonly Int32? _databaseId;

    //        public String LoginName
    //        {
    //            get { return _loginName; }
    //        }
    //        private readonly String _loginName;

    //        public String NtUserName
    //        {
    //            get { return _ntUserName; }
    //        }
    //        private readonly String _ntUserName;

    //        public String NtDomainName
    //        {
    //            get { return _ntDomainName; }
    //        }
    //        private readonly String _ntDomainName;

    //        public String TextData
    //        {
    //            get { return _textData; }
    //        }
    //        private readonly String _textData;
    //    }
    //}
}
