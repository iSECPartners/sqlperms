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
using System.Data;
using System.Diagnostics.Contracts;
using SqlPermissions.Core.Utility;

namespace SqlPermissions.Core.Trace.Event
{
	class FulltextEventLoaderInfo : EventClassLoaderInfoBase
	{
		public FulltextEventLoaderInfo(IDataRecord record)
		{
			Contract.Requires(null != record, "The record object can not be null.");

			record.TryGetOrdinal("TextData", out _textDataOrdinal);
			record.TryGetOrdinal("DatabaseID", out _databaseIDOrdinal);
			record.TryGetOrdinal("NTUserName", out _nTUserNameOrdinal);
			record.TryGetOrdinal("NTDomainName", out _nTDomainNameOrdinal);
			record.TryGetOrdinal("SPID", out _sPIDOrdinal);
			record.TryGetOrdinal("LoginName", out _loginNameOrdinal);
			record.TryGetOrdinal("StartTime", out _startTimeOrdinal);
			record.TryGetOrdinal("EventSubClass", out _eventSubClassOrdinal);
			record.TryGetOrdinal("Success", out _successOrdinal);
			record.TryGetOrdinal("EventClass", out _eventClassOrdinal);
			record.TryGetOrdinal("Error", out _errorOrdinal);
			record.TryGetOrdinal("TargetLoginName", out _targetLoginNameOrdinal);
			record.TryGetOrdinal("TargetLoginSid", out _targetLoginSidOrdinal);
			record.TryGetOrdinal("EventSequence", out _eventSequenceOrdinal);
			record.TryGetOrdinal("IsSystem", out _isSystemOrdinal);
			record.TryGetOrdinal("SessionLoginName", out _sessionLoginNameOrdinal);
			
		}
		
		public Int32? TextDataOrdinal
		{
			get { return _textDataOrdinal; }
			// set { _textDataOrdinal = value; }
		}
		private readonly Int32? _textDataOrdinal;
		
		public Int32? DatabaseIDOrdinal
		{
			get { return _databaseIDOrdinal; }
			// set { _databaseIDOrdinal = value; }
		}
		private readonly Int32? _databaseIDOrdinal;
		
		public Int32? NTUserNameOrdinal
		{
			get { return _nTUserNameOrdinal; }
			// set { _nTUserNameOrdinal = value; }
		}
		private readonly Int32? _nTUserNameOrdinal;
		
		public Int32? NTDomainNameOrdinal
		{
			get { return _nTDomainNameOrdinal; }
			// set { _nTDomainNameOrdinal = value; }
		}
		private readonly Int32? _nTDomainNameOrdinal;
		
		public Int32? SPIDOrdinal
		{
			get { return _sPIDOrdinal; }
			// set { _sPIDOrdinal = value; }
		}
		private readonly Int32? _sPIDOrdinal;
		
		public Int32? LoginNameOrdinal
		{
			get { return _loginNameOrdinal; }
			// set { _loginNameOrdinal = value; }
		}
		private readonly Int32? _loginNameOrdinal;
		
		public Int32? StartTimeOrdinal
		{
			get { return _startTimeOrdinal; }
			// set { _startTimeOrdinal = value; }
		}
		private readonly Int32? _startTimeOrdinal;
		
		public Int32? EventSubClassOrdinal
		{
			get { return _eventSubClassOrdinal; }
			// set { _eventSubClassOrdinal = value; }
		}
		private readonly Int32? _eventSubClassOrdinal;
		
		public Int32? SuccessOrdinal
		{
			get { return _successOrdinal; }
			// set { _successOrdinal = value; }
		}
		private readonly Int32? _successOrdinal;
		
		public Int32? EventClassOrdinal
		{
			get { return _eventClassOrdinal; }
			// set { _eventClassOrdinal = value; }
		}
		private readonly Int32? _eventClassOrdinal;
		
		public Int32? ErrorOrdinal
		{
			get { return _errorOrdinal; }
			// set { _errorOrdinal = value; }
		}
		private readonly Int32? _errorOrdinal;
		
		public Int32? TargetLoginNameOrdinal
		{
			get { return _targetLoginNameOrdinal; }
			// set { _targetLoginNameOrdinal = value; }
		}
		private readonly Int32? _targetLoginNameOrdinal;
		
		public Int32? TargetLoginSidOrdinal
		{
			get { return _targetLoginSidOrdinal; }
			// set { _targetLoginSidOrdinal = value; }
		}
		private readonly Int32? _targetLoginSidOrdinal;
		
		public Int32? EventSequenceOrdinal
		{
			get { return _eventSequenceOrdinal; }
			// set { _eventSequenceOrdinal = value; }
		}
		private readonly Int32? _eventSequenceOrdinal;
		
		public Int32? IsSystemOrdinal
		{
			get { return _isSystemOrdinal; }
			// set { _isSystemOrdinal = value; }
		}
		private readonly Int32? _isSystemOrdinal;
		
		public Int32? SessionLoginNameOrdinal
		{
			get { return _sessionLoginNameOrdinal; }
			// set { _sessionLoginNameOrdinal = value; }
		}
		private readonly Int32? _sessionLoginNameOrdinal;

	}
}
