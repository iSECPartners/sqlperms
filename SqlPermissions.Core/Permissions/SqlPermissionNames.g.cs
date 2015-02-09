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

namespace SqlPermissions.Core.Permissions
{
	/// <summary>Enumeration of the available object types.</summary>
	public static class SqlPermissionNames
	{

        private static readonly IDictionary<SqlPermissionName, String> _map = new SortedDictionary<SqlPermissionName, String>
            {
				{ SqlPermissionName.ADMINISTER_BULK_OPERATIONS, ADMINISTER_BULK_OPERATIONS },
				{ SqlPermissionName.ALTER, ALTER },
				{ SqlPermissionName.ALTER_ANY_APPLICATION_ROLE, ALTER_ANY_APPLICATION_ROLE },
				{ SqlPermissionName.ALTER_ANY_ASSEMBLY, ALTER_ANY_ASSEMBLY },
				{ SqlPermissionName.ALTER_ANY_ASYMMETRIC_KEY, ALTER_ANY_ASYMMETRIC_KEY },
				{ SqlPermissionName.ALTER_ANY_AVAILABILITY_GROUP, ALTER_ANY_AVAILABILITY_GROUP },
				{ SqlPermissionName.ALTER_ANY_CERTIFICATE, ALTER_ANY_CERTIFICATE },
				{ SqlPermissionName.ALTER_ANY_CONNECTION, ALTER_ANY_CONNECTION },
				{ SqlPermissionName.ALTER_ANY_CONTRACT, ALTER_ANY_CONTRACT },
				{ SqlPermissionName.ALTER_ANY_CREDENTIAL, ALTER_ANY_CREDENTIAL },
				{ SqlPermissionName.ALTER_ANY_DATABASE, ALTER_ANY_DATABASE },
				{ SqlPermissionName.ALTER_ANY_DATABASE_AUDIT, ALTER_ANY_DATABASE_AUDIT },
				{ SqlPermissionName.ALTER_ANY_DATABASE_DDL_TRIGGER, ALTER_ANY_DATABASE_DDL_TRIGGER },
				{ SqlPermissionName.ALTER_ANY_DATABASE_EVENT_NOTIFICATION, ALTER_ANY_DATABASE_EVENT_NOTIFICATION },
				{ SqlPermissionName.ALTER_ANY_DATABASE_EVENT_SESSION, ALTER_ANY_DATABASE_EVENT_SESSION },
				{ SqlPermissionName.ALTER_ANY_DATASPACE, ALTER_ANY_DATASPACE },
				{ SqlPermissionName.ALTER_ANY_ENDPOINT, ALTER_ANY_ENDPOINT },
				{ SqlPermissionName.ALTER_ANY_EVENT_NOTIFICATION, ALTER_ANY_EVENT_NOTIFICATION },
				{ SqlPermissionName.ALTER_ANY_EVENT_SESSION, ALTER_ANY_EVENT_SESSION },
				{ SqlPermissionName.ALTER_ANY_FULLTEXT_CATALOG, ALTER_ANY_FULLTEXT_CATALOG },
				{ SqlPermissionName.ALTER_ANY_LINKED_SERVER, ALTER_ANY_LINKED_SERVER },
				{ SqlPermissionName.ALTER_ANY_LOGIN, ALTER_ANY_LOGIN },
				{ SqlPermissionName.ALTER_ANY_MESSAGE_TYPE, ALTER_ANY_MESSAGE_TYPE },
				{ SqlPermissionName.ALTER_ANY_REMOTE_SERVICE_BINDING, ALTER_ANY_REMOTE_SERVICE_BINDING },
				{ SqlPermissionName.ALTER_ANY_ROLE, ALTER_ANY_ROLE },
				{ SqlPermissionName.ALTER_ANY_ROUTE, ALTER_ANY_ROUTE },
				{ SqlPermissionName.ALTER_ANY_SCHEMA, ALTER_ANY_SCHEMA },
				{ SqlPermissionName.ALTER_ANY_SERVER_AUDIT, ALTER_ANY_SERVER_AUDIT },
				{ SqlPermissionName.ALTER_ANY_SERVER_ROLE, ALTER_ANY_SERVER_ROLE },
				{ SqlPermissionName.ALTER_ANY_SERVICE, ALTER_ANY_SERVICE },
				{ SqlPermissionName.ALTER_ANY_SYMMETRIC_KEY, ALTER_ANY_SYMMETRIC_KEY },
				{ SqlPermissionName.ALTER_ANY_USER, ALTER_ANY_USER },
				{ SqlPermissionName.ALTER_RESOURCES, ALTER_RESOURCES },
				{ SqlPermissionName.ALTER_SERVER_STATE, ALTER_SERVER_STATE },
				{ SqlPermissionName.ALTER_SETTINGS, ALTER_SETTINGS },
				{ SqlPermissionName.ALTER_TRACE, ALTER_TRACE },
				{ SqlPermissionName.AUTHENTICATE, AUTHENTICATE },
				{ SqlPermissionName.AUTHENTICATE_SERVER, AUTHENTICATE_SERVER },
				{ SqlPermissionName.BACKUP_DATABASE, BACKUP_DATABASE },
				{ SqlPermissionName.BACKUP_LOG, BACKUP_LOG },
				{ SqlPermissionName.CHECKPOINT, CHECKPOINT },
				{ SqlPermissionName.CONNECT, CONNECT },
				{ SqlPermissionName.CONNECT_ANY_DATABASE, CONNECT_ANY_DATABASE },
				{ SqlPermissionName.CONNECT_REPLICATION, CONNECT_REPLICATION },
				{ SqlPermissionName.CONNECT_SQL, CONNECT_SQL },
				{ SqlPermissionName.CONTROL, CONTROL },
				{ SqlPermissionName.CONTROL_SERVER, CONTROL_SERVER },
				{ SqlPermissionName.CREATE_AGGREGATE, CREATE_AGGREGATE },
				{ SqlPermissionName.CREATE_ANY_DATABASE, CREATE_ANY_DATABASE },
				{ SqlPermissionName.CREATE_ASSEMBLY, CREATE_ASSEMBLY },
				{ SqlPermissionName.CREATE_ASYMMETRIC_KEY, CREATE_ASYMMETRIC_KEY },
				{ SqlPermissionName.CREATE_AVAILABILITY_GROUP, CREATE_AVAILABILITY_GROUP },
				{ SqlPermissionName.CREATE_CERTIFICATE, CREATE_CERTIFICATE },
				{ SqlPermissionName.CREATE_CONTRACT, CREATE_CONTRACT },
				{ SqlPermissionName.CREATE_DATABASE, CREATE_DATABASE },
				{ SqlPermissionName.CREATE_DATABASE_DDL_EVENT_NOTIFICATION, CREATE_DATABASE_DDL_EVENT_NOTIFICATION },
				{ SqlPermissionName.CREATE_DDL_EVENT_NOTIFICATION, CREATE_DDL_EVENT_NOTIFICATION },
				{ SqlPermissionName.CREATE_DEFAULT, CREATE_DEFAULT },
				{ SqlPermissionName.CREATE_ENDPOINT, CREATE_ENDPOINT },
				{ SqlPermissionName.CREATE_FULLTEXT_CATALOG, CREATE_FULLTEXT_CATALOG },
				{ SqlPermissionName.CREATE_FUNCTION, CREATE_FUNCTION },
				{ SqlPermissionName.CREATE_MESSAGE_TYPE, CREATE_MESSAGE_TYPE },
				{ SqlPermissionName.CREATE_PROCEDURE, CREATE_PROCEDURE },
				{ SqlPermissionName.CREATE_QUEUE, CREATE_QUEUE },
				{ SqlPermissionName.CREATE_REMOTE_SERVICE_BINDING, CREATE_REMOTE_SERVICE_BINDING },
				{ SqlPermissionName.CREATE_ROLE, CREATE_ROLE },
				{ SqlPermissionName.CREATE_ROUTE, CREATE_ROUTE },
				{ SqlPermissionName.CREATE_RULE, CREATE_RULE },
				{ SqlPermissionName.CREATE_SCHEMA, CREATE_SCHEMA },
				{ SqlPermissionName.CREATE_SEQUENCE, CREATE_SEQUENCE },
				{ SqlPermissionName.CREATE_SERVER_ROLE, CREATE_SERVER_ROLE },
				{ SqlPermissionName.CREATE_SERVICE, CREATE_SERVICE },
				{ SqlPermissionName.CREATE_SYMMETRIC_KEY, CREATE_SYMMETRIC_KEY },
				{ SqlPermissionName.CREATE_SYNONYM, CREATE_SYNONYM },
				{ SqlPermissionName.CREATE_TABLE, CREATE_TABLE },
				{ SqlPermissionName.CREATE_TRACE_EVENT_NOTIFICATION, CREATE_TRACE_EVENT_NOTIFICATION },
				{ SqlPermissionName.CREATE_TYPE, CREATE_TYPE },
				{ SqlPermissionName.CREATE_VIEW, CREATE_VIEW },
				{ SqlPermissionName.CREATE_XML_SCHEMA_COLLECTION, CREATE_XML_SCHEMA_COLLECTION },
				{ SqlPermissionName.DELETE, DELETE },
				{ SqlPermissionName.EXECUTE, EXECUTE },
				{ SqlPermissionName.EXTERNAL_ACCESS_ASSEMBLY, EXTERNAL_ACCESS_ASSEMBLY },
				{ SqlPermissionName.IMPERSONATE, IMPERSONATE },
				{ SqlPermissionName.IMPERSONATE_ANY_LOGIN, IMPERSONATE_ANY_LOGIN },
				{ SqlPermissionName.INSERT, INSERT },
				{ SqlPermissionName.KILL_DATABASE_CONNECTION, KILL_DATABASE_CONNECTION },
				{ SqlPermissionName.RECEIVE, RECEIVE },
				{ SqlPermissionName.REFERENCES, REFERENCES },
				{ SqlPermissionName.SELECT, SELECT },
				{ SqlPermissionName.SELECT_ALL_USER_SECURABLES, SELECT_ALL_USER_SECURABLES },
				{ SqlPermissionName.SEND, SEND },
				{ SqlPermissionName.SHOWPLAN, SHOWPLAN },
				{ SqlPermissionName.SHUTDOWN, SHUTDOWN },
				{ SqlPermissionName.SUBSCRIBE_QUERY_NOTIFICATIONS, SUBSCRIBE_QUERY_NOTIFICATIONS },
				{ SqlPermissionName.TAKE_OWNERSHIP, TAKE_OWNERSHIP },
				{ SqlPermissionName.UNSAFE_ASSEMBLY, UNSAFE_ASSEMBLY },
				{ SqlPermissionName.UPDATE, UPDATE },
				{ SqlPermissionName.VIEW_ANY_DATABASE, VIEW_ANY_DATABASE },
				{ SqlPermissionName.VIEW_ANY_DEFINITION, VIEW_ANY_DEFINITION },
				{ SqlPermissionName.VIEW_CHANGE_TRACKING, VIEW_CHANGE_TRACKING },
				{ SqlPermissionName.VIEW_DATABASE_STATE, VIEW_DATABASE_STATE },
				{ SqlPermissionName.VIEW_DEFINITION, VIEW_DEFINITION },
				{ SqlPermissionName.VIEW_SERVER_STATE, VIEW_SERVER_STATE },
            };

