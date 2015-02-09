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
using System.Text;

using SqlPermissions.Core.Utility;

namespace SqlPermissions.Core.Trace.Event
{
	public partial class AddDbUserEvent : AbstractEventBase
	{
		internal AddDbUserEvent(IDataRecord record, AddDbUserEventLoaderInfo loaderInfo)
		{
			Contract.Requires(null != record, "The record must be valid.");
			Contract.Requires(null != loaderInfo, "The loaderInfo must be valid.");

			if (null != loaderInfo.TextDataOrdinal)
				_textData = record.GetNullableString(loaderInfo.TextDataOrdinal.Value);
			if (null != loaderInfo.DatabaseIDOrdinal)
				_databaseID = record.GetNullableInt32(loaderInfo.DatabaseIDOrdinal.Value);
			if (null != loaderInfo.TransactionIDOrdinal)
				_transactionID = record.GetNullableInt64(loaderInfo.TransactionIDOrdinal.Value);
			if (null != loaderInfo.NTUserNameOrdinal)
				_nTUserName = record.GetNullableString(loaderInfo.NTUserNameOrdinal.Value);
			if (null != loaderInfo.NTDomainNameOrdinal)
				_nTDomainName = record.GetNullableString(loaderInfo.NTDomainNameOrdinal.Value);
			if (null != loaderInfo.HostNameOrdinal)
				_hostName = record.GetNullableString(loaderInfo.HostNameOrdinal.Value);
			if (null != loaderInfo.ClientProcessIDOrdinal)
				_clientProcessID = record.GetNullableInt32(loaderInfo.ClientProcessIDOrdinal.Value);
			if (null != loaderInfo.ApplicationNameOrdinal)
				_applicationName = record.GetNullableString(loaderInfo.ApplicationNameOrdinal.Value);
			if (null != loaderInfo.LoginNameOrdinal)
				_loginName = record.GetNullableString(loaderInfo.LoginNameOrdinal.Value);
			if (null != loaderInfo.SPIDOrdinal)
				_sPID = record.GetNullableInt32(loaderInfo.SPIDOrdinal.Value);
			if (null != loaderInfo.StartTimeOrdinal)
				_startTime = record.GetNullableDateTime(loaderInfo.StartTimeOrdinal.Value);
			if (null != loaderInfo.EventSubClassOrdinal)
				_eventSubClass = record.GetNullableInt32(loaderInfo.EventSubClassOrdinal.Value);
			if (null != loaderInfo.SuccessOrdinal)
				_success = record.GetNullableInt32(loaderInfo.SuccessOrdinal.Value);
			if (null != loaderInfo.ServerNameOrdinal)
				_serverName = record.GetNullableString(loaderInfo.ServerNameOrdinal.Value);
			if (null != loaderInfo.ObjectTypeOrdinal)
				_objectType = record.GetNullableInt32(loaderInfo.ObjectTypeOrdinal.Value);
			if (null != loaderInfo.NestLevelOrdinal)
				_nestLevel = record.GetNullableInt32(loaderInfo.NestLevelOrdinal.Value);
			if (null != loaderInfo.DatabaseNameOrdinal)
				_databaseName = record.GetNullableString(loaderInfo.DatabaseNameOrdinal.Value);
			if (null != loaderInfo.OwnerNameOrdinal)
				_ownerName = record.GetNullableString(loaderInfo.OwnerNameOrdinal.Value);
			if (null != loaderInfo.RoleNameOrdinal)
				_roleName = record.GetNullableString(loaderInfo.RoleNameOrdinal.Value);
			if (null != loaderInfo.TargetUserNameOrdinal)
				_targetUserName = record.GetNullableString(loaderInfo.TargetUserNameOrdinal.Value);
			if (null != loaderInfo.DBUserNameOrdinal)
				_dBUserName = record.GetNullableString(loaderInfo.DBUserNameOrdinal.Value);
			if (null != loaderInfo.LoginSidOrdinal)
				_loginSid = (Byte[])record.GetValue(loaderInfo.LoginSidOrdinal.Value);
			if (null != loaderInfo.TargetLoginNameOrdinal)
				_targetLoginName = record.GetNullableString(loaderInfo.TargetLoginNameOrdinal.Value);
			if (null != loaderInfo.TargetLoginSidOrdinal)
				_targetLoginSid = (Byte[])record.GetValue(loaderInfo.TargetLoginSidOrdinal.Value);
			if (null != loaderInfo.ColumnPermissionsOrdinal)
				_columnPermissions = record.GetNullableInt32(loaderInfo.ColumnPermissionsOrdinal.Value);
			if (null != loaderInfo.RequestIDOrdinal)
				_requestID = record.GetNullableInt32(loaderInfo.RequestIDOrdinal.Value);
			if (null != loaderInfo.XactSequenceOrdinal)
				_xactSequence = record.GetNullableInt64(loaderInfo.XactSequenceOrdinal.Value);
			if (null != loaderInfo.EventSequenceOrdinal)
				_eventSequence = record.GetNullableInt64(loaderInfo.EventSequenceOrdinal.Value);
			if (null != loaderInfo.IsSystemOrdinal)
				_isSystem = record.GetNullableInt32(loaderInfo.IsSystemOrdinal.Value);
			if (null != loaderInfo.SessionLoginNameOrdinal)
				_sessionLoginName = record.GetNullableString(loaderInfo.SessionLoginNameOrdinal.Value);
		}

