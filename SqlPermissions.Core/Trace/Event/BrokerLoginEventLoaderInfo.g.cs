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
	class BrokerLoginEventLoaderInfo : EventClassLoaderInfoBase
	{
		public BrokerLoginEventLoaderInfo(IDataRecord record)
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
			record.TryGetOrdinal("EventSubClass", out _eventSubClassOrdinal);
			record.TryGetOrdinal("ServerName", out _serverNameOrdinal);
			record.TryGetOrdinal("EventClass", out _eventClassOrdinal);
			record.TryGetOrdinal("State", out _stateOrdinal);
			record.TryGetOrdinal("ObjectName", out _objectNameOrdinal);
			record.TryGetOrdinal("FileName", out _fileNameOrdinal);
			record.TryGetOrdinal("OwnerName", out _ownerNameOrdinal);
			record.TryGetOrdinal("RoleName", out _roleNameOrdinal);
			record.TryGetOrdinal("TargetUserName", out _targetUserNameOrdinal);
			record.TryGetOrdinal("LoginSid", out _loginSidOrdinal);
			record.TryGetOrdinal("ProviderName", out _providerNameOrdinal);
			record.TryGetOrdinal("EventSequence", out _eventSequenceOrdinal);
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
		
		public Int32? EventSubClassOrdinal
		{
			get { return _eventSubClassOrdinal; }
			// set { _eventSubClassOrdinal = value; }
		}
		private readonly Int32? _eventSubClassOrdinal;
		
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
		
		public Int32? ObjectNameOrdinal
		{
			get { return _objectNameOrdinal; }
			// set { _objectNameOrdinal = value; }
		}
		private readonly Int32? _objectNameOrdinal;
		
		public Int32? FileNameOrdinal
		{
			get { return _fileNameOrdinal; }
			// set { _fileNameOrdinal = value; }
		}
		private readonly Int32? _fileNameOrdinal;
		
		public Int32? OwnerNameOrdinal
		{
			get { return _ownerNameOrdinal; }
			// set { _ownerNameOrdinal = value; }
		}
		private readonly Int32? _ownerNameOrdinal;
		
		public Int32? RoleNameOrdinal
		{
			get { return _roleNameOrdinal; }
			// set { _roleNameOrdinal = value; }
		}
		private readonly Int32? _roleNameOrdinal;
		
		public Int32? TargetUserNameOrdinal
		{
			get { return _targetUserNameOrdinal; }
			// set { _targetUserNameOrdinal = value; }
		}
		private readonly Int32? _targetUserNameOrdinal;
		
		public Int32? LoginSidOrdinal
		{
			get { return _loginSidOrdinal; }
			// set { _loginSidOrdinal = value; }
		}
		private readonly Int32? _loginSidOrdinal;
		
		public Int32? ProviderNameOrdinal
		{
			get { return _providerNameOrdinal; }
			// set { _providerNameOrdinal = value; }
		}
		private readonly Int32? _providerNameOrdinal;
		
		public Int32? EventSequenceOrdinal
		{
			get { return _eventSequenceOrdinal; }
			// set { _eventSequenceOrdinal = value; }
		}
		private readonly Int32? _eventSequenceOrdinal;
		
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