        public static String ToSqlString(this SqlPermissionName permissionName)
        {
            return _map[permissionName];
        }



		/// <summary>(1)</summary>
		public const String ADMINISTER_BULK_OPERATIONS = "ADMINISTER BULK OPERATIONS";

		/// <summary>(2)</summary>
		public const String ALTER = "ALTER";

		/// <summary>(3)</summary>
		public const String ALTER_ANY_APPLICATION_ROLE = "ALTER ANY APPLICATION ROLE";

		/// <summary>(4)</summary>
		public const String ALTER_ANY_ASSEMBLY = "ALTER ANY ASSEMBLY";

		/// <summary>(5)</summary>
		public const String ALTER_ANY_ASYMMETRIC_KEY = "ALTER ANY ASYMMETRIC KEY";

		/// <summary>(6)</summary>
		public const String ALTER_ANY_AVAILABILITY_GROUP = "ALTER ANY AVAILABILITY GROUP";

		/// <summary>(7)</summary>
		public const String ALTER_ANY_CERTIFICATE = "ALTER ANY CERTIFICATE";

		/// <summary>(8)</summary>
		public const String ALTER_ANY_CONNECTION = "ALTER ANY CONNECTION";

		/// <summary>(9)</summary>
		public const String ALTER_ANY_CONTRACT = "ALTER ANY CONTRACT";

