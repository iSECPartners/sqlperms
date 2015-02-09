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
	public static class SqlObjectLongTypes
	{

        private static readonly IDictionary<SqlObjectLongType, String> _map = new SortedDictionary<SqlObjectLongType, String>
            {
				{ SqlObjectLongType.ADHOC_QUERY, ADHOC_QUERY },
				{ SqlObjectLongType.AGGREGATE, AGGREGATE },
				{ SqlObjectLongType.APPLICATION_ROLE, APPLICATION_ROLE },
				{ SqlObjectLongType.ASSEMBLY, ASSEMBLY },
				{ SqlObjectLongType.ASYMMETRIC_KEY, ASYMMETRIC_KEY },
				{ SqlObjectLongType.ASYMMETRIC_KEY_LOGIN, ASYMMETRIC_KEY_LOGIN },
				{ SqlObjectLongType.ASYMMETRIC_KEY_USER, ASYMMETRIC_KEY_USER },
				{ SqlObjectLongType.AUDIT, AUDIT },
				{ SqlObjectLongType.CERTIFICATE, CERTIFICATE },
				{ SqlObjectLongType.CERTIFICATE_LOGIN, CERTIFICATE_LOGIN },
				{ SqlObjectLongType.CERTIFICATE_USER, CERTIFICATE_USER },
				{ SqlObjectLongType.CHECK_CONSTRAINT, CHECK_CONSTRAINT },
				{ SqlObjectLongType.CONTRACT, CONTRACT },
				{ SqlObjectLongType.CREDENTIAL, CREDENTIAL },
				{ SqlObjectLongType.CRYPTOGRAPHIC_PROVIDER, CRYPTOGRAPHIC_PROVIDER },
				{ SqlObjectLongType.DATABASE, DATABASE },
				{ SqlObjectLongType.DATABASE_AUDIT_SPECIFICATION, DATABASE_AUDIT_SPECIFICATION },
				{ SqlObjectLongType.DATABASE_ENCRYPTION_KEY, DATABASE_ENCRYPTION_KEY },
				{ SqlObjectLongType.DEFAULT, DEFAULT },
				{ SqlObjectLongType.ENDPOINT, ENDPOINT },
				{ SqlObjectLongType.EVENT_NOTIFICATION, EVENT_NOTIFICATION },
				{ SqlObjectLongType.EVENT_NOTIFICATION_DATABASE, EVENT_NOTIFICATION_DATABASE },
				{ SqlObjectLongType.EVENT_NOTIFICATION_OBJECT, EVENT_NOTIFICATION_OBJECT },
				{ SqlObjectLongType.EVENT_NOTIFICATION_SERVER, EVENT_NOTIFICATION_SERVER },
				{ SqlObjectLongType.EVENT_SESSION, EVENT_SESSION },
				{ SqlObjectLongType.FOREIGN_KEY_CONSTRAINT, FOREIGN_KEY_CONSTRAINT },
				{ SqlObjectLongType.FULLTEXT_CATALOG, FULLTEXT_CATALOG },
				{ SqlObjectLongType.FULLTEXT_STOPLIST, FULLTEXT_STOPLIST },
				{ SqlObjectLongType.FUNCTION_SCALAR_ASSEMBLY_, FUNCTION_SCALAR_ASSEMBLY_ },
				{ SqlObjectLongType.FUNCTION_SCALAR_INLINE_SQL_, FUNCTION_SCALAR_INLINE_SQL_ },
				{ SqlObjectLongType.FUNCTION_SCALAR_SQL, FUNCTION_SCALAR_SQL },
				{ SqlObjectLongType.FUNCTION_TABLEVALUED_ASSEMBLY_, FUNCTION_TABLEVALUED_ASSEMBLY_ },
				{ SqlObjectLongType.FUNCTION_TABLEVALUED_INLINE_SQL, FUNCTION_TABLEVALUED_INLINE_SQL },
				{ SqlObjectLongType.FUNCTION_TABLEVALUED_SQL, FUNCTION_TABLEVALUED_SQL },
				{ SqlObjectLongType.GROUP_USER, GROUP_USER },
				{ SqlObjectLongType.INDEX, INDEX },
				{ SqlObjectLongType.INTERNAL_TABLE, INTERNAL_TABLE },
				{ SqlObjectLongType.LOGIN, LOGIN },
				{ SqlObjectLongType.MASTER_KEY, MASTER_KEY },
				{ SqlObjectLongType.MESSAGE_TYPE, MESSAGE_TYPE },
				{ SqlObjectLongType.OBJECT, OBJECT },
				{ SqlObjectLongType.PARTITION_FUNCTION, PARTITION_FUNCTION },
				{ SqlObjectLongType.PARTITION_SCHEME, PARTITION_SCHEME },
				{ SqlObjectLongType.PREPARED_ADHOC_QUERY, PREPARED_ADHOC_QUERY },
				{ SqlObjectLongType.PRIMARY_KEY, PRIMARY_KEY },
				{ SqlObjectLongType.QUEUE, QUEUE },
				{ SqlObjectLongType.REMOTE_SERVICE_BINDING, REMOTE_SERVICE_BINDING },
				{ SqlObjectLongType.RESOURCE_GOVERNOR, RESOURCE_GOVERNOR },
				{ SqlObjectLongType.ROLE, ROLE },
				{ SqlObjectLongType.ROUTE, ROUTE },
				{ SqlObjectLongType.RULE, RULE },
				{ SqlObjectLongType.SCHEMA, SCHEMA },
				{ SqlObjectLongType.SEQUENCE_OBJECT, SEQUENCE_OBJECT },
				{ SqlObjectLongType.SERVER, SERVER },
				{ SqlObjectLongType.SERVER_AUDIT, SERVER_AUDIT },
				{ SqlObjectLongType.SERVER_AUDIT_SPECIFICATION, SERVER_AUDIT_SPECIFICATION },
				{ SqlObjectLongType.SERVER_ROLE, SERVER_ROLE },
				{ SqlObjectLongType.SERVICE, SERVICE },
				{ SqlObjectLongType.SQL_LOGIN, SQL_LOGIN },
				{ SqlObjectLongType.SQL_USER, SQL_USER },
				{ SqlObjectLongType.STATISTICS, STATISTICS },
				{ SqlObjectLongType.STORED_PROCEDURE, STORED_PROCEDURE },
				{ SqlObjectLongType.STORED_PROCEDURE_ASSEMBLY, STORED_PROCEDURE_ASSEMBLY },
				{ SqlObjectLongType.STORED_PROCEDURE_EXTENDED, STORED_PROCEDURE_EXTENDED },
				{ SqlObjectLongType.STORED_PROCEDURE_REPLICATION_FILTER, STORED_PROCEDURE_REPLICATION_FILTER },
				{ SqlObjectLongType.SYMMETRIC_KEY, SYMMETRIC_KEY },
				{ SqlObjectLongType.SYNONYM, SYNONYM },
				{ SqlObjectLongType.TABLE, TABLE },
				{ SqlObjectLongType.TABLE_SYSTEM, TABLE_SYSTEM },
				{ SqlObjectLongType.TRIGGER, TRIGGER },
				{ SqlObjectLongType.TRIGGER_ASSEMBLY, TRIGGER_ASSEMBLY },
				{ SqlObjectLongType.TRIGGER_DATABASE, TRIGGER_DATABASE },
				{ SqlObjectLongType.TRIGGER_SERVER, TRIGGER_SERVER },
				{ SqlObjectLongType.TYPE, TYPE },
				{ SqlObjectLongType.Undocumented, Undocumented },
				{ SqlObjectLongType.UNIQUE_CONSTRAINT, UNIQUE_CONSTRAINT },
				{ SqlObjectLongType.USER, USER },
				{ SqlObjectLongType.VIEW, VIEW },
				{ SqlObjectLongType.WINDOWS_GROUP, WINDOWS_GROUP },
				{ SqlObjectLongType.WINDOWS_LOGIN, WINDOWS_LOGIN },
				{ SqlObjectLongType.WINDOWS_USER, WINDOWS_USER },
				{ SqlObjectLongType.XML_SCHEMA_COLLECTION, XML_SCHEMA_COLLECTION },
            };

        public static String ToSqlString(this SqlObjectLongType objectLongType)
        {
            return _map[objectLongType];
        }



		/// <summary>AQ (20801)</summary>
		public const String ADHOC_QUERY = "ADHOC QUERY";

		/// <summary>AF (17985)</summary>
		public const String AGGREGATE = "AGGREGATE";

		/// <summary>AR (21057)</summary>
		public const String APPLICATION_ROLE = "APPLICATION ROLE";

		/// <summary>AS (21313)</summary>
		public const String ASSEMBLY = "ASSEMBLY";

		/// <summary>AK (19265)</summary>
		public const String ASYMMETRIC_KEY = "ASYMMETRIC KEY";

		/// <summary>AL (19521)</summary>
		public const String ASYMMETRIC_KEY_LOGIN = "ASYMMETRIC KEY LOGIN";

		/// <summary>AU (21825)</summary>
		public const String ASYMMETRIC_KEY_USER = "ASYMMETRIC KEY USER";

		/// <summary>DU (21828)</summary>
		public const String AUDIT = "AUDIT";

		/// <summary>CR (21059)</summary>
		public const String CERTIFICATE = "CERTIFICATE";

		/// <summary>CL (19523)</summary>
		public const String CERTIFICATE_LOGIN = "CERTIFICATE LOGIN";

		/// <summary>CU (21827)</summary>
		public const String CERTIFICATE_USER = "CERTIFICATE USER";

		/// <summary>C (8259)</summary>
		public const String CHECK_CONSTRAINT = "CHECK CONSTRAINT";

		/// <summary>CT (21571)</summary>
		public const String CONTRACT = "CONTRACT";

		/// <summary>CD (17475)</summary>
		public const String CREDENTIAL = "CREDENTIAL";

		/// <summary>CP (20547)</summary>
		public const String CRYPTOGRAPHIC_PROVIDER = "CRYPTOGRAPHIC PROVIDER";

		/// <summary>DB (16964)</summary>
		public const String DATABASE = "DATABASE";

		/// <summary>DA (16708)</summary>
		public const String DATABASE_AUDIT_SPECIFICATION = "DATABASE AUDIT SPECIFICATION";

		/// <summary>DK (19268)</summary>
		public const String DATABASE_ENCRYPTION_KEY = "DATABASE ENCRYPTION KEY";

		/// <summary>D (8260)</summary>
		public const String DEFAULT = "DEFAULT";

		/// <summary>EP (20549)</summary>
		public const String ENDPOINT = "ENDPOINT";

		/// <summary>EN (20037)</summary>
		public const String EVENT_NOTIFICATION = "EVENT NOTIFICATION";

		/// <summary>DN (20036)</summary>
		public const String EVENT_NOTIFICATION_DATABASE = "EVENT NOTIFICATION DATABASE";

		/// <summary>ON (20047)</summary>
		public const String EVENT_NOTIFICATION_OBJECT = "EVENT NOTIFICATION OBJECT";

		/// <summary>SD (17491)</summary>
		public const String EVENT_NOTIFICATION_SERVER = "EVENT NOTIFICATION SERVER";

		/// <summary>SE (17747)</summary>
		public const String EVENT_SESSION = "EVENT SESSION";

		/// <summary>F (8262)</summary>
		public const String FOREIGN_KEY_CONSTRAINT = "FOREIGN KEY CONSTRAINT";

		/// <summary>FC (17222)</summary>
		public const String FULLTEXT_CATALOG = "FULLTEXT CATALOG";

		/// <summary>FL (19526)</summary>
		public const String FULLTEXT_STOPLIST = "FULLTEXT STOPLIST";

		/// <summary>FS (21318)</summary>
		public const String FUNCTION_SCALAR_ASSEMBLY_ = "FUNCTION SCALAR ASSEMBLY ";

		/// <summary>IS (21321)</summary>
		public const String FUNCTION_SCALAR_INLINE_SQL_ = "FUNCTION SCALAR INLINE SQL ";

		/// <summary>FN (20038)</summary>
		public const String FUNCTION_SCALAR_SQL = "FUNCTION SCALAR SQL";

		/// <summary>FT (21574)</summary>
		public const String FUNCTION_TABLEVALUED_ASSEMBLY_ = "FUNCTION TABLE-VALUED ASSEMBLY ";

		/// <summary>IF (17993)</summary>
		public const String FUNCTION_TABLEVALUED_INLINE_SQL = "FUNCTION TABLE-VALUED INLINE SQL";

		/// <summary>TF (18004)</summary>
		public const String FUNCTION_TABLEVALUED_SQL = "FUNCTION TABLE-VALUED SQL";

		/// <summary>GU (21831)</summary>
		public const String GROUP_USER = "GROUP USER";

		/// <summary>IX (22601)</summary>
		public const String INDEX = "INDEX";

		/// <summary>IT (21577)</summary>
		public const String INTERNAL_TABLE = "INTERNAL TABLE";

		/// <summary>L (22604)</summary>
		public const String LOGIN = "LOGIN";

		/// <summary>MK (19277)</summary>
		public const String MASTER_KEY = "MASTER KEY";

		/// <summary>MT (21581)</summary>
		public const String MESSAGE_TYPE = "MESSAGE TYPE";

		/// <summary>OB (16975)</summary>
		public const String OBJECT = "OBJECT";

		/// <summary>PF (18000)</summary>
		public const String PARTITION_FUNCTION = "PARTITION FUNCTION";

		/// <summary>PS (21328)</summary>
		public const String PARTITION_SCHEME = "PARTITION SCHEME";

		/// <summary>PQ (20816)</summary>
		public const String PREPARED_ADHOC_QUERY = "PREPARED ADHOC QUERY";

		/// <summary>PK (19280)</summary>
		public const String PRIMARY_KEY = "PRIMARY KEY";

		/// <summary>SQ (20819)</summary>
		public const String QUEUE = "QUEUE";

		/// <summary>BN (20034)</summary>
		public const String REMOTE_SERVICE_BINDING = "REMOTE SERVICE BINDING";

		/// <summary>RG (18258)</summary>
		public const String RESOURCE_GOVERNOR = "RESOURCE GOVERNOR";

		/// <summary>RL (19538)</summary>
		public const String ROLE = "ROLE";

		/// <summary>RT (21586)</summary>
		public const String ROUTE = "ROUTE";

		/// <summary>R (8274)</summary>
		public const String RULE = "RULE";

		/// <summary>SC (17235)</summary>
		public const String SCHEMA = "SCHEMA";

		/// <summary>SO (20307)</summary>
		public const String SEQUENCE_OBJECT = "SEQUENCE OBJECT";

		/// <summary>SR (21075)</summary>
		public const String SERVER = "SERVER";

		/// <summary>A (8257)</summary>
		public const String SERVER_AUDIT = "SERVER AUDIT";

		/// <summary>SA (16723)</summary>
		public const String SERVER_AUDIT_SPECIFICATION = "SERVER AUDIT SPECIFICATION";

		/// <summary>SG (18259)</summary>
		public const String SERVER_ROLE = "SERVER ROLE";

		/// <summary>SV (22099)</summary>
		public const String SERVICE = "SERVICE";

		/// <summary>SL (19539)</summary>
		public const String SQL_LOGIN = "SQL LOGIN";

		/// <summary>SU (21843)</summary>
		public const String SQL_USER = "SQL USER";

		/// <summary>ST (21587)</summary>
		public const String STATISTICS = "STATISTICS";

		/// <summary>P (8272)</summary>
		public const String STORED_PROCEDURE = "STORED PROCEDURE";

		/// <summary>PC (17232)</summary>
		public const String STORED_PROCEDURE_ASSEMBLY = "STORED PROCEDURE ASSEMBLY";

		/// <summary>X (8280)</summary>
		public const String STORED_PROCEDURE_EXTENDED = "STORED PROCEDURE EXTENDED";

		/// <summary>RF (18002)</summary>
		public const String STORED_PROCEDURE_REPLICATION_FILTER = "STORED PROCEDURE REPLICATION FILTER";

		/// <summary>SK (19283)</summary>
		public const String SYMMETRIC_KEY = "SYMMETRIC KEY";

		/// <summary>SN (20051)</summary>
		public const String SYNONYM = "SYNONYM";

		/// <summary>U (8277)</summary>
		public const String TABLE = "TABLE";

		/// <summary>S (8275)</summary>
		public const String TABLE_SYSTEM = "TABLE SYSTEM";

		/// <summary>TR (21076)</summary>
		public const String TRIGGER = "TRIGGER";

		/// <summary>TA (16724)</summary>
		public const String TRIGGER_ASSEMBLY = "TRIGGER ASSEMBLY";

		/// <summary>DT (21572)</summary>
		public const String TRIGGER_DATABASE = "TRIGGER DATABASE";

		/// <summary>T (8276)</summary>
		public const String TRIGGER_SERVER = "TRIGGER SERVER";

		/// <summary>TY (22868)</summary>
		public const String TYPE = "TYPE";

		/// <summary>AP (20545)</summary>
		public const String Undocumented = "Undocumented";

		/// <summary>UQ (20821)</summary>
		public const String UNIQUE_CONSTRAINT = "UNIQUE CONSTRAINT";

		/// <summary>US (21333)</summary>
		public const String USER = "USER";

		/// <summary>V (8278)</summary>
		public const String VIEW = "VIEW";

		/// <summary>WG (18263)</summary>
		public const String WINDOWS_GROUP = "WINDOWS GROUP";

		/// <summary>WL (19543)</summary>
		public const String WINDOWS_LOGIN = "WINDOWS LOGIN";

		/// <summary>WU (21847)</summary>
		public const String WINDOWS_USER = "WINDOWS USER";

		/// <summary>SX (22611)</summary>
		public const String XML_SCHEMA_COLLECTION = "XML SCHEMA COLLECTION";
	}
}