		public override String Name
		{ get { return EventName; } }
		private const String EventName = "Audit Add DB User Event";

		public override Int32 Id
		{ get { return EventId; } }
		private const Int32 EventId = 109;

		
		public override String TextData
		{
			get { return _textData; }
			set { _textData = value; }
		}
		private String _textData;
		
		public override Int32? DatabaseID
		{
			get { return _databaseID; }
			set { _databaseID = value; }
		}
		private Int32? _databaseID;
		
		public  Int64? TransactionID
		{
			get { return _transactionID; }
			set { _transactionID = value; }
		}
		private Int64? _transactionID;
		
		public override String NTUserName
		{
			get { return _nTUserName; }
			set { _nTUserName = value; }
		}
		private String _nTUserName;
		
		public override String NTDomainName
		{
			get { return _nTDomainName; }
			set { _nTDomainName = value; }
		}
		private String _nTDomainName;
		
		public  String HostName
		{
			get { return _hostName; }
			set { _hostName = value; }
		}
		private String _hostName;
		
		public  Int32? ClientProcessID
		{
			get { return _clientProcessID; }
			set { _clientProcessID = value; }
		}
		private Int32? _clientProcessID;
		
		public  String ApplicationName
		{
			get { return _applicationName; }
			set { _applicationName = value; }
		}
		private String _applicationName;
		
		public override String LoginName
		{
			get { return _loginName; }
			set { _loginName = value; }
		}
		private String _loginName;
		
		public  Int32? SPID
		{
			get { return _sPID; }
			set { _sPID = value; }
		}
		private Int32? _sPID;
		
		public  DateTime? StartTime
		{
			get { return _startTime; }
			set { _startTime = value; }
		}
		private DateTime? _startTime;
		
		public  Int32? EventSubClass
		{
			get { return _eventSubClass; }
			set { _eventSubClass = value; }
		}
		private Int32? _eventSubClass;
		
		public  Int32? Success
		{
			get { return _success; }
			set { _success = value; }
		}
		private Int32? _success;
		
		public  String ServerName
		{
			get { return _serverName; }
			set { _serverName = value; }
		}
		private String _serverName;
		
		public  Int32? ObjectType
		{
			get { return _objectType; }
			set { _objectType = value; }
		}
		private Int32? _objectType;
		
		public  Int32? NestLevel
		{
			get { return _nestLevel; }
			set { _nestLevel = value; }
		}
		private Int32? _nestLevel;
		
		public  String DatabaseName
		{
			get { return _databaseName; }
			set { _databaseName = value; }
		}
		private String _databaseName;
		
		public  String OwnerName
		{
			get { return _ownerName; }
			set { _ownerName = value; }
		}
		private String _ownerName;
		
		public  String RoleName
		{
			get { return _roleName; }
			set { _roleName = value; }
		}
		private String _roleName;
		
		public  String TargetUserName
		{
			get { return _targetUserName; }
			set { _targetUserName = value; }
		}
		private String _targetUserName;
		
		public  String DBUserName
		{
			get { return _dBUserName; }
			set { _dBUserName = value; }
		}
		private String _dBUserName;
		
		public  Byte[] LoginSid
		{
			get { return _loginSid; }
			set { _loginSid = value; }
		}
		private Byte[] _loginSid;
		
		public  String TargetLoginName
		{
			get { return _targetLoginName; }
			set { _targetLoginName = value; }
		}
		private String _targetLoginName;
		
		public  Byte[] TargetLoginSid
		{
			get { return _targetLoginSid; }
			set { _targetLoginSid = value; }
		}
		private Byte[] _targetLoginSid;
		