		/// <summary>(10)</summary>
		public const String ALTER_ANY_CREDENTIAL = "ALTER ANY CREDENTIAL";

		/// <summary>(11)</summary>
		public const String ALTER_ANY_DATABASE = "ALTER ANY DATABASE";

		/// <summary>(12)</summary>
		public const String ALTER_ANY_DATABASE_AUDIT = "ALTER ANY DATABASE AUDIT";

		/// <summary>(13)</summary>
		public const String ALTER_ANY_DATABASE_DDL_TRIGGER = "ALTER ANY DATABASE DDL TRIGGER";

		/// <summary>(14)</summary>
		public const String ALTER_ANY_DATABASE_EVENT_NOTIFICATION = "ALTER ANY DATABASE EVENT NOTIFICATION";

		/// <summary>(15)</summary>
		public const String ALTER_ANY_DATABASE_EVENT_SESSION = "ALTER ANY DATABASE EVENT SESSION";

		/// <summary>(16)</summary>
		public const String ALTER_ANY_DATASPACE = "ALTER ANY DATASPACE";

		/// <summary>(17)</summary>
		public const String ALTER_ANY_ENDPOINT = "ALTER ANY ENDPOINT";

		/// <summary>(18)</summary>
		public const String ALTER_ANY_EVENT_NOTIFICATION = "ALTER ANY EVENT NOTIFICATION";

