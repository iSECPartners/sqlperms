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




namespace SqlPermissions.Core.Permissions
{
	/// <summary>Enumeration of the available object types.</summary>
	public enum SqlPermissionName
	{
		/// <summary>Indicates no value is specified.</summary>
		None = 0,

		/// <summary>(1)</summary>
		ADMINISTER_BULK_OPERATIONS = 1,

		/// <summary>(2)</summary>
		ALTER = 2,

		/// <summary>(3)</summary>
		ALTER_ANY_APPLICATION_ROLE = 3,

		/// <summary>(4)</summary>
		ALTER_ANY_ASSEMBLY = 4,

		/// <summary>(5)</summary>
		ALTER_ANY_ASYMMETRIC_KEY = 5,

		/// <summary>(6)</summary>
		ALTER_ANY_AVAILABILITY_GROUP = 6,

		/// <summary>(7)</summary>
		ALTER_ANY_CERTIFICATE = 7,

		/// <summary>(8)</summary>
		ALTER_ANY_CONNECTION = 8,

		/// <summary>(9)</summary>
		ALTER_ANY_CONTRACT = 9,

		/// <summary>(10)</summary>
		ALTER_ANY_CREDENTIAL = 10,

		/// <summary>(11)</summary>
		ALTER_ANY_DATABASE = 11,

		/// <summary>(12)</summary>
		ALTER_ANY_DATABASE_AUDIT = 12,

		/// <summary>(13)</summary>
		ALTER_ANY_DATABASE_DDL_TRIGGER = 13,

		/// <summary>(14)</summary>
		ALTER_ANY_DATABASE_EVENT_NOTIFICATION = 14,

		/// <summary>(15)</summary>
		ALTER_ANY_DATABASE_EVENT_SESSION = 15,

		/// <summary>(16)</summary>
		ALTER_ANY_DATASPACE = 16,

		/// <summary>(17)</summary>
		ALTER_ANY_ENDPOINT = 17,

		/// <summary>(18)</summary>
		ALTER_ANY_EVENT_NOTIFICATION = 18,

		/// <summary>(19)</summary>
		ALTER_ANY_EVENT_SESSION = 19,

		/// <summary>(20)</summary>
		ALTER_ANY_FULLTEXT_CATALOG = 20,

		/// <summary>(21)</summary>
		ALTER_ANY_LINKED_SERVER = 21,

		/// <summary>(22)</summary>
		ALTER_ANY_LOGIN = 22,

		/// <summary>(23)</summary>
		ALTER_ANY_MESSAGE_TYPE = 23,

		/// <summary>(24)</summary>
		ALTER_ANY_REMOTE_SERVICE_BINDING = 24,

		/// <summary>(25)</summary>
		ALTER_ANY_ROLE = 25,

		/// <summary>(26)</summary>
		ALTER_ANY_ROUTE = 26,

		/// <summary>(27)</summary>
		ALTER_ANY_SCHEMA = 27,

		/// <summary>(28)</summary>
		ALTER_ANY_SERVER_AUDIT = 28,

		/// <summary>(29)</summary>
		ALTER_ANY_SERVER_ROLE = 29,

		/// <summary>(30)</summary>
		ALTER_ANY_SERVICE = 30,

		/// <summary>(31)</summary>
		ALTER_ANY_SYMMETRIC_KEY = 31,

		/// <summary>(32)</summary>
		ALTER_ANY_USER = 32,

		/// <summary>(33)</summary>
		ALTER_RESOURCES = 33,

		/// <summary>(34)</summary>
		ALTER_SERVER_STATE = 34,

		/// <summary>(35)</summary>
		ALTER_SETTINGS = 35,

		/// <summary>(36)</summary>
		ALTER_TRACE = 36,

		/// <summary>(37)</summary>
		AUTHENTICATE = 37,

		/// <summary>(38)</summary>
		AUTHENTICATE_SERVER = 38,

		/// <summary>(39)</summary>
		BACKUP_DATABASE = 39,

		/// <summary>(40)</summary>
		BACKUP_LOG = 40,

		/// <summary>(41)</summary>
		CHECKPOINT = 41,

		/// <summary>(42)</summary>
		CONNECT = 42,

		/// <summary>(43)</summary>
		CONNECT_ANY_DATABASE = 43,

		/// <summary>(44)</summary>
		CONNECT_REPLICATION = 44,

		/// <summary>(45)</summary>
		CONNECT_SQL = 45,

		/// <summary>(46)</summary>
		CONTROL = 46,

		/// <summary>(47)</summary>
		CONTROL_SERVER = 47,

		/// <summary>(48)</summary>
		CREATE_AGGREGATE = 48,

