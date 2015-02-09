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
	public enum SqlObjectLongType
	{
		/// <summary>Indicates no value is specified.</summary>
		None = 0,

		/// <summary>AQ (20801)</summary>
		ADHOC_QUERY = 20801,

		/// <summary>AF (17985)</summary>
		AGGREGATE = 17985,

		/// <summary>AR (21057)</summary>
		APPLICATION_ROLE = 21057,

		/// <summary>AS (21313)</summary>
		ASSEMBLY = 21313,

		/// <summary>AK (19265)</summary>
		ASYMMETRIC_KEY = 19265,

		/// <summary>AL (19521)</summary>
		ASYMMETRIC_KEY_LOGIN = 19521,

		/// <summary>AU (21825)</summary>
		ASYMMETRIC_KEY_USER = 21825,

		/// <summary>DU (21828)</summary>
		AUDIT = 21828,

		/// <summary>CR (21059)</summary>
		CERTIFICATE = 21059,

		/// <summary>CL (19523)</summary>
		CERTIFICATE_LOGIN = 19523,

		/// <summary>CU (21827)</summary>
		CERTIFICATE_USER = 21827,

		/// <summary>C (8259)</summary>
		CHECK_CONSTRAINT = 8259,

		/// <summary>CT (21571)</summary>
		CONTRACT = 21571,

		/// <summary>CD (17475)</summary>
		CREDENTIAL = 17475,

		/// <summary>CP (20547)</summary>
		CRYPTOGRAPHIC_PROVIDER = 20547,

		/// <summary>DB (16964)</summary>
		DATABASE = 16964,

		/// <summary>DA (16708)</summary>
		DATABASE_AUDIT_SPECIFICATION = 16708,

		/// <summary>DK (19268)</summary>
		DATABASE_ENCRYPTION_KEY = 19268,

		/// <summary>D (8260)</summary>
		DEFAULT = 8260,

		/// <summary>EP (20549)</summary>
		ENDPOINT = 20549,

		/// <summary>EN (20037)</summary>
		EVENT_NOTIFICATION = 20037,

		/// <summary>DN (20036)</summary>
		EVENT_NOTIFICATION_DATABASE = 20036,

		/// <summary>ON (20047)</summary>
		EVENT_NOTIFICATION_OBJECT = 20047,

		/// <summary>SD (17491)</summary>
		EVENT_NOTIFICATION_SERVER = 17491,

		/// <summary>SE (17747)</summary>
		EVENT_SESSION = 17747,

		/// <summary>F (8262)</summary>
		FOREIGN_KEY_CONSTRAINT = 8262,

		/// <summary>FC (17222)</summary>
		FULLTEXT_CATALOG = 17222,

		/// <summary>FL (19526)</summary>
		FULLTEXT_STOPLIST = 19526,

		/// <summary>FS (21318)</summary>
		FUNCTION_SCALAR_ASSEMBLY_ = 21318,

		/// <summary>IS (21321)</summary>
		FUNCTION_SCALAR_INLINE_SQL_ = 21321,

		/// <summary>FN (20038)</summary>
		FUNCTION_SCALAR_SQL = 20038,

		/// <summary>FT (21574)</summary>
		FUNCTION_TABLEVALUED_ASSEMBLY_ = 21574,

		/// <summary>IF (17993)</summary>
		FUNCTION_TABLEVALUED_INLINE_SQL = 17993,

		/// <summary>TF (18004)</summary>
		FUNCTION_TABLEVALUED_SQL = 18004,

		/// <summary>GU (21831)</summary>
		GROUP_USER = 21831,

		/// <summary>IX (22601)</summary>
		INDEX = 22601,

		/// <summary>IT (21577)</summary>
		INTERNAL_TABLE = 21577,

		/// <summary>L (22604)</summary>
		LOGIN = 22604,

		/// <summary>MK (19277)</summary>
		MASTER_KEY = 19277,

		/// <summary>MT (21581)</summary>
		MESSAGE_TYPE = 21581,

		/// <summary>OB (16975)</summary>
		OBJECT = 16975,

		/// <summary>PF (18000)</summary>
		PARTITION_FUNCTION = 18000,

		/// <summary>PS (21328)</summary>
		PARTITION_SCHEME = 21328,

		/// <summary>PQ (20816)</summary>
		PREPARED_ADHOC_QUERY = 20816,

		/// <summary>PK (19280)</summary>
		PRIMARY_KEY = 19280,

		/// <summary>SQ (20819)</summary>
		QUEUE = 20819,

		/// <summary>BN (20034)</summary>
		REMOTE_SERVICE_BINDING = 20034,

		/// <summary>RG (18258)</summary>
		RESOURCE_GOVERNOR = 18258,

		/// <summary>RL (19538)</summary>
		ROLE = 19538,

		/// <summary>RT (21586)</summary>
		ROUTE = 21586,

		/// <summary>R (8274)</summary>
		RULE = 8274,

		/// <summary>SC (17235)</summary>
		SCHEMA = 17235,

		/// <summary>SO (20307)</summary>
		SEQUENCE_OBJECT = 20307,

		/// <summary>SR (21075)</summary>
		SERVER = 21075,

		/// <summary>A (8257)</summary>
		SERVER_AUDIT = 8257,

		/// <summary>SA (16723)</summary>
		SERVER_AUDIT_SPECIFICATION = 16723,

		/// <summary>SG (18259)</summary>
		SERVER_ROLE = 18259,

		/// <summary>SV (22099)</summary>
		SERVICE = 22099,

		/// <summary>SL (19539)</summary>
		SQL_LOGIN = 19539,

		/// <summary>SU (21843)</summary>
		SQL_USER = 21843,

		/// <summary>ST (21587)</summary>
		STATISTICS = 21587,

		/// <summary>P (8272)</summary>
		STORED_PROCEDURE = 8272,

		/// <summary>PC (17232)</summary>
		STORED_PROCEDURE_ASSEMBLY = 17232,

		/// <summary>X (8280)</summary>
		STORED_PROCEDURE_EXTENDED = 8280,

		/// <summary>RF (18002)</summary>
		STORED_PROCEDURE_REPLICATION_FILTER = 18002,

		/// <summary>SK (19283)</summary>
		SYMMETRIC_KEY = 19283,

		/// <summary>SN (20051)</summary>
		SYNONYM = 20051,

		/// <summary>U (8277)</summary>
		TABLE = 8277,

		/// <summary>S (8275)</summary>
		TABLE_SYSTEM = 8275,

		/// <summary>TR (21076)</summary>
		TRIGGER = 21076,

		/// <summary>TA (16724)</summary>
		TRIGGER_ASSEMBLY = 16724,

		/// <summary>DT (21572)</summary>
		TRIGGER_DATABASE = 21572,

		/// <summary>T (8276)</summary>
		TRIGGER_SERVER = 8276,

		/// <summary>TY (22868)</summary>
		TYPE = 22868,

		/// <summary>AP (20545)</summary>
		Undocumented = 20545,

		/// <summary>UQ (20821)</summary>
		UNIQUE_CONSTRAINT = 20821,

		/// <summary>US (21333)</summary>
		USER = 21333,

		/// <summary>V (8278)</summary>
		VIEW = 8278,

		/// <summary>WG (18263)</summary>
		WINDOWS_GROUP = 18263,

		/// <summary>WL (19543)</summary>
		WINDOWS_LOGIN = 19543,

		/// <summary>WU (21847)</summary>
		WINDOWS_USER = 21847,

		/// <summary>SX (22611)</summary>
		XML_SCHEMA_COLLECTION = 22611,
	}
}