		/// <summary>(19)</summary>
		public const String ALTER_ANY_EVENT_SESSION = "ALTER ANY EVENT SESSION";

		/// <summary>(20)</summary>
		public const String ALTER_ANY_FULLTEXT_CATALOG = "ALTER ANY FULLTEXT CATALOG";

		/// <summary>(21)</summary>
		public const String ALTER_ANY_LINKED_SERVER = "ALTER ANY LINKED SERVER";

		/// <summary>(22)</summary>
		public const String ALTER_ANY_LOGIN = "ALTER ANY LOGIN";

		/// <summary>(23)</summary>
		public const String ALTER_ANY_MESSAGE_TYPE = "ALTER ANY MESSAGE TYPE";

		/// <summary>(24)</summary>
		public const String ALTER_ANY_REMOTE_SERVICE_BINDING = "ALTER ANY REMOTE SERVICE BINDING";

		/// <summary>(25)</summary>
		public const String ALTER_ANY_ROLE = "ALTER ANY ROLE";

		/// <summary>(26)</summary>
		public const String ALTER_ANY_ROUTE = "ALTER ANY ROUTE";

		/// <summary>(27)</summary>
		public const String ALTER_ANY_SCHEMA = "ALTER ANY SCHEMA";

		/// <summary>(28)</summary>
		public const String ALTER_ANY_SERVER_AUDIT = "ALTER ANY SERVER AUDIT";

		/// <summary>(29)</summary>
		public const String ALTER_ANY_SERVER_ROLE = "ALTER ANY SERVER ROLE";

		/// <summary>(30)</summary>
		public const String ALTER_ANY_SERVICE = "ALTER ANY SERVICE";

		/// <summary>(31)</summary>
		public const String ALTER_ANY_SYMMETRIC_KEY = "ALTER ANY SYMMETRIC KEY";

		/// <summary>(32)</summary>
		public const String ALTER_ANY_USER = "ALTER ANY USER";

		/// <summary>(33)</summary>
		public const String ALTER_RESOURCES = "ALTER RESOURCES";