		public  Int32? ColumnPermissions
		{
			get { return _columnPermissions; }
			set { _columnPermissions = value; }
		}
		private Int32? _columnPermissions;
		
		public  Int32? RequestID
		{
			get { return _requestID; }
			set { _requestID = value; }
		}
		private Int32? _requestID;
		
		public  Int64? XactSequence
		{
			get { return _xactSequence; }
			set { _xactSequence = value; }
		}
		private Int64? _xactSequence;
		
		public  Int64? EventSequence
		{
			get { return _eventSequence; }
			set { _eventSequence = value; }
		}
		private Int64? _eventSequence;
		
		public  Int32? IsSystem
		{
			get { return _isSystem; }
			set { _isSystem = value; }
		}
		private Int32? _isSystem;
		
		public  String SessionLoginName
		{
			get { return _sessionLoginName; }
			set { _sessionLoginName = value; }
		}
		private String _sessionLoginName;

		public override String ToString()
		{
			const Boolean IsDetailed = false;
			return ToString(IsDetailed);
		}

		public override String ToString(bool isDetailed)
		{
			const String Line1 = "Event Class: Audit Add DB User Event (AddDbUserEvent)";

			// return the short version if requested
			if (!isDetailed)
				return Line1;

			const Int32 ExpectedCapacity = 0x100;
			var sb = new StringBuilder(ExpectedCapacity);

			sb.AppendLine(Line1);

			sb.AppendLine("\tEventClassId: 109");
			sb.Append("\tTextData: ").AppendLine(Convert.ToString(TextData));
			sb.Append("\tDatabaseID: ").AppendLine(Convert.ToString(DatabaseID));
			sb.Append("\tTransactionID: ").AppendLine(Convert.ToString(TransactionID));
			sb.Append("\tNTUserName: ").AppendLine(Convert.ToString(NTUserName));
			sb.Append("\tNTDomainName: ").AppendLine(Convert.ToString(NTDomainName));
			sb.Append("\tHostName: ").AppendLine(Convert.ToString(HostName));
			sb.Append("\tClientProcessID: ").AppendLine(Convert.ToString(ClientProcessID));
			sb.Append("\tApplicationName: ").AppendLine(Convert.ToString(ApplicationName));
			sb.Append("\tLoginName: ").AppendLine(Convert.ToString(LoginName));
			sb.Append("\tSPID: ").AppendLine(Convert.ToString(SPID));
			sb.Append("\tStartTime: ").AppendLine(Convert.ToString(StartTime));
			sb.Append("\tEventSubClass: ").AppendLine(Convert.ToString(EventSubClass));
			sb.Append("\tSuccess: ").AppendLine(Convert.ToString(Success));
			sb.Append("\tServerName: ").AppendLine(Convert.ToString(ServerName));
			sb.Append("\tObjectType: ").AppendLine(Convert.ToString(ObjectType));
			sb.Append("\tNestLevel: ").AppendLine(Convert.ToString(NestLevel));
			sb.Append("\tDatabaseName: ").AppendLine(Convert.ToString(DatabaseName));
			sb.Append("\tOwnerName: ").AppendLine(Convert.ToString(OwnerName));
			sb.Append("\tRoleName: ").AppendLine(Convert.ToString(RoleName));
			sb.Append("\tTargetUserName: ").AppendLine(Convert.ToString(TargetUserName));
			sb.Append("\tDBUserName: ").AppendLine(Convert.ToString(DBUserName));
			sb.Append("\tLoginSid: ").AppendLine(null != LoginSid ? "0x" + BitConverter.ToString(LoginSid).Replace("-", String.Empty) : "");
			sb.Append("\tTargetLoginName: ").AppendLine(Convert.ToString(TargetLoginName));
			sb.Append("\tTargetLoginSid: ").AppendLine(null != TargetLoginSid ? "0x" + BitConverter.ToString(TargetLoginSid).Replace("-", String.Empty) : "");
			sb.Append("\tColumnPermissions: ").AppendLine(Convert.ToString(ColumnPermissions));
			sb.Append("\tRequestID: ").AppendLine(Convert.ToString(RequestID));
			sb.Append("\tXactSequence: ").AppendLine(Convert.ToString(XactSequence));
			sb.Append("\tEventSequence: ").AppendLine(Convert.ToString(EventSequence));
			sb.Append("\tIsSystem: ").AppendLine(Convert.ToString(IsSystem));
			sb.Append("\tSessionLoginName: ").AppendLine(Convert.ToString(SessionLoginName));

			return sb.ToString();
		}
	}
}
