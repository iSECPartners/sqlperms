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
	class LogoutEventLoaderInfo : EventClassLoaderInfoBase
	{
		public LogoutEventLoaderInfo(IDataRecord record)
		{
			Contract.Requires(null != record, "The record object can not be null.");

			record.TryGetOrdinal("TextData", out _textDataOrdinal);
			record.TryGetOrdinal("DatabaseID", out _databaseIDOrdinal);
			record.TryGetOrdinal("NTUserName", out _nTUserNameOrdinal);
			record.TryGetOrdinal("NTDomainName", out _nTDomainNameOrdinal);
			record.TryGetOrdinal("HostName", out _hostNameOrdinal);
			record.TryGetOrdinal("ClientProcessID", out _clientProcessIDOrdinal);
			record.TryGetOrdinal("ApplicationName", out _applicationNameOrdinal);
			record.TryGetOrdinal("LoginName", out _loginNameOrdinal);
			record.TryGetOrdinal("SPID", out _sPIDOrdinal);
			record.TryGetOrdinal("Duration", out _durationOrdinal);
			record.TryGetOrdinal("StartTime", out _startTimeOrdinal);
			record.TryGetOrdinal("EndTime", out _endTimeOrdinal);
			record.TryGetOrdinal("Reads", out _readsOrdinal);
			record.TryGetOrdinal("Writes", out _writesOrdinal);
			record.TryGetOrdinal("CPU", out _cPUOrdinal);
			record.TryGetOrdinal("EventSubClass", out _eventSubClassOrdinal);
			record.TryGetOrdinal("Success", out _successOrdinal);
			record.TryGetOrdinal("ServerName", out _serverNameOrdinal);
			record.TryGetOrdinal("EventClass", out _eventClassOrdinal);
			record.TryGetOrdinal("DatabaseName", out _databaseNameOrdinal);
			record.TryGetOrdinal("LoginSid", out _loginSidOrdinal);
			record.TryGetOrdinal("RequestID", out _requestIDOrdinal);
			record.TryGetOrdinal("EventSequence", out _eventSequenceOrdinal);
			record.TryGetOrdinal("Type", out _typeOrdinal);
			record.TryGetOrdinal("IsSystem", out _isSystemOrdinal);
			record.TryGetOrdinal("SessionLoginName", out _sessionLoginNameOrdinal);
			record.TryGetOrdinal("GroupID", out _groupIDOrdinal);
			
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
		
		public Int32? HostNameOrdinal
		{
			get { return _hostNameOrdinal; }
			// set { _hostNameOrdinal = value; }
		}
		private readonly Int32? _hostNameOrdinal;
		
		public Int32? ClientProcessIDOrdinal
		{
			get { return _clientProcessIDOrdinal; }
			// set { _clientProcessIDOrdinal = value; }
		}
		private readonly Int32? _clientProcessIDOrdinal;
		
		public Int32? ApplicationNameOrdinal
		{
			get { return _applicationNameOrdinal; }
			// set { _applicationNameOrdinal = value; }
		}
		private readonly Int32? _applicationNameOrdinal;
		
		public Int32? LoginNameOrdinal
		{
			get { return _loginNameOrdinal; }
			// set { _loginNameOrdinal = value; }
		}
		private readonly Int32? _loginNameOrdinal;
		
		public Int32? SPIDOrdinal
		{
			get { return _sPIDOrdinal; }
			// set { _sPIDOrdinal = value; }
		}
		private readonly Int32? _sPIDOrdinal;
		
		public Int32? DurationOrdinal
		{
			get { return _durationOrdinal; }
			// set { _durationOrdinal = value; }
		}
		private readonly Int32? _durationOrdinal;
		
		public Int32? StartTimeOrdinal
		{
			get { return _startTimeOrdinal; }
			// set { _startTimeOrdinal = value; }
		}
		private readonly Int32? _startTimeOrdinal;
		
		public Int32? EndTimeOrdinal
		{
			get { return _endTimeOrdinal; }
			// set { _endTimeOrdinal = value; }
		}
		private readonly Int32? _endTimeOrdinal;
		
		public Int32? ReadsOrdinal
		{
			get { return _readsOrdinal; }
			// set { _readsOrdinal = value; }
		}
		private readonly Int32? _readsOrdinal;
		
		public Int32? WritesOrdinal
		{
			get { return _writesOrdinal; }
			// set { _writesOrdinal = value; }
		}
		private readonly Int32? _writesOrdinal;
		
		public Int32? CPUOrdinal
		{
			get { return _cPUOrdinal; }
			// set { _cPUOrdinal = value; }
		}
		private readonly Int32? _cPUOrdinal;
		
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
		
		public Int32? ServerNameOrdinal
		{
			get { return _serverNameOrdinal; }
			// set { _serverNameOrdinal = value; }
		}
		private readonly Int32? _serverNameOrdinal;
		
		public Int32? EventClassOrdinal
		{
			get { return _eventClassOrdinal; }
			// set { _eventClassOrdinal = value; }
		}
		private readonly Int32? _eventClassOrdinal;
		
		public Int32? DatabaseNameOrdinal
		{
			get { return _databaseNameOrdinal; }
			// set { _databaseNameOrdinal = value; }
		}
		private readonly Int32? _databaseNameOrdinal;
		
		public Int32? LoginSidOrdinal
		{
			get { return _loginSidOrdinal; }
			// set { _loginSidOrdinal = value; }
		}
		private readonly Int32? _loginSidOrdinal;
		
		public Int32? RequestIDOrdinal
		{
			get { return _requestIDOrdinal; }
			// set { _requestIDOrdinal = value; }
		}
		private readonly Int32? _requestIDOrdinal;
		
		public Int32? EventSequenceOrdinal
		{
			get { return _eventSequenceOrdinal; }
			// set { _eventSequenceOrdinal = value; }
		}
		private readonly Int32? _eventSequenceOrdinal;
		
		public Int32? TypeOrdinal
		{
			get { return _typeOrdinal; }
			// set { _typeOrdinal = value; }
		}
		private readonly Int32? _typeOrdinal;
		
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
		
		public Int32? GroupIDOrdinal
		{
			get { return _groupIDOrdinal; }
			// set { _groupIDOrdinal = value; }
		}
		private readonly Int32? _groupIDOrdinal;

	}
}