		/// <summary>(34)</summary>
		public const String ALTER_SERVER_STATE = "ALTER SERVER STATE";

		/// <summary>(35)</summary>
		public const String ALTER_SETTINGS = "ALTER SETTINGS";

		/// <summary>(36)</summary>
		public const String ALTER_TRACE = "ALTER TRACE";

		/// <summary>(37)</summary>
		public const String AUTHENTICATE = "AUTHENTICATE";

		/// <summary>(38)</summary>
		public const String AUTHENTICATE_SERVER = "AUTHENTICATE SERVER";

		/// <summary>(39)</summary>
		public const String BACKUP_DATABASE = "BACKUP DATABASE";

		/// <summary>(40)</summary>
		public const String BACKUP_LOG = "BACKUP LOG";

		/// <summary>(41)</summary>
		public const String CHECKPOINT = "CHECKPOINT";

		/// <summary>(42)</summary>
		public const String CONNECT = "CONNECT";

		/// <summary>(43)</summary>
		public const String CONNECT_ANY_DATABASE = "CONNECT ANY DATABASE";

		/// <summary>(44)</summary>
		public const String CONNECT_REPLICATION = "CONNECT REPLICATION";

		/// <summary>(45)</summary>
		public const String CONNECT_SQL = "CONNECT SQL";

		/// <summary>(46)</summary>
		public const String CONTROL = "CONTROL";

		/// <summary>(47)</summary>
		public const String CONTROL_SERVER = "CONTROL SERVER";

		/// <summary>(48)</summary>
		public const String CREATE_AGGREGATE = "CREATE AGGREGATE";

		/// <summary>(49)</summary>
		public const String CREATE_ANY_DATABASE = "CREATE ANY DATABASE";

		/// <summary>(50)</summary>
		public const String CREATE_ASSEMBLY = "CREATE ASSEMBLY";

		/// <summary>(51)</summary>
		public const String CREATE_ASYMMETRIC_KEY = "CREATE ASYMMETRIC KEY";

		/// <summary>(52)</summary>
		public const String CREATE_AVAILABILITY_GROUP = "CREATE AVAILABILITY GROUP";

		/// <summary>(53)</summary>
		public const String CREATE_CERTIFICATE = "CREATE CERTIFICATE";

		/// <summary>(54)</summary>
		public const String CREATE_CONTRACT = "CREATE CONTRACT";

		/// <summary>(55)</summary>
		public const String CREATE_DATABASE = "CREATE DATABASE";

		/// <summary>(56)</summary>
		public const String CREATE_DATABASE_DDL_EVENT_NOTIFICATION = "CREATE DATABASE DDL EVENT NOTIFICATION";

		/// <summary>(57)</summary>
		public const String CREATE_DDL_EVENT_NOTIFICATION = "CREATE DDL EVENT NOTIFICATION";

		/// <summary>(58)</summary>
		public const String CREATE_DEFAULT = "CREATE DEFAULT";

		/// <summary>(59)</summary>
		public const String CREATE_ENDPOINT = "CREATE ENDPOINT";

		/// <summary>(60)</summary>
		public const String CREATE_FULLTEXT_CATALOG = "CREATE FULLTEXT CATALOG";

		/// <summary>(61)</summary>
		public const String CREATE_FUNCTION = "CREATE FUNCTION";

		/// <summary>(62)</summary>
		public const String CREATE_MESSAGE_TYPE = "CREATE MESSAGE TYPE";

		/// <summary>(63)</summary>
		public const String CREATE_PROCEDURE = "CREATE PROCEDURE";

		/// <summary>(64)</summary>
		public const String CREATE_QUEUE = "CREATE QUEUE";

		/// <summary>(65)</summary>
		public const String CREATE_REMOTE_SERVICE_BINDING = "CREATE REMOTE SERVICE BINDING";

		/// <summary>(66)</summary>
		public const String CREATE_ROLE = "CREATE ROLE";

		/// <summary>(67)</summary>
		public const String CREATE_ROUTE = "CREATE ROUTE";

		/// <summary>(68)</summary>
		public const String CREATE_RULE = "CREATE RULE";