		/// <summary>(49)</summary>
		CREATE_ANY_DATABASE = 49,

		/// <summary>(50)</summary>
		CREATE_ASSEMBLY = 50,

		/// <summary>(51)</summary>
		CREATE_ASYMMETRIC_KEY = 51,

		/// <summary>(52)</summary>
		CREATE_AVAILABILITY_GROUP = 52,

		/// <summary>(53)</summary>
		CREATE_CERTIFICATE = 53,

		/// <summary>(54)</summary>
		CREATE_CONTRACT = 54,

		/// <summary>(55)</summary>
		CREATE_DATABASE = 55,

		/// <summary>(56)</summary>
		CREATE_DATABASE_DDL_EVENT_NOTIFICATION = 56,

		/// <summary>(57)</summary>
		CREATE_DDL_EVENT_NOTIFICATION = 57,

		/// <summary>(58)</summary>
		CREATE_DEFAULT = 58,

		/// <summary>(59)</summary>
		CREATE_ENDPOINT = 59,

		/// <summary>(60)</summary>
		CREATE_FULLTEXT_CATALOG = 60,

		/// <summary>(61)</summary>
		CREATE_FUNCTION = 61,

		/// <summary>(62)</summary>
		CREATE_MESSAGE_TYPE = 62,

		/// <summary>(63)</summary>
		CREATE_PROCEDURE = 63,

		/// <summary>(64)</summary>
		CREATE_QUEUE = 64,

		/// <summary>(65)</summary>
		CREATE_REMOTE_SERVICE_BINDING = 65,

		/// <summary>(66)</summary>
		CREATE_ROLE = 66,

		/// <summary>(67)</summary>
		CREATE_ROUTE = 67,

		/// <summary>(68)</summary>
		CREATE_RULE = 68,

		/// <summary>(69)</summary>
		CREATE_SCHEMA = 69,

		/// <summary>(70)</summary>
		CREATE_SEQUENCE = 70,

		/// <summary>(71)</summary>
		CREATE_SERVER_ROLE = 71,

		/// <summary>(72)</summary>
		CREATE_SERVICE = 72,

		/// <summary>(73)</summary>
		CREATE_SYMMETRIC_KEY = 73,

		/// <summary>(74)</summary>
		CREATE_SYNONYM = 74,

		/// <summary>(75)</summary>
		CREATE_TABLE = 75,

		/// <summary>(76)</summary>
		CREATE_TRACE_EVENT_NOTIFICATION = 76,

		/// <summary>(77)</summary>
		CREATE_TYPE = 77,

		/// <summary>(78)</summary>
		CREATE_VIEW = 78,

		/// <summary>(79)</summary>
		CREATE_XML_SCHEMA_COLLECTION = 79,

		/// <summary>(80)</summary>
		DELETE = 80,

		/// <summary>(81)</summary>
		EXECUTE = 81,

		/// <summary>(82)</summary>
		EXTERNAL_ACCESS_ASSEMBLY = 82,

		/// <summary>(83)</summary>
		IMPERSONATE = 83,

		/// <summary>(84)</summary>
		IMPERSONATE_ANY_LOGIN = 84,

		/// <summary>(85)</summary>
		INSERT = 85,

		/// <summary>(86)</summary>
		KILL_DATABASE_CONNECTION = 86,

		/// <summary>(87)</summary>
		RECEIVE = 87,

		/// <summary>(88)</summary>
		REFERENCES = 88,

		/// <summary>(89)</summary>
		SELECT = 89,

		/// <summary>(90)</summary>
		SELECT_ALL_USER_SECURABLES = 90,

		/// <summary>(91)</summary>
		SEND = 91,

		/// <summary>(92)</summary>
		SHOWPLAN = 92,

		/// <summary>(93)</summary>
		SHUTDOWN = 93,

		/// <summary>(94)</summary>
		SUBSCRIBE_QUERY_NOTIFICATIONS = 94,

		/// <summary>(95)</summary>
		TAKE_OWNERSHIP = 95,

		/// <summary>(96)</summary>
		UNSAFE_ASSEMBLY = 96,

		/// <summary>(97)</summary>
		UPDATE = 97,

		/// <summary>(98)</summary>
		VIEW_ANY_DATABASE = 98,

		/// <summary>(99)</summary>
		VIEW_ANY_DEFINITION = 99,

		/// <summary>(100)</summary>
		VIEW_CHANGE_TRACKING = 100,

		/// <summary>(101)</summary>
		VIEW_DATABASE_STATE = 101,

		/// <summary>(102)</summary>
		VIEW_DEFINITION = 102,

		/// <summary>(103)</summary>
		VIEW_SERVER_STATE = 103,
	}
}
