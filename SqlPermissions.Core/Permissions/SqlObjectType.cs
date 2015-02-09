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
	public enum SqlObjectType
	{
		/// <summary>Indicates no value is specified.</summary>
		None = 0,

		/// <summary>SERVER AUDIT (8257)</summary>
		A = 8257,

		/// <summary>CHECK CONSTRAINT (8259)</summary>
		C = 8259,

		/// <summary>DEFAULT (8260)</summary>
		D = 8260,

		/// <summary>FOREIGN KEY CONSTRAINT (8262)</summary>
		F = 8262,

		/// <summary>STORED PROCEDURE (8272)</summary>
		P = 8272,

		/// <summary>RULE (8274)</summary>
		R = 8274,

		/// <summary>TABLE SYSTEM (8275)</summary>
		S = 8275,

		/// <summary>TRIGGER SERVER (8276)</summary>
		T = 8276,

		/// <summary>TABLE (8277)</summary>
		U = 8277,

		/// <summary>VIEW (8278)</summary>
		V = 8278,

		/// <summary>STORED PROCEDURE EXTENDED (8280)</summary>
		X = 8280,

		/// <summary>DATABASE AUDIT SPECIFICATION (16708)</summary>
		DA = 16708,

		/// <summary>SERVER AUDIT SPECIFICATION (16723)</summary>
		SA = 16723,

		/// <summary>TRIGGER ASSEMBLY (16724)</summary>
		TA = 16724,

		/// <summary>DATABASE (16964)</summary>
		DB = 16964,

		/// <summary>OBJECT (16975)</summary>
		OB = 16975,

		/// <summary>FULLTEXT CATALOG (17222)</summary>
		FC = 17222,

		/// <summary>STORED PROCEDURE ASSEMBLY (17232)</summary>
		PC = 17232,

		/// <summary>SCHEMA (17235)</summary>
		SC = 17235,

		/// <summary>CREDENTIAL (17475)</summary>
		CD = 17475,

		/// <summary>EVENT NOTIFICATION SERVER (17491)</summary>
		SD = 17491,

		/// <summary>EVENT SESSION (17747)</summary>
		SE = 17747,

		/// <summary>AGGREGATE (17985)</summary>
		AF = 17985,

		/// <summary>FUNCTION TABLE-VALUED INLINE SQL (17993)</summary>
		IF = 17993,

		/// <summary>PARTITION FUNCTION (18000)</summary>
		PF = 18000,

		/// <summary>STORED PROCEDURE REPLICATION FILTER (18002)</summary>
		RF = 18002,

		/// <summary>FUNCTION TABLE-VALUED SQL (18004)</summary>
		TF = 18004,

		/// <summary>RESOURCE GOVERNOR (18258)</summary>
		RG = 18258,

		/// <summary>SERVER ROLE (18259)</summary>
		SG = 18259,

		/// <summary>WINDOWS GROUP (18263)</summary>
		WG = 18263,

		/// <summary>ASYMMETRIC KEY (19265)</summary>
		AK = 19265,

		/// <summary>DATABASE ENCRYPTION KEY (19268)</summary>
		DK = 19268,

		/// <summary>MASTER KEY (19277)</summary>
		MK = 19277,

		/// <summary>PRIMARY KEY (19280)</summary>
		PK = 19280,

		/// <summary>SYMMETRIC KEY (19283)</summary>
		SK = 19283,

		/// <summary>ASYMMETRIC KEY LOGIN (19521)</summary>
		AL = 19521,

		/// <summary>CERTIFICATE LOGIN (19523)</summary>
		CL = 19523,

		/// <summary>FULLTEXT STOPLIST (19526)</summary>
		FL = 19526,

		/// <summary>ROLE (19538)</summary>
		RL = 19538,

		/// <summary>SQL LOGIN (19539)</summary>
		SL = 19539,

		/// <summary>WINDOWS LOGIN (19543)</summary>
		WL = 19543,

		/// <summary>REMOTE SERVICE BINDING (20034)</summary>
		BN = 20034,

		/// <summary>EVENT NOTIFICATION DATABASE (20036)</summary>
		DN = 20036,

		/// <summary>EVENT NOTIFICATION (20037)</summary>
		EN = 20037,

		/// <summary>FUNCTION SCALAR SQL (20038)</summary>
		FN = 20038,

		/// <summary>EVENT NOTIFICATION OBJECT (20047)</summary>
		ON = 20047,

		/// <summary>SYNONYM (20051)</summary>
		SN = 20051,

		/// <summary>SEQUENCE OBJECT (20307)</summary>
		SO = 20307,

		/// <summary>Undocumented (20545)</summary>
		AP = 20545,

		/// <summary>CRYPTOGRAPHIC PROVIDER (20547)</summary>
		CP = 20547,

		/// <summary>ENDPOINT (20549)</summary>
		EP = 20549,

		/// <summary>ADHOC QUERY (20801)</summary>
		AQ = 20801,

		/// <summary>PREPARED ADHOC QUERY (20816)</summary>
		PQ = 20816,

		/// <summary>QUEUE (20819)</summary>
		SQ = 20819,

		/// <summary>UNIQUE CONSTRAINT (20821)</summary>
		UQ = 20821,

		/// <summary>APPLICATION ROLE (21057)</summary>
		AR = 21057,

		/// <summary>CERTIFICATE (21059)</summary>
		CR = 21059,

		/// <summary>SERVER (21075)</summary>
		SR = 21075,

		/// <summary>TRIGGER (21076)</summary>
		TR = 21076,

		/// <summary>ASSEMBLY (21313)</summary>
		AS = 21313,

		/// <summary>FUNCTION SCALAR ASSEMBLY  (21318)</summary>
		FS = 21318,

		/// <summary>FUNCTION SCALAR INLINE SQL  (21321)</summary>
		IS = 21321,

		/// <summary>PARTITION SCHEME (21328)</summary>
		PS = 21328,

		/// <summary>USER (21333)</summary>
		US = 21333,

		/// <summary>CONTRACT (21571)</summary>
		CT = 21571,

		/// <summary>TRIGGER DATABASE (21572)</summary>
		DT = 21572,

		/// <summary>FUNCTION TABLE-VALUED ASSEMBLY  (21574)</summary>
		FT = 21574,

		/// <summary>INTERNAL TABLE (21577)</summary>
		IT = 21577,

		/// <summary>MESSAGE TYPE (21581)</summary>
		MT = 21581,

		/// <summary>ROUTE (21586)</summary>
		RT = 21586,

		/// <summary>STATISTICS (21587)</summary>
		ST = 21587,

		/// <summary>ASYMMETRIC KEY USER (21825)</summary>
		AU = 21825,

		/// <summary>CERTIFICATE USER (21827)</summary>
		CU = 21827,

		/// <summary>AUDIT (21828)</summary>
		DU = 21828,

		/// <summary>GROUP USER (21831)</summary>
		GU = 21831,

		/// <summary>SQL USER (21843)</summary>
		SU = 21843,

		/// <summary>WINDOWS USER (21847)</summary>
		WU = 21847,

		/// <summary>SERVICE (22099)</summary>
		SV = 22099,

		/// <summary>INDEX (22601)</summary>
		IX = 22601,

		/// <summary>LOGIN (22604)</summary>
		L = 22604,

		/// <summary>XML SCHEMA COLLECTION (22611)</summary>
		SX = 22611,

		/// <summary>TYPE (22868)</summary>
		TY = 22868,
	}
}