		/// <summary>(69)</summary>
		public const String CREATE_SCHEMA = "CREATE SCHEMA";

		/// <summary>(70)</summary>
		public const String CREATE_SEQUENCE = "CREATE SEQUENCE";

		/// <summary>(71)</summary>
		public const String CREATE_SERVER_ROLE = "CREATE SERVER ROLE";

		/// <summary>(72)</summary>
		public const String CREATE_SERVICE = "CREATE SERVICE";

		/// <summary>(73)</summary>
		public const String CREATE_SYMMETRIC_KEY = "CREATE SYMMETRIC KEY";

		/// <summary>(74)</summary>
		public const String CREATE_SYNONYM = "CREATE SYNONYM";

		/// <summary>(75)</summary>
		public const String CREATE_TABLE = "CREATE TABLE";

		/// <summary>(76)</summary>
		public const String CREATE_TRACE_EVENT_NOTIFICATION = "CREATE TRACE EVENT NOTIFICATION";

		/// <summary>(77)</summary>
		public const String CREATE_TYPE = "CREATE TYPE";

		/// <summary>(78)</summary>
		public const String CREATE_VIEW = "CREATE VIEW";

		/// <summary>(79)</summary>
		public const String CREATE_XML_SCHEMA_COLLECTION = "CREATE XML SCHEMA COLLECTION";

		/// <summary>(80)</summary>
		public const String DELETE = "DELETE";

		/// <summary>(81)</summary>
		public const String EXECUTE = "EXECUTE";

		/// <summary>(82)</summary>
		public const String EXTERNAL_ACCESS_ASSEMBLY = "EXTERNAL ACCESS ASSEMBLY";

		/// <summary>(83)</summary>
		public const String IMPERSONATE = "IMPERSONATE";

		/// <summary>(84)</summary>
		public const String IMPERSONATE_ANY_LOGIN = "IMPERSONATE ANY LOGIN";

		/// <summary>(85)</summary>
		public const String INSERT = "INSERT";

		/// <summary>(86)</summary>
		public const String KILL_DATABASE_CONNECTION = "KILL DATABASE CONNECTION";

		/// <summary>(87)</summary>
		public const String RECEIVE = "RECEIVE";

		/// <summary>(88)</summary>
		public const String REFERENCES = "REFERENCES";

		/// <summary>(89)</summary>
		public const String SELECT = "SELECT";

		/// <summary>(90)</summary>
		public const String SELECT_ALL_USER_SECURABLES = "SELECT ALL USER SECURABLES";

		/// <summary>(91)</summary>
		public const String SEND = "SEND";

		/// <summary>(92)</summary>
		public const String SHOWPLAN = "SHOWPLAN";

		/// <summary>(93)</summary>
		public const String SHUTDOWN = "SHUTDOWN";

		/// <summary>(94)</summary>
		public const String SUBSCRIBE_QUERY_NOTIFICATIONS = "SUBSCRIBE QUERY NOTIFICATIONS";

		/// <summary>(95)</summary>
		public const String TAKE_OWNERSHIP = "TAKE OWNERSHIP";

		/// <summary>(96)</summary>
		public const String UNSAFE_ASSEMBLY = "UNSAFE ASSEMBLY";

		/// <summary>(97)</summary>
		public const String UPDATE = "UPDATE";

		/// <summary>(98)</summary>
		public const String VIEW_ANY_DATABASE = "VIEW ANY DATABASE";

		/// <summary>(99)</summary>
		public const String VIEW_ANY_DEFINITION = "VIEW ANY DEFINITION";

		/// <summary>(100)</summary>
		public const String VIEW_CHANGE_TRACKING = "VIEW CHANGE TRACKING";

		/// <summary>(101)</summary>
		public const String VIEW_DATABASE_STATE = "VIEW DATABASE STATE";

		/// <summary>(102)</summary>
		public const String VIEW_DEFINITION = "VIEW DEFINITION";

		/// <summary>(103)</summary>
		public const String VIEW_SERVER_STATE = "VIEW SERVER STATE";
	}
}
