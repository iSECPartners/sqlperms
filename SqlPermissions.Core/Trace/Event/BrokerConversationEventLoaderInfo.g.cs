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
	class BrokerConversationEventLoaderInfo : EventClassLoaderInfoBase
	{
		public BrokerConversationEventLoaderInfo(IDataRecord record)
		{
			Contract.Requires(null != record, "The record object can not be null.");

			record.TryGetOrdinal("TextData", out _textDataOrdinal);
			record.TryGetOrdinal("DatabaseID", out _databaseIDOrdinal);
			record.TryGetOrdinal("TransactionID", out _transactionIDOrdinal);
			record.TryGetOrdinal("NTUserName", out _nTUserNameOrdinal);
			record.TryGetOrdinal("NTDomainName", out _nTDomainNameOrdinal);
			record.TryGetOrdinal("HostName", out _hostNameOrdinal);
			record.TryGetOrdinal("ClientProcessID", out _clientProcessIDOrdinal);
			record.TryGetOrdinal("ApplicationName", out _applicationNameOrdinal);
			record.TryGetOrdinal("LoginName", out _loginNameOrdinal);
			record.TryGetOrdinal("SPID", out _sPIDOrdinal);
			record.TryGetOrdinal("StartTime", out _startTimeOrdinal);
			record.TryGetOrdinal("Severity", out _severityOrdinal);
			record.TryGetOrdinal("EventSubClass", out _eventSubClassOrdinal);
			record.TryGetOrdinal("ObjectID", out _objectIDOrdinal);
			record.TryGetOrdinal("IntegerData", out _integerDataOrdinal);
			record.TryGetOrdinal("ServerName", out _serverNameOrdinal);
			record.TryGetOrdinal("EventClass", out _eventClassOrdinal);
			record.TryGetOrdinal("State", out _stateOrdinal);
			record.TryGetOrdinal("Error", out _errorOrdinal);
			record.TryGetOrdinal("RoleName", out _roleNameOrdinal);
			record.TryGetOrdinal("DBUserName", out _dBUserNameOrdinal);
			record.TryGetOrdinal("LoginSid", out _loginSidOrdinal);
			record.TryGetOrdinal("TargetLoginName", out _targetLoginNameOrdinal);
			record.TryGetOrdinal("EventSequence", out _eventSequenceOrdinal);
			record.TryGetOrdinal("BigintData1", out _bigintData1Ordinal);
			record.TryGetOrdinal("GUID", out _gUIDOrdinal);
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
		
		public Int32? TransactionIDOrdinal
		{
			get { return _transactionIDOrdinal; }
			// set { _transactionIDOrdinal = value; }
		}
		private readonly Int32? _transactionIDOrdinal;
		
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
		
		public Int32? StartTimeOrdinal
		{
			get { return _startTimeOrdinal; }
			// set { _startTimeOrdinal = value; }
		}
		private readonly Int32? _startTimeOrdinal;
		
		public Int32? SeverityOrdinal
		{
			get { return _severityOrdinal; }
			// set { _severityOrdinal = value; }
		}
		private readonly Int32? _severityOrdinal;
		
		public Int32? EventSubClassOrdinal
		{
			get { return _eventSubClassOrdinal; }
			// set { _eventSubClassOrdinal = value; }
		}
		private readonly Int32? _eventSubClassOrdinal;
		
		public Int32? ObjectIDOrdinal
		{
			get { return _objectIDOrdinal; }
			// set { _objectIDOrdinal = value; }
		}
		private readonly Int32? _objectIDOrdinal;
		
		public Int32? IntegerDataOrdinal
		{
			get { return _integerDataOrdinal; }
			// set { _integerDataOrdinal = value; }
		}
		private readonly Int32? _integerDataOrdinal;
		
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
		
		public Int32? StateOrdinal
		{
			get { return _stateOrdinal; }
			// set { _stateOrdinal = value; }
		}
		private readonly Int32? _stateOrdinal;
		
		public Int32? ErrorOrdinal
		{
			get { return _errorOrdinal; }
			// set { _errorOrdinal = value; }
		}
		private readonly Int32? _errorOrdinal;
		
		public Int32? RoleNameOrdinal
		{
			get { return _roleNameOrdinal; }
			// set { _roleNameOrdinal = value; }
		}
		private readonly Int32? _roleNameOrdinal;
		
		public Int32? DBUserNameOrdinal
		{
			get { return _dBUserNameOrdinal; }
			// set { _dBUserNameOrdinal = value; }
		}
		private readonly Int32? _dBUserNameOrdinal;
		
		public Int32? LoginSidOrdinal
		{
			get { return _loginSidOrdinal; }
			// set { _loginSidOrdinal = value; }
		}
		private readonly Int32? _loginSidOrdinal;
		
		public Int32? TargetLoginNameOrdinal
		{
			get { return _targetLoginNameOrdinal; }
			// set { _targetLoginNameOrdinal = value; }
		}
		private readonly Int32? _targetLoginNameOrdinal;
		
		public Int32? EventSequenceOrdinal
		{
			get { return _eventSequenceOrdinal; }
			// set { _eventSequenceOrdinal = value; }
		}
		private readonly Int32? _eventSequenceOrdinal;
		
		public Int32? BigintData1Ordinal
		{
			get { return _bigintData1Ordinal; }
			// set { _bigintData1Ordinal = value; }
		}
		private readonly Int32? _bigintData1Ordinal;
		
		public Int32? GUIDOrdinal
		{
			get { return _gUIDOrdinal; }
			// set { _gUIDOrdinal = value; }
		}
		private readonly Int32? _gUIDOrdinal;
		
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
