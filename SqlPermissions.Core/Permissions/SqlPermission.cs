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
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlPermissions.Core.Utility;

namespace SqlPermissions.Core.Permissions
{

    public class SqlPermission
    {
 
        public SqlPermission(string objectClass, string permission)
            : this((SecurableObjectType)Enum.Parse(typeof(SecurableObjectType), objectClass.Replace(" ", ""), true), permission)
        { }

        public SqlPermission(SecurableObjectType objectClass, string permission)
        {
            this.PermissionClass = objectClass;
            this.Permission = permission;
        }

        public SecurableObjectType PermissionClass
        {
            get;
            private set;
        }

        public string Permission
        {
            get;
            private set;
        }

        public static SqlPermission GetPermission(int objectId, long permissions)
        {
            CheckPopulationCountIsZero(permissions);
            Tuple<int, long> key = new Tuple<int, long>(objectId, permissions);

            if (_map.ContainsKey(key))
            {
                return _map[key];
            }

            Debug.WriteLine("permission map does not contain objectId(" + objectId + ") permissions(" + permissions + ")");
            throw new KeyNotFoundException("objectId(" + objectId + ") permissions(" + permissions + ")");
        }

        [Conditional("DEBUG")]
        private static void CheckPopulationCountIsZero(long bits)
        {
            var permissions = bits;
            var count = 0;
            while (0 != bits)
            {
                count += (int)(bits & 0x1L);
                bits >>= 1; // shift down
            }

            Debug.Assert(1 == count, String.Format("More than one permission {0:x2}.", permissions));
        }

        public static SqlPermission GetPermission(int objectTypeId, String permissionName)
        {
            IEnumerable<SqlPermission> permissions;
            if (!_map2.TryGetValue(objectTypeId, out permissions))
            {
                ExceptionEx.Throw<KeyNotFoundException>("Object Type ({0}", objectTypeId);
                return null; // never happen
            }

            return permissions.First(sp => sp.Permission == permissionName);
        }

        public override string ToString()
        {
            return String.Format(CultureInfo.InvariantCulture,
                                 "{0}: {1}::{2}", "SqlPermission", PermissionClass, Permission);
        }

        #region sql queries

        /*
         * The crazy SQL query to generate the content below:
         * OBJECT types were hard coded for Aggregate, Default, Function, Procedure, Queue, Rule, Synonym, Table and View.
         *          There didn't appear to be a place this hierarchy was expressed in metadata
         * 
         * This query also uses the system table dbo.spt_values :-(

WITH
  Pass0 as (select 1 as C union all select 1), --2 rows
  Pass1 as (select 1 as C from Pass0 as A, Pass0 as B),--4 rows
  Pass2 as (select 1 as C from Pass1 as A, Pass1 as B),--16 rows
  Pass3 as (select 1 as C from Pass2 as A, Pass2 as B),--256 rows
  Pass4 as (select 1 as C from Pass3 as A, Pass3 as B),--65536 rows
  Pass5 as (select 1 as C from Pass4 as A, Pass4 as B),--4,294,967,296 rows
  Tally as (select row_number() over(order by C) as Number from Pass5)
  , Types as (select distinct subclass_name [Short], subclass_value [value], name from (select distinct trace_column_id from sys.trace_columns where name = 'ObjectType') c
	join sys.trace_subclass_values sc on c.trace_column_id = sc.trace_column_id
	left outer join dbo.spt_values o on sc.subclass_value = o.number 
	where  o.type = 'EOD')
select (CHAR(9) + CHAR(9) + CHAR(9) + CHAR(9)) + N'{ Tuple.Create(' + cast(q.value as nvarchar(255)) + ', ' + cast(q.number as nvarchar(255)) 
		+ 'L), new SqlPermission("' + cast(q.Class as nvarchar(255)) COLLATE SQL_Latin1_General_CP1_CI_AS + '", "' + q.permission_name + '") }, // ' + q.name  from (
select bip.class_desc [Class], t.Short, t.value, t.name, cast(n.Number as binary(8)) [bin_number], n.Number, tp.permission_name, tp.type from (select distinct class_desc from sys.fn_builtin_permissions(default)) bip
	join Types t on (bip.class_desc = t.name COLLATE SQL_Latin1_General_CP1_CI_AS)
					OR (bip.class_desc = N'OBJECT' AND t.Short IN ('AF', 'D', 'FS', 'IS', 'FN', 'FT', 'IF', 'TF', 'SQ', 'R', 'P', 'PC', 'X', 'RF', 'SN', 'U', 'S', 'V'))
	cross join (select power(cast(2 as bigint), Number - 1) [Number] from Tally where Number <= 63) n
	cross apply sys.fn_translate_permissions(bip.class_desc, n.Number ) tp
	--where bip.class_desc = 'object'
	)q
         * 
         * 
         * 
         * 
         * Dict<int, IEnum<SqlPerm>>
  

WITH
  Pass0 as (select 1 as C union all select 1), --2 rows
  Pass1 as (select 1 as C from Pass0 as A, Pass0 as B),--4 rows
  Pass2 as (select 1 as C from Pass1 as A, Pass1 as B),--16 rows
  Pass3 as (select 1 as C from Pass2 as A, Pass2 as B),--256 rows
  Pass4 as (select 1 as C from Pass3 as A, Pass3 as B),--65536 rows
  --Pass5 as (select 1 as C from Pass4 as A, Pass4 as B),--4,294,967,296 rows
  Tally as (select row_number() over(order by C) as Number from Pass4)
  , Types as (select distinct subclass_name [Short], subclass_value [value], name from (select distinct trace_column_id from sys.trace_columns where name = 'ObjectType') c
	join sys.trace_subclass_values sc on c.trace_column_id = sc.trace_column_id
	left outer join dbo.spt_values o on sc.subclass_value = o.number 
	where  o.type = 'EOD')
  , q as (select bip.class_desc [Class], t.Short, t.value, t.name, cast(n.Number as binary(8)) [bin_number], n.Number, tp.permission_name, tp.type from (select distinct class_desc from sys.fn_builtin_permissions(default)) bip
	join Types t on (bip.class_desc = t.name COLLATE SQL_Latin1_General_CP1_CI_AS)
					OR (bip.class_desc = N'OBJECT' AND t.Short IN ('AF', 'D', 'FS', 'IS', 'FN', 'FT', 'IF', 'TF', 'SQ', 'R', 'P', 'PC', 'X', 'RF', 'SN', 'U', 'S', 'V'))
	cross join (select power(cast(2 as bigint), Number - 1) [Number] from Tally where Number <= 63) n
	cross apply sys.fn_translate_permissions(bip.class_desc, n.Number ) tp
	--where bip.class_desc = 'object'
	)
select (CHAR(9) + CHAR(9) + CHAR(9) + CHAR(9)) + N'{ ' + cast(q.value as nvarchar(255)) + N', new [] { '
	+ (select N'new SqlPermission("' + cast(iq.Class as nvarchar(255)) COLLATE SQL_Latin1_General_CP1_CI_AS + '", "' + iq.permission_name + '") , ' from q iq where q.value = iq.value FOR XML PATH(''))
	+ N' } },'
	FROM q
	group by q.value

         */

        #endregion

        #region Map2 dict

        private static readonly IDictionary<int, IEnumerable<SqlPermission>> _map2 = new SortedDictionary
            <int, IEnumerable<SqlPermission>>
            {
                {
                    8275,
                    new[]
                        {
                            new SqlPermission("OBJECT", "SELECT"), new SqlPermission("OBJECT", "UPDATE"),
                            new SqlPermission("OBJECT", "REFERENCES"), new SqlPermission("OBJECT", "INSERT"),
                            new SqlPermission("OBJECT", "DELETE"), new SqlPermission("OBJECT", "EXECUTE"),
                            new SqlPermission("OBJECT", "RECEIVE"), new SqlPermission("OBJECT", "VIEW DEFINITION"),
                            new SqlPermission("OBJECT", "ALTER"), new SqlPermission("OBJECT", "TAKE OWNERSHIP"),
                            new SqlPermission("OBJECT", "CONTROL"), new SqlPermission("OBJECT", "VIEW CHANGE TRACKING"),
                        }
                },
                {
                    17985,
                    new[]
                        {
                            new SqlPermission("OBJECT", "SELECT"), new SqlPermission("OBJECT", "UPDATE"),
                            new SqlPermission("OBJECT", "REFERENCES"), new SqlPermission("OBJECT", "INSERT"),
                            new SqlPermission("OBJECT", "DELETE"), new SqlPermission("OBJECT", "EXECUTE"),
                            new SqlPermission("OBJECT", "RECEIVE"), new SqlPermission("OBJECT", "VIEW DEFINITION"),
                            new SqlPermission("OBJECT", "ALTER"), new SqlPermission("OBJECT", "TAKE OWNERSHIP"),
                            new SqlPermission("OBJECT", "CONTROL"), new SqlPermission("OBJECT", "VIEW CHANGE TRACKING"),
                        }
                },
                {
                    20034,
                    new[]
                        {
                            new SqlPermission("REMOTE SERVICE BINDING", "VIEW DEFINITION"),
                            new SqlPermission("REMOTE SERVICE BINDING", "ALTER"),
                            new SqlPermission("REMOTE SERVICE BINDING", "TAKE OWNERSHIP"),
                            new SqlPermission("REMOTE SERVICE BINDING", "CONTROL"),
                        }
                },
                {
                    21059,
                    new[]
                        {
                            new SqlPermission("CERTIFICATE", "VIEW DEFINITION"),
                            new SqlPermission("CERTIFICATE", "REFERENCES"), new SqlPermission("CERTIFICATE", "ALTER"),
                            new SqlPermission("CERTIFICATE", "TAKE OWNERSHIP"),
                            new SqlPermission("CERTIFICATE", "CONTROL"),
                        }
                },
                {
                    21075,
                    new[]
                        {
                            new SqlPermission("SERVER", "CONNECT SQL"), new SqlPermission("SERVER", "SHUTDOWN"),
                            new SqlPermission("SERVER", "ALTER ANY AVAILABILITY GROUP"),
                            new SqlPermission("SERVER", "CREATE AVAILABILITY GROUP"),
                            new SqlPermission("SERVER", "CREATE ENDPOINT"),
                            new SqlPermission("SERVER", "CREATE ANY DATABASE"),
                            new SqlPermission("SERVER", "ALTER ANY LOGIN"),
                            new SqlPermission("SERVER", "ALTER ANY CREDENTIAL"),
                            new SqlPermission("SERVER", "ALTER ANY ENDPOINT"),
                            new SqlPermission("SERVER", "ALTER ANY LINKED SERVER"),
                            new SqlPermission("SERVER", "ALTER ANY CONNECTION"),
                            new SqlPermission("SERVER", "ALTER ANY DATABASE"),
                            new SqlPermission("SERVER", "ALTER RESOURCES"),
                            new SqlPermission("SERVER", "ALTER SETTINGS"), new SqlPermission("SERVER", "ALTER TRACE"),
                            new SqlPermission("SERVER", "ADMINISTER BULK OPERATIONS"),
                            new SqlPermission("SERVER", "AUTHENTICATE SERVER"),
                            new SqlPermission("SERVER", "EXTERNAL ACCESS ASSEMBLY"),
                            new SqlPermission("SERVER", "VIEW ANY DATABASE"),
                            new SqlPermission("SERVER", "VIEW ANY DEFINITION"),
                            new SqlPermission("SERVER", "VIEW SERVER STATE"),
                            new SqlPermission("SERVER", "CREATE DDL EVENT NOTIFICATION"),
                            new SqlPermission("SERVER", "CREATE TRACE EVENT NOTIFICATION"),
                            new SqlPermission("SERVER", "ALTER ANY EVENT NOTIFICATION"),
                            new SqlPermission("SERVER", "ALTER SERVER STATE"),
                            new SqlPermission("SERVER", "UNSAFE ASSEMBLY"),
                            new SqlPermission("SERVER", "ALTER ANY SERVER AUDIT"),
                            new SqlPermission("SERVER", "CREATE SERVER ROLE"),
                            new SqlPermission("SERVER", "ALTER ANY SERVER ROLE"),
                            new SqlPermission("SERVER", "ALTER ANY EVENT SESSION"),
                            new SqlPermission("SERVER", "CONTROL SERVER"),
                        }
                },
                {
                    21318,
                    new[]
                        {
                            new SqlPermission("OBJECT", "SELECT"), new SqlPermission("OBJECT", "UPDATE"),
                            new SqlPermission("OBJECT", "REFERENCES"), new SqlPermission("OBJECT", "INSERT"),
                            new SqlPermission("OBJECT", "DELETE"), new SqlPermission("OBJECT", "EXECUTE"),
                            new SqlPermission("OBJECT", "RECEIVE"), new SqlPermission("OBJECT", "VIEW DEFINITION"),
                            new SqlPermission("OBJECT", "ALTER"), new SqlPermission("OBJECT", "TAKE OWNERSHIP"),
                            new SqlPermission("OBJECT", "CONTROL"), new SqlPermission("OBJECT", "VIEW CHANGE TRACKING"),
                        }
                },
                {
                    21586,
                    new[]
                        {
                            new SqlPermission("ROUTE", "VIEW DEFINITION"), new SqlPermission("ROUTE", "ALTER"),
                            new SqlPermission("ROUTE", "TAKE OWNERSHIP"), new SqlPermission("ROUTE", "CONTROL"),
                        }
                },
                {
                    22604,
                    new[]
                        {
                            new SqlPermission("LOGIN", "IMPERSONATE"), new SqlPermission("LOGIN", "VIEW DEFINITION"),
                            new SqlPermission("LOGIN", "ALTER"), new SqlPermission("LOGIN", "CONTROL"),
                        }
                },
                {
                    8272,
                    new[]
                        {
                            new SqlPermission("OBJECT", "SELECT"), new SqlPermission("OBJECT", "UPDATE"),
                            new SqlPermission("OBJECT", "REFERENCES"), new SqlPermission("OBJECT", "INSERT"),
                            new SqlPermission("OBJECT", "DELETE"), new SqlPermission("OBJECT", "EXECUTE"),
                            new SqlPermission("OBJECT", "RECEIVE"), new SqlPermission("OBJECT", "VIEW DEFINITION"),
                            new SqlPermission("OBJECT", "ALTER"), new SqlPermission("OBJECT", "TAKE OWNERSHIP"),
                            new SqlPermission("OBJECT", "CONTROL"), new SqlPermission("OBJECT", "VIEW CHANGE TRACKING"),
                        }
                },
                {
                    19283,
                    new[]
                        {
                            new SqlPermission("SYMMETRIC KEY", "REFERENCES"),
                            new SqlPermission("SYMMETRIC KEY", "VIEW DEFINITION"),
                            new SqlPermission("SYMMETRIC KEY", "ALTER"),
                            new SqlPermission("SYMMETRIC KEY", "TAKE OWNERSHIP"),
                            new SqlPermission("SYMMETRIC KEY", "CONTROL"),
                        }
                },
                {
                    20038,
                    new[]
                        {
                            new SqlPermission("OBJECT", "SELECT"), new SqlPermission("OBJECT", "UPDATE"),
                            new SqlPermission("OBJECT", "REFERENCES"), new SqlPermission("OBJECT", "INSERT"),
                            new SqlPermission("OBJECT", "DELETE"), new SqlPermission("OBJECT", "EXECUTE"),
                            new SqlPermission("OBJECT", "RECEIVE"), new SqlPermission("OBJECT", "VIEW DEFINITION"),
                            new SqlPermission("OBJECT", "ALTER"), new SqlPermission("OBJECT", "TAKE OWNERSHIP"),
                            new SqlPermission("OBJECT", "CONTROL"), new SqlPermission("OBJECT", "VIEW CHANGE TRACKING"),
                        }
                },
                {
                    20051,
                    new[]
                        {
                            new SqlPermission("OBJECT", "SELECT"), new SqlPermission("OBJECT", "UPDATE"),
                            new SqlPermission("OBJECT", "REFERENCES"), new SqlPermission("OBJECT", "INSERT"),
                            new SqlPermission("OBJECT", "DELETE"), new SqlPermission("OBJECT", "EXECUTE"),
                            new SqlPermission("OBJECT", "RECEIVE"), new SqlPermission("OBJECT", "VIEW DEFINITION"),
                            new SqlPermission("OBJECT", "ALTER"), new SqlPermission("OBJECT", "TAKE OWNERSHIP"),
                            new SqlPermission("OBJECT", "CONTROL"), new SqlPermission("OBJECT", "VIEW CHANGE TRACKING"),
                        }
                },
                {
                    21057,
                    new[]
                        {
                            new SqlPermission("APPLICATION ROLE", "VIEW DEFINITION"),
                            new SqlPermission("APPLICATION ROLE", "ALTER"),
                            new SqlPermission("APPLICATION ROLE", "CONTROL"),
                        }
                },
                {
                    21321,
                    new[]
                        {
                            new SqlPermission("OBJECT", "SELECT"), new SqlPermission("OBJECT", "UPDATE"),
                            new SqlPermission("OBJECT", "REFERENCES"), new SqlPermission("OBJECT", "INSERT"),
                            new SqlPermission("OBJECT", "DELETE"), new SqlPermission("OBJECT", "EXECUTE"),
                            new SqlPermission("OBJECT", "RECEIVE"), new SqlPermission("OBJECT", "VIEW DEFINITION"),
                            new SqlPermission("OBJECT", "ALTER"), new SqlPermission("OBJECT", "TAKE OWNERSHIP"),
                            new SqlPermission("OBJECT", "CONTROL"), new SqlPermission("OBJECT", "VIEW CHANGE TRACKING"),
                        }
                },
                {
                    21574,
                    new[]
                        {
                            new SqlPermission("OBJECT", "SELECT"), new SqlPermission("OBJECT", "UPDATE"),
                            new SqlPermission("OBJECT", "REFERENCES"), new SqlPermission("OBJECT", "INSERT"),
                            new SqlPermission("OBJECT", "DELETE"), new SqlPermission("OBJECT", "EXECUTE"),
                            new SqlPermission("OBJECT", "RECEIVE"), new SqlPermission("OBJECT", "VIEW DEFINITION"),
                            new SqlPermission("OBJECT", "ALTER"), new SqlPermission("OBJECT", "TAKE OWNERSHIP"),
                            new SqlPermission("OBJECT", "CONTROL"), new SqlPermission("OBJECT", "VIEW CHANGE TRACKING"),
                        }
                },
                {
                    22611,
                    new[]
                        {
                            new SqlPermission("XML SCHEMA COLLECTION", "REFERENCES"),
                            new SqlPermission("XML SCHEMA COLLECTION", "EXECUTE"),
                            new SqlPermission("XML SCHEMA COLLECTION", "VIEW DEFINITION"),
                            new SqlPermission("XML SCHEMA COLLECTION", "ALTER"),
                            new SqlPermission("XML SCHEMA COLLECTION", "TAKE OWNERSHIP"),
                            new SqlPermission("XML SCHEMA COLLECTION", "CONTROL"),
                        }
                },
                {
                    8277,
                    new[]
                        {
                            new SqlPermission("OBJECT", "SELECT"), new SqlPermission("OBJECT", "UPDATE"),
                            new SqlPermission("OBJECT", "REFERENCES"), new SqlPermission("OBJECT", "INSERT"),
                            new SqlPermission("OBJECT", "DELETE"), new SqlPermission("OBJECT", "EXECUTE"),
                            new SqlPermission("OBJECT", "RECEIVE"), new SqlPermission("OBJECT", "VIEW DEFINITION"),
                            new SqlPermission("OBJECT", "ALTER"), new SqlPermission("OBJECT", "TAKE OWNERSHIP"),
                            new SqlPermission("OBJECT", "CONTROL"), new SqlPermission("OBJECT", "VIEW CHANGE TRACKING"),
                        }
                },
                {
                    8278,
                    new[]
                        {
                            new SqlPermission("OBJECT", "SELECT"), new SqlPermission("OBJECT", "UPDATE"),
                            new SqlPermission("OBJECT", "REFERENCES"), new SqlPermission("OBJECT", "INSERT"),
                            new SqlPermission("OBJECT", "DELETE"), new SqlPermission("OBJECT", "EXECUTE"),
                            new SqlPermission("OBJECT", "RECEIVE"), new SqlPermission("OBJECT", "VIEW DEFINITION"),
                            new SqlPermission("OBJECT", "ALTER"), new SqlPermission("OBJECT", "TAKE OWNERSHIP"),
                            new SqlPermission("OBJECT", "CONTROL"), new SqlPermission("OBJECT", "VIEW CHANGE TRACKING"),
                        }
                },
                {
                    8280,
                    new[]
                        {
                            new SqlPermission("OBJECT", "SELECT"), new SqlPermission("OBJECT", "UPDATE"),
                            new SqlPermission("OBJECT", "REFERENCES"), new SqlPermission("OBJECT", "INSERT"),
                            new SqlPermission("OBJECT", "DELETE"), new SqlPermission("OBJECT", "EXECUTE"),
                            new SqlPermission("OBJECT", "RECEIVE"), new SqlPermission("OBJECT", "VIEW DEFINITION"),
                            new SqlPermission("OBJECT", "ALTER"), new SqlPermission("OBJECT", "TAKE OWNERSHIP"),
                            new SqlPermission("OBJECT", "CONTROL"), new SqlPermission("OBJECT", "VIEW CHANGE TRACKING"),
                        }
                },
                {
                    17222,
                    new[]
                        {
                            new SqlPermission("FULLTEXT CATALOG", "REFERENCES"),
                            new SqlPermission("FULLTEXT CATALOG", "VIEW DEFINITION"),
                            new SqlPermission("FULLTEXT CATALOG", "ALTER"),
                            new SqlPermission("FULLTEXT CATALOG", "TAKE OWNERSHIP"),
                            new SqlPermission("FULLTEXT CATALOG", "CONTROL"),
                        }
                },
                {
                    17235,
                    new[]
                        {
                            new SqlPermission("SCHEMA", "SELECT"), new SqlPermission("SCHEMA", "INSERT"),
                            new SqlPermission("SCHEMA", "UPDATE"), new SqlPermission("SCHEMA", "DELETE"),
                            new SqlPermission("SCHEMA", "REFERENCES"), new SqlPermission("SCHEMA", "EXECUTE"),
                            new SqlPermission("SCHEMA", "VIEW DEFINITION"),
                            new SqlPermission("SCHEMA", "CREATE SEQUENCE"), new SqlPermission("SCHEMA", "ALTER"),
                            new SqlPermission("SCHEMA", "TAKE OWNERSHIP"), new SqlPermission("SCHEMA", "CONTROL"),
                            new SqlPermission("SCHEMA", "VIEW CHANGE TRACKING"),
                        }
                },
                {
                    18004,
                    new[]
                        {
                            new SqlPermission("OBJECT", "SELECT"), new SqlPermission("OBJECT", "UPDATE"),
                            new SqlPermission("OBJECT", "REFERENCES"), new SqlPermission("OBJECT", "INSERT"),
                            new SqlPermission("OBJECT", "DELETE"), new SqlPermission("OBJECT", "EXECUTE"),
                            new SqlPermission("OBJECT", "RECEIVE"), new SqlPermission("OBJECT", "VIEW DEFINITION"),
                            new SqlPermission("OBJECT", "ALTER"), new SqlPermission("OBJECT", "TAKE OWNERSHIP"),
                            new SqlPermission("OBJECT", "CONTROL"), new SqlPermission("OBJECT", "VIEW CHANGE TRACKING"),
                        }
                },
                {
                    19526,
                    new[]
                        {
                            new SqlPermission("FULLTEXT STOPLIST", "REFERENCES"),
                            new SqlPermission("FULLTEXT STOPLIST", "VIEW DEFINITION"),
                            new SqlPermission("FULLTEXT STOPLIST", "ALTER"),
                            new SqlPermission("FULLTEXT STOPLIST", "TAKE OWNERSHIP"),
                            new SqlPermission("FULLTEXT STOPLIST", "CONTROL"),
                        }
                },
                {
                    21313,
                    new[]
                        {
                            new SqlPermission("ASSEMBLY", "REFERENCES"), new SqlPermission("ASSEMBLY", "VIEW DEFINITION"),
                            new SqlPermission("ASSEMBLY", "ALTER"), new SqlPermission("ASSEMBLY", "TAKE OWNERSHIP"),
                            new SqlPermission("ASSEMBLY", "CONTROL"),
                        }
                },
                {
                    21333,
                    new[]
                        {
                            new SqlPermission("USER", "IMPERSONATE"), new SqlPermission("USER", "VIEW DEFINITION"),
                            new SqlPermission("USER", "ALTER"), new SqlPermission("USER", "CONTROL"),
                        }
                },
                {
                    21581,
                    new[]
                        {
                            new SqlPermission("MESSAGE TYPE", "REFERENCES"),
                            new SqlPermission("MESSAGE TYPE", "VIEW DEFINITION"),
                            new SqlPermission("MESSAGE TYPE", "ALTER"),
                            new SqlPermission("MESSAGE TYPE", "TAKE OWNERSHIP"),
                            new SqlPermission("MESSAGE TYPE", "CONTROL"),
                        }
                },
                {
                    22099,
                    new[]
                        {
                            new SqlPermission("SERVICE", "SEND"), new SqlPermission("SERVICE", "VIEW DEFINITION"),
                            new SqlPermission("SERVICE", "ALTER"), new SqlPermission("SERVICE", "TAKE OWNERSHIP"),
                            new SqlPermission("SERVICE", "CONTROL"),
                        }
                },
                {
                    22868,
                    new[]
                        {
                            new SqlPermission("TYPE", "REFERENCES"), new SqlPermission("TYPE", "EXECUTE"),
                            new SqlPermission("TYPE", "VIEW DEFINITION"), new SqlPermission("TYPE", "TAKE OWNERSHIP"),
                            new SqlPermission("TYPE", "CONTROL"),
                        }
                },
                {
                    8260,
                    new[]
                        {
                            new SqlPermission("OBJECT", "SELECT"), new SqlPermission("OBJECT", "UPDATE"),
                            new SqlPermission("OBJECT", "REFERENCES"), new SqlPermission("OBJECT", "INSERT"),
                            new SqlPermission("OBJECT", "DELETE"), new SqlPermission("OBJECT", "EXECUTE"),
                            new SqlPermission("OBJECT", "RECEIVE"), new SqlPermission("OBJECT", "VIEW DEFINITION"),
                            new SqlPermission("OBJECT", "ALTER"), new SqlPermission("OBJECT", "TAKE OWNERSHIP"),
                            new SqlPermission("OBJECT", "CONTROL"), new SqlPermission("OBJECT", "VIEW CHANGE TRACKING"),
                        }
                },
                {
                    8274,
                    new[]
                        {
                            new SqlPermission("OBJECT", "SELECT"), new SqlPermission("OBJECT", "UPDATE"),
                            new SqlPermission("OBJECT", "REFERENCES"), new SqlPermission("OBJECT", "INSERT"),
                            new SqlPermission("OBJECT", "DELETE"), new SqlPermission("OBJECT", "EXECUTE"),
                            new SqlPermission("OBJECT", "RECEIVE"), new SqlPermission("OBJECT", "VIEW DEFINITION"),
                            new SqlPermission("OBJECT", "ALTER"), new SqlPermission("OBJECT", "TAKE OWNERSHIP"),
                            new SqlPermission("OBJECT", "CONTROL"), new SqlPermission("OBJECT", "VIEW CHANGE TRACKING"),
                        }
                },
                {
                    16964,
                    new[]
                        {
                            new SqlPermission("DATABASE", "CREATE DATABASE"), new SqlPermission("DATABASE", "CREATE TABLE")
                            , new SqlPermission("DATABASE", "CREATE PROCEDURE"),
                            new SqlPermission("DATABASE", "CREATE VIEW"), new SqlPermission("DATABASE", "CREATE RULE"),
                            new SqlPermission("DATABASE", "CREATE DEFAULT"),
                            new SqlPermission("DATABASE", "BACKUP DATABASE"),
                            new SqlPermission("DATABASE", "BACKUP LOG"), new SqlPermission("DATABASE", "CONNECT"),
                            new SqlPermission("DATABASE", "CREATE FUNCTION"),
                            new SqlPermission("DATABASE", "CREATE TYPE"),
                            new SqlPermission("DATABASE", "CREATE ASSEMBLY"),
                            new SqlPermission("DATABASE", "CREATE XML SCHEMA COLLECTION"),
                            new SqlPermission("DATABASE", "CREATE SCHEMA"),
                            new SqlPermission("DATABASE", "CREATE SYNONYM"),
                            new SqlPermission("DATABASE", "CREATE AGGREGATE"),
                            new SqlPermission("DATABASE", "CREATE ROLE"),
                            new SqlPermission("DATABASE", "CREATE MESSAGE TYPE"),
                            new SqlPermission("DATABASE", "CREATE SERVICE"),
                            new SqlPermission("DATABASE", "CREATE CONTRACT"),
                            new SqlPermission("DATABASE", "CREATE REMOTE SERVICE BINDING"),
                            new SqlPermission("DATABASE", "CREATE ROUTE"), new SqlPermission("DATABASE", "CREATE QUEUE")
                            , new SqlPermission("DATABASE", "CREATE SYMMETRIC KEY"),
                            new SqlPermission("DATABASE", "CREATE ASYMMETRIC KEY"),
                            new SqlPermission("DATABASE", "CREATE FULLTEXT CATALOG"),
                            new SqlPermission("DATABASE", "CREATE CERTIFICATE"),
                            new SqlPermission("DATABASE", "CREATE DATABASE DDL EVENT NOTIFICATION"),
                            new SqlPermission("DATABASE", "CONNECT REPLICATION"),
                            new SqlPermission("DATABASE", "CHECKPOINT"),
                            new SqlPermission("DATABASE", "SUBSCRIBE QUERY NOTIFICATIONS"),
                            new SqlPermission("DATABASE", "AUTHENTICATE"), new SqlPermission("DATABASE", "SHOWPLAN"),
                            new SqlPermission("DATABASE", "ALTER ANY USER"),
                            new SqlPermission("DATABASE", "ALTER ANY ROLE"),
                            new SqlPermission("DATABASE", "ALTER ANY APPLICATION ROLE"),
                            new SqlPermission("DATABASE", "ALTER ANY SCHEMA"),
                            new SqlPermission("DATABASE", "ALTER ANY ASSEMBLY"),
                            new SqlPermission("DATABASE", "VIEW DATABASE STATE"),
                            new SqlPermission("DATABASE", "ALTER ANY DATASPACE"),
                            new SqlPermission("DATABASE", "ALTER ANY MESSAGE TYPE"),
                            new SqlPermission("DATABASE", "ALTER ANY CONTRACT"),
                            new SqlPermission("DATABASE", "ALTER ANY SERVICE"),
                            new SqlPermission("DATABASE", "ALTER ANY REMOTE SERVICE BINDING"),
                            new SqlPermission("DATABASE", "ALTER ANY ROUTE"),
                            new SqlPermission("DATABASE", "ALTER ANY FULLTEXT CATALOG"),
                            new SqlPermission("DATABASE", "ALTER ANY SYMMETRIC KEY"),
                            new SqlPermission("DATABASE", "ALTER ANY ASYMMETRIC KEY"),
                            new SqlPermission("DATABASE", "ALTER ANY CERTIFICATE"),
                            new SqlPermission("DATABASE", "SELECT"), new SqlPermission("DATABASE", "INSERT"),
                            new SqlPermission("DATABASE", "UPDATE"), new SqlPermission("DATABASE", "DELETE"),
                            new SqlPermission("DATABASE", "REFERENCES"), new SqlPermission("DATABASE", "EXECUTE"),
                            new SqlPermission("DATABASE", "ALTER ANY DATABASE DDL TRIGGER"),
                            new SqlPermission("DATABASE", "ALTER ANY DATABASE EVENT NOTIFICATION"),
                            new SqlPermission("DATABASE", "ALTER ANY DATABASE AUDIT"),
                            new SqlPermission("DATABASE", "VIEW DEFINITION"),
                            new SqlPermission("DATABASE", "TAKE OWNERSHIP"), new SqlPermission("DATABASE", "ALTER"),
                            new SqlPermission("DATABASE", "CONTROL"),
                        }
                },
                {
                    16975,
                    new[]
                        {
                            new SqlPermission("OBJECT", "SELECT"), new SqlPermission("OBJECT", "UPDATE"),
                            new SqlPermission("OBJECT", "REFERENCES"), new SqlPermission("OBJECT", "INSERT"),
                            new SqlPermission("OBJECT", "DELETE"), new SqlPermission("OBJECT", "EXECUTE"),
                            new SqlPermission("OBJECT", "RECEIVE"), new SqlPermission("OBJECT", "VIEW DEFINITION"),
                            new SqlPermission("OBJECT", "ALTER"), new SqlPermission("OBJECT", "TAKE OWNERSHIP"),
                            new SqlPermission("OBJECT", "CONTROL"), new SqlPermission("OBJECT", "VIEW CHANGE TRACKING"),
                        }
                },
                {
                    17232,
                    new[]
                        {
                            new SqlPermission("OBJECT", "SELECT"), new SqlPermission("OBJECT", "UPDATE"),
                            new SqlPermission("OBJECT", "REFERENCES"), new SqlPermission("OBJECT", "INSERT"),
                            new SqlPermission("OBJECT", "DELETE"), new SqlPermission("OBJECT", "EXECUTE"),
                            new SqlPermission("OBJECT", "RECEIVE"), new SqlPermission("OBJECT", "VIEW DEFINITION"),
                            new SqlPermission("OBJECT", "ALTER"), new SqlPermission("OBJECT", "TAKE OWNERSHIP"),
                            new SqlPermission("OBJECT", "CONTROL"), new SqlPermission("OBJECT", "VIEW CHANGE TRACKING"),
                        }
                },
                {
                    17993,
                    new[]
                        {
                            new SqlPermission("OBJECT", "SELECT"), new SqlPermission("OBJECT", "UPDATE"),
                            new SqlPermission("OBJECT", "REFERENCES"), new SqlPermission("OBJECT", "INSERT"),
                            new SqlPermission("OBJECT", "DELETE"), new SqlPermission("OBJECT", "EXECUTE"),
                            new SqlPermission("OBJECT", "RECEIVE"), new SqlPermission("OBJECT", "VIEW DEFINITION"),
                            new SqlPermission("OBJECT", "ALTER"), new SqlPermission("OBJECT", "TAKE OWNERSHIP"),
                            new SqlPermission("OBJECT", "CONTROL"), new SqlPermission("OBJECT", "VIEW CHANGE TRACKING"),
                        }
                },
                {
                    18002,
                    new[]
                        {
                            new SqlPermission("OBJECT", "SELECT"), new SqlPermission("OBJECT", "UPDATE"),
                            new SqlPermission("OBJECT", "REFERENCES"), new SqlPermission("OBJECT", "INSERT"),
                            new SqlPermission("OBJECT", "DELETE"), new SqlPermission("OBJECT", "EXECUTE"),
                            new SqlPermission("OBJECT", "RECEIVE"), new SqlPermission("OBJECT", "VIEW DEFINITION"),
                            new SqlPermission("OBJECT", "ALTER"), new SqlPermission("OBJECT", "TAKE OWNERSHIP"),
                            new SqlPermission("OBJECT", "CONTROL"), new SqlPermission("OBJECT", "VIEW CHANGE TRACKING"),
                        }
                },
                {
                    18259,
                    new[]
                        {
                            new SqlPermission("SERVER ROLE", "VIEW DEFINITION"), new SqlPermission("SERVER ROLE", "ALTER"),
                            new SqlPermission("SERVER ROLE", "TAKE OWNERSHIP"),
                            new SqlPermission("SERVER ROLE", "CONTROL"),
                        }
                },
                {
                    19265,
                    new[]
                        {
                            new SqlPermission("ASYMMETRIC KEY", "REFERENCES"),
                            new SqlPermission("ASYMMETRIC KEY", "VIEW DEFINITION"),
                            new SqlPermission("ASYMMETRIC KEY", "ALTER"),
                            new SqlPermission("ASYMMETRIC KEY", "TAKE OWNERSHIP"),
                            new SqlPermission("ASYMMETRIC KEY", "CONTROL"),
                        }
                },
                {
                    19538,
                    new[]
                        {
                            new SqlPermission("ROLE", "VIEW DEFINITION"), new SqlPermission("ROLE", "ALTER"),
                            new SqlPermission("ROLE", "TAKE OWNERSHIP"), new SqlPermission("ROLE", "CONTROL"),
                        }
                },
                {
                    20549,
                    new[]
                        {
                            new SqlPermission("ENDPOINT", "CONNECT"), new SqlPermission("ENDPOINT", "VIEW DEFINITION"),
                            new SqlPermission("ENDPOINT", "ALTER"), new SqlPermission("ENDPOINT", "TAKE OWNERSHIP"),
                            new SqlPermission("ENDPOINT", "CONTROL"),
                        }
                },
                {
                    20819,
                    new[]
                        {
                            new SqlPermission("OBJECT", "SELECT"), new SqlPermission("OBJECT", "UPDATE"),
                            new SqlPermission("OBJECT", "REFERENCES"), new SqlPermission("OBJECT", "INSERT"),
                            new SqlPermission("OBJECT", "DELETE"), new SqlPermission("OBJECT", "EXECUTE"),
                            new SqlPermission("OBJECT", "RECEIVE"), new SqlPermission("OBJECT", "VIEW DEFINITION"),
                            new SqlPermission("OBJECT", "ALTER"), new SqlPermission("OBJECT", "TAKE OWNERSHIP"),
                            new SqlPermission("OBJECT", "CONTROL"), new SqlPermission("OBJECT", "VIEW CHANGE TRACKING"),
                        }
                },
                {
                    21571,
                    new[]
                        {
                            new SqlPermission("CONTRACT", "REFERENCES"), new SqlPermission("CONTRACT", "VIEW DEFINITION"),
                            new SqlPermission("CONTRACT", "ALTER"), new SqlPermission("CONTRACT", "TAKE OWNERSHIP"),
                            new SqlPermission("CONTRACT", "CONTROL"),
                        }
                },
            };

        #endregion

        #region _map dict

        public static readonly IDictionary<Tuple<int, long>, SqlPermission> _map = new SortedDictionary<Tuple<int, long>, SqlPermission>
            {
    			{ Tuple.Create(21057, 2L), new SqlPermission("APPLICATION ROLE", "VIEW DEFINITION") }, // APPLICATION ROLE
				{ Tuple.Create(21057, 8L), new SqlPermission("APPLICATION ROLE", "ALTER") }, // APPLICATION ROLE
				{ Tuple.Create(21057, 32L), new SqlPermission("APPLICATION ROLE", "CONTROL") }, // APPLICATION ROLE
				{ Tuple.Create(21313, 1L), new SqlPermission("ASSEMBLY", "REFERENCES") }, // ASSEMBLY
				{ Tuple.Create(21313, 4L), new SqlPermission("ASSEMBLY", "VIEW DEFINITION") }, // ASSEMBLY
				{ Tuple.Create(21313, 16L), new SqlPermission("ASSEMBLY", "ALTER") }, // ASSEMBLY
				{ Tuple.Create(21313, 32L), new SqlPermission("ASSEMBLY", "TAKE OWNERSHIP") }, // ASSEMBLY
				{ Tuple.Create(21313, 128L), new SqlPermission("ASSEMBLY", "CONTROL") }, // ASSEMBLY
				{ Tuple.Create(19265, 1L), new SqlPermission("ASYMMETRIC KEY", "REFERENCES") }, // ASYMMETRIC KEY
				{ Tuple.Create(19265, 2L), new SqlPermission("ASYMMETRIC KEY", "VIEW DEFINITION") }, // ASYMMETRIC KEY
				{ Tuple.Create(19265, 8L), new SqlPermission("ASYMMETRIC KEY", "ALTER") }, // ASYMMETRIC KEY
				{ Tuple.Create(19265, 16L), new SqlPermission("ASYMMETRIC KEY", "TAKE OWNERSHIP") }, // ASYMMETRIC KEY
				{ Tuple.Create(19265, 64L), new SqlPermission("ASYMMETRIC KEY", "CONTROL") }, // ASYMMETRIC KEY
				{ Tuple.Create(21059, 1L), new SqlPermission("CERTIFICATE", "VIEW DEFINITION") }, // CERTIFICATE
				{ Tuple.Create(21059, 4L), new SqlPermission("CERTIFICATE", "REFERENCES") }, // CERTIFICATE
				{ Tuple.Create(21059, 8L), new SqlPermission("CERTIFICATE", "ALTER") }, // CERTIFICATE
				{ Tuple.Create(21059, 16L), new SqlPermission("CERTIFICATE", "TAKE OWNERSHIP") }, // CERTIFICATE
				{ Tuple.Create(21059, 64L), new SqlPermission("CERTIFICATE", "CONTROL") }, // CERTIFICATE
				{ Tuple.Create(21571, 1L), new SqlPermission("CONTRACT", "REFERENCES") }, // CONTRACT
				{ Tuple.Create(21571, 2L), new SqlPermission("CONTRACT", "VIEW DEFINITION") }, // CONTRACT
				{ Tuple.Create(21571, 8L), new SqlPermission("CONTRACT", "ALTER") }, // CONTRACT
				{ Tuple.Create(21571, 16L), new SqlPermission("CONTRACT", "TAKE OWNERSHIP") }, // CONTRACT
				{ Tuple.Create(21571, 64L), new SqlPermission("CONTRACT", "CONTROL") }, // CONTRACT
				{ Tuple.Create(16964, 1L), new SqlPermission("DATABASE", "CREATE DATABASE") }, // DATABASE
				{ Tuple.Create(16964, 2L), new SqlPermission("DATABASE", "CREATE TABLE") }, // DATABASE
				{ Tuple.Create(16964, 4L), new SqlPermission("DATABASE", "CREATE PROCEDURE") }, // DATABASE
				{ Tuple.Create(16964, 8L), new SqlPermission("DATABASE", "CREATE VIEW") }, // DATABASE
				{ Tuple.Create(16964, 16L), new SqlPermission("DATABASE", "CREATE RULE") }, // DATABASE
				{ Tuple.Create(16964, 32L), new SqlPermission("DATABASE", "CREATE DEFAULT") }, // DATABASE
				{ Tuple.Create(16964, 64L), new SqlPermission("DATABASE", "BACKUP DATABASE") }, // DATABASE
				{ Tuple.Create(16964, 128L), new SqlPermission("DATABASE", "BACKUP LOG") }, // DATABASE
				{ Tuple.Create(16964, 256L), new SqlPermission("DATABASE", "CONNECT") }, // DATABASE
				{ Tuple.Create(16964, 512L), new SqlPermission("DATABASE", "CREATE FUNCTION") }, // DATABASE
				{ Tuple.Create(16964, 1024L), new SqlPermission("DATABASE", "CREATE TYPE") }, // DATABASE
				{ Tuple.Create(16964, 2048L), new SqlPermission("DATABASE", "CREATE ASSEMBLY") }, // DATABASE
				{ Tuple.Create(16964, 4096L), new SqlPermission("DATABASE", "CREATE XML SCHEMA COLLECTION") }, // DATABASE
				{ Tuple.Create(16964, 8192L), new SqlPermission("DATABASE", "CREATE SCHEMA") }, // DATABASE
				{ Tuple.Create(16964, 16384L), new SqlPermission("DATABASE", "CREATE SYNONYM") }, // DATABASE
				{ Tuple.Create(16964, 32768L), new SqlPermission("DATABASE", "CREATE AGGREGATE") }, // DATABASE
				{ Tuple.Create(16964, 65536L), new SqlPermission("DATABASE", "CREATE ROLE") }, // DATABASE
				{ Tuple.Create(16964, 131072L), new SqlPermission("DATABASE", "CREATE MESSAGE TYPE") }, // DATABASE
				{ Tuple.Create(16964, 262144L), new SqlPermission("DATABASE", "CREATE SERVICE") }, // DATABASE
				{ Tuple.Create(16964, 524288L), new SqlPermission("DATABASE", "CREATE CONTRACT") }, // DATABASE
				{ Tuple.Create(16964, 1048576L), new SqlPermission("DATABASE", "CREATE REMOTE SERVICE BINDING") }, // DATABASE
				{ Tuple.Create(16964, 2097152L), new SqlPermission("DATABASE", "CREATE ROUTE") }, // DATABASE
				{ Tuple.Create(16964, 4194304L), new SqlPermission("DATABASE", "CREATE QUEUE") }, // DATABASE
				{ Tuple.Create(16964, 8388608L), new SqlPermission("DATABASE", "CREATE SYMMETRIC KEY") }, // DATABASE
				{ Tuple.Create(16964, 16777216L), new SqlPermission("DATABASE", "CREATE ASYMMETRIC KEY") }, // DATABASE
				{ Tuple.Create(16964, 33554432L), new SqlPermission("DATABASE", "CREATE FULLTEXT CATALOG") }, // DATABASE
				{ Tuple.Create(16964, 67108864L), new SqlPermission("DATABASE", "CREATE CERTIFICATE") }, // DATABASE
				{ Tuple.Create(16964, 134217728L), new SqlPermission("DATABASE", "CREATE DATABASE DDL EVENT NOTIFICATION") }, // DATABASE
				{ Tuple.Create(16964, 268435456L), new SqlPermission("DATABASE", "CONNECT REPLICATION") }, // DATABASE
				{ Tuple.Create(16964, 536870912L), new SqlPermission("DATABASE", "CHECKPOINT") }, // DATABASE
				{ Tuple.Create(16964, 1073741824L), new SqlPermission("DATABASE", "SUBSCRIBE QUERY NOTIFICATIONS") }, // DATABASE
				{ Tuple.Create(16964, 2147483648L), new SqlPermission("DATABASE", "AUTHENTICATE") }, // DATABASE
				{ Tuple.Create(16964, 4294967296L), new SqlPermission("DATABASE", "SHOWPLAN") }, // DATABASE
				{ Tuple.Create(16964, 8589934592L), new SqlPermission("DATABASE", "ALTER ANY USER") }, // DATABASE
				{ Tuple.Create(16964, 17179869184L), new SqlPermission("DATABASE", "ALTER ANY ROLE") }, // DATABASE
				{ Tuple.Create(16964, 34359738368L), new SqlPermission("DATABASE", "ALTER ANY APPLICATION ROLE") }, // DATABASE
				{ Tuple.Create(16964, 68719476736L), new SqlPermission("DATABASE", "ALTER ANY SCHEMA") }, // DATABASE
				{ Tuple.Create(16964, 137438953472L), new SqlPermission("DATABASE", "ALTER ANY ASSEMBLY") }, // DATABASE
				{ Tuple.Create(16964, 274877906944L), new SqlPermission("DATABASE", "VIEW DATABASE STATE") }, // DATABASE
				{ Tuple.Create(16964, 549755813888L), new SqlPermission("DATABASE", "ALTER ANY DATASPACE") }, // DATABASE
				{ Tuple.Create(16964, 1099511627776L), new SqlPermission("DATABASE", "ALTER ANY MESSAGE TYPE") }, // DATABASE
				{ Tuple.Create(16964, 2199023255552L), new SqlPermission("DATABASE", "ALTER ANY CONTRACT") }, // DATABASE
				{ Tuple.Create(16964, 4398046511104L), new SqlPermission("DATABASE", "ALTER ANY SERVICE") }, // DATABASE
				{ Tuple.Create(16964, 8796093022208L), new SqlPermission("DATABASE", "ALTER ANY REMOTE SERVICE BINDING") }, // DATABASE
				{ Tuple.Create(16964, 17592186044416L), new SqlPermission("DATABASE", "ALTER ANY ROUTE") }, // DATABASE
				{ Tuple.Create(16964, 35184372088832L), new SqlPermission("DATABASE", "ALTER ANY FULLTEXT CATALOG") }, // DATABASE
				{ Tuple.Create(16964, 70368744177664L), new SqlPermission("DATABASE", "ALTER ANY SYMMETRIC KEY") }, // DATABASE
				{ Tuple.Create(16964, 140737488355328L), new SqlPermission("DATABASE", "ALTER ANY ASYMMETRIC KEY") }, // DATABASE
				{ Tuple.Create(16964, 281474976710656L), new SqlPermission("DATABASE", "ALTER ANY CERTIFICATE") }, // DATABASE
				{ Tuple.Create(16964, 562949953421312L), new SqlPermission("DATABASE", "SELECT") }, // DATABASE
				{ Tuple.Create(16964, 1125899906842624L), new SqlPermission("DATABASE", "INSERT") }, // DATABASE
				{ Tuple.Create(16964, 2251799813685248L), new SqlPermission("DATABASE", "UPDATE") }, // DATABASE
				{ Tuple.Create(16964, 4503599627370496L), new SqlPermission("DATABASE", "DELETE") }, // DATABASE
				{ Tuple.Create(16964, 9007199254740992L), new SqlPermission("DATABASE", "REFERENCES") }, // DATABASE
				{ Tuple.Create(16964, 18014398509481984L), new SqlPermission("DATABASE", "EXECUTE") }, // DATABASE
				{ Tuple.Create(16964, 36028797018963968L), new SqlPermission("DATABASE", "ALTER ANY DATABASE DDL TRIGGER") }, // DATABASE
				{ Tuple.Create(16964, 72057594037927936L), new SqlPermission("DATABASE", "ALTER ANY DATABASE EVENT NOTIFICATION") }, // DATABASE
				{ Tuple.Create(16964, 144115188075855872L), new SqlPermission("DATABASE", "ALTER ANY DATABASE AUDIT") }, // DATABASE
				{ Tuple.Create(16964, 288230376151711744L), new SqlPermission("DATABASE", "VIEW DEFINITION") }, // DATABASE
				{ Tuple.Create(16964, 576460752303423488L), new SqlPermission("DATABASE", "TAKE OWNERSHIP") }, // DATABASE
				{ Tuple.Create(16964, 1152921504606846976L), new SqlPermission("DATABASE", "ALTER") }, // DATABASE
				{ Tuple.Create(16964, 4611686018427387904L), new SqlPermission("DATABASE", "CONTROL") }, // DATABASE
				{ Tuple.Create(20549, 1L), new SqlPermission("ENDPOINT", "CONNECT") }, // ENDPOINT
				{ Tuple.Create(20549, 2L), new SqlPermission("ENDPOINT", "VIEW DEFINITION") }, // ENDPOINT
				{ Tuple.Create(20549, 8L), new SqlPermission("ENDPOINT", "ALTER") }, // ENDPOINT
				{ Tuple.Create(20549, 16L), new SqlPermission("ENDPOINT", "TAKE OWNERSHIP") }, // ENDPOINT
				{ Tuple.Create(20549, 64L), new SqlPermission("ENDPOINT", "CONTROL") }, // ENDPOINT
				{ Tuple.Create(17222, 1L), new SqlPermission("FULLTEXT CATALOG", "REFERENCES") }, // FULLTEXT CATALOG
				{ Tuple.Create(17222, 2L), new SqlPermission("FULLTEXT CATALOG", "VIEW DEFINITION") }, // FULLTEXT CATALOG
				{ Tuple.Create(17222, 8L), new SqlPermission("FULLTEXT CATALOG", "ALTER") }, // FULLTEXT CATALOG
				{ Tuple.Create(17222, 16L), new SqlPermission("FULLTEXT CATALOG", "TAKE OWNERSHIP") }, // FULLTEXT CATALOG
				{ Tuple.Create(17222, 64L), new SqlPermission("FULLTEXT CATALOG", "CONTROL") }, // FULLTEXT CATALOG
				{ Tuple.Create(19526, 1L), new SqlPermission("FULLTEXT STOPLIST", "REFERENCES") }, // FULLTEXT STOPLIST
				{ Tuple.Create(19526, 2L), new SqlPermission("FULLTEXT STOPLIST", "VIEW DEFINITION") }, // FULLTEXT STOPLIST
				{ Tuple.Create(19526, 8L), new SqlPermission("FULLTEXT STOPLIST", "ALTER") }, // FULLTEXT STOPLIST
				{ Tuple.Create(19526, 16L), new SqlPermission("FULLTEXT STOPLIST", "TAKE OWNERSHIP") }, // FULLTEXT STOPLIST
				{ Tuple.Create(19526, 64L), new SqlPermission("FULLTEXT STOPLIST", "CONTROL") }, // FULLTEXT STOPLIST
				{ Tuple.Create(22604, 1L), new SqlPermission("LOGIN", "IMPERSONATE") }, // LOGIN
				{ Tuple.Create(22604, 2L), new SqlPermission("LOGIN", "VIEW DEFINITION") }, // LOGIN
				{ Tuple.Create(22604, 8L), new SqlPermission("LOGIN", "ALTER") }, // LOGIN
				{ Tuple.Create(22604, 64L), new SqlPermission("LOGIN", "CONTROL") }, // LOGIN
				{ Tuple.Create(21581, 1L), new SqlPermission("MESSAGE TYPE", "REFERENCES") }, // MESSAGE TYPE
				{ Tuple.Create(21581, 2L), new SqlPermission("MESSAGE TYPE", "VIEW DEFINITION") }, // MESSAGE TYPE
				{ Tuple.Create(21581, 8L), new SqlPermission("MESSAGE TYPE", "ALTER") }, // MESSAGE TYPE
				{ Tuple.Create(21581, 16L), new SqlPermission("MESSAGE TYPE", "TAKE OWNERSHIP") }, // MESSAGE TYPE
				{ Tuple.Create(21581, 64L), new SqlPermission("MESSAGE TYPE", "CONTROL") }, // MESSAGE TYPE
				{ Tuple.Create(8277, 1L), new SqlPermission("OBJECT", "SELECT") }, // TABLE
				{ Tuple.Create(8278, 1L), new SqlPermission("OBJECT", "SELECT") }, // VIEW
				{ Tuple.Create(8280, 1L), new SqlPermission("OBJECT", "SELECT") }, // STORED PROCEDURE EXTENDED
				{ Tuple.Create(16975, 1L), new SqlPermission("OBJECT", "SELECT") }, // OBJECT
				{ Tuple.Create(8272, 1L), new SqlPermission("OBJECT", "SELECT") }, // STORED PROCEDURE
				{ Tuple.Create(17232, 1L), new SqlPermission("OBJECT", "SELECT") }, // STORED PROCEDURE ASSEMBLY
				{ Tuple.Create(8274, 1L), new SqlPermission("OBJECT", "SELECT") }, // RULE
				{ Tuple.Create(18002, 1L), new SqlPermission("OBJECT", "SELECT") }, // STORED PROCEDURE REPLICATION FILTER
				{ Tuple.Create(20038, 1L), new SqlPermission("OBJECT", "SELECT") }, // FUNCTION SCALAR SQL
				{ Tuple.Create(21318, 1L), new SqlPermission("OBJECT", "SELECT") }, // FUNCTION SCALAR ASSEMBLY 
				{ Tuple.Create(21574, 1L), new SqlPermission("OBJECT", "SELECT") }, // FUNCTION TABLE-VALUED ASSEMBLY 
				{ Tuple.Create(17993, 1L), new SqlPermission("OBJECT", "SELECT") }, // FUNCTION TABLE-VALUED INLINE SQL
				{ Tuple.Create(21321, 1L), new SqlPermission("OBJECT", "SELECT") }, // FUNCTION SCALAR INLINE SQL 
				{ Tuple.Create(8260, 1L), new SqlPermission("OBJECT", "SELECT") }, // DEFAULT
				{ Tuple.Create(18004, 1L), new SqlPermission("OBJECT", "SELECT") }, // FUNCTION TABLE-VALUED SQL
				{ Tuple.Create(20051, 1L), new SqlPermission("OBJECT", "SELECT") }, // SYNONYM
				{ Tuple.Create(20819, 1L), new SqlPermission("OBJECT", "SELECT") }, // QUEUE
				{ Tuple.Create(17985, 1L), new SqlPermission("OBJECT", "SELECT") }, // AGGREGATE
				{ Tuple.Create(8275, 1L), new SqlPermission("OBJECT", "SELECT") }, // TABLE SYSTEM
				{ Tuple.Create(17985, 2L), new SqlPermission("OBJECT", "UPDATE") }, // AGGREGATE
				{ Tuple.Create(8260, 2L), new SqlPermission("OBJECT", "UPDATE") }, // DEFAULT
				{ Tuple.Create(20038, 2L), new SqlPermission("OBJECT", "UPDATE") }, // FUNCTION SCALAR SQL
				{ Tuple.Create(21318, 2L), new SqlPermission("OBJECT", "UPDATE") }, // FUNCTION SCALAR ASSEMBLY 
				{ Tuple.Create(21574, 2L), new SqlPermission("OBJECT", "UPDATE") }, // FUNCTION TABLE-VALUED ASSEMBLY 
				{ Tuple.Create(17993, 2L), new SqlPermission("OBJECT", "UPDATE") }, // FUNCTION TABLE-VALUED INLINE SQL
				{ Tuple.Create(21321, 2L), new SqlPermission("OBJECT", "UPDATE") }, // FUNCTION SCALAR INLINE SQL 
				{ Tuple.Create(16975, 2L), new SqlPermission("OBJECT", "UPDATE") }, // OBJECT
				{ Tuple.Create(8272, 2L), new SqlPermission("OBJECT", "UPDATE") }, // STORED PROCEDURE
				{ Tuple.Create(17232, 2L), new SqlPermission("OBJECT", "UPDATE") }, // STORED PROCEDURE ASSEMBLY
				{ Tuple.Create(8274, 2L), new SqlPermission("OBJECT", "UPDATE") }, // RULE
				{ Tuple.Create(18002, 2L), new SqlPermission("OBJECT", "UPDATE") }, // STORED PROCEDURE REPLICATION FILTER
				{ Tuple.Create(8275, 2L), new SqlPermission("OBJECT", "UPDATE") }, // TABLE SYSTEM
				{ Tuple.Create(20051, 2L), new SqlPermission("OBJECT", "UPDATE") }, // SYNONYM
				{ Tuple.Create(20819, 2L), new SqlPermission("OBJECT", "UPDATE") }, // QUEUE
				{ Tuple.Create(8278, 2L), new SqlPermission("OBJECT", "UPDATE") }, // VIEW
				{ Tuple.Create(8280, 2L), new SqlPermission("OBJECT", "UPDATE") }, // STORED PROCEDURE EXTENDED
				{ Tuple.Create(18004, 2L), new SqlPermission("OBJECT", "UPDATE") }, // FUNCTION TABLE-VALUED SQL
				{ Tuple.Create(8277, 2L), new SqlPermission("OBJECT", "UPDATE") }, // TABLE
				{ Tuple.Create(17985, 4L), new SqlPermission("OBJECT", "REFERENCES") }, // AGGREGATE
				{ Tuple.Create(20038, 4L), new SqlPermission("OBJECT", "REFERENCES") }, // FUNCTION SCALAR SQL
				{ Tuple.Create(21318, 4L), new SqlPermission("OBJECT", "REFERENCES") }, // FUNCTION SCALAR ASSEMBLY 
				{ Tuple.Create(21574, 4L), new SqlPermission("OBJECT", "REFERENCES") }, // FUNCTION TABLE-VALUED ASSEMBLY 
				{ Tuple.Create(17993, 4L), new SqlPermission("OBJECT", "REFERENCES") }, // FUNCTION TABLE-VALUED INLINE SQL
				{ Tuple.Create(21321, 4L), new SqlPermission("OBJECT", "REFERENCES") }, // FUNCTION SCALAR INLINE SQL 
				{ Tuple.Create(16975, 4L), new SqlPermission("OBJECT", "REFERENCES") }, // OBJECT
				{ Tuple.Create(8272, 4L), new SqlPermission("OBJECT", "REFERENCES") }, // STORED PROCEDURE
				{ Tuple.Create(17232, 4L), new SqlPermission("OBJECT", "REFERENCES") }, // STORED PROCEDURE ASSEMBLY
				{ Tuple.Create(8274, 4L), new SqlPermission("OBJECT", "REFERENCES") }, // RULE
				{ Tuple.Create(18002, 4L), new SqlPermission("OBJECT", "REFERENCES") }, // STORED PROCEDURE REPLICATION FILTER
				{ Tuple.Create(8260, 4L), new SqlPermission("OBJECT", "REFERENCES") }, // DEFAULT
				{ Tuple.Create(8275, 4L), new SqlPermission("OBJECT", "REFERENCES") }, // TABLE SYSTEM
				{ Tuple.Create(8278, 4L), new SqlPermission("OBJECT", "REFERENCES") }, // VIEW
				{ Tuple.Create(8280, 4L), new SqlPermission("OBJECT", "REFERENCES") }, // STORED PROCEDURE EXTENDED
				{ Tuple.Create(20051, 4L), new SqlPermission("OBJECT", "REFERENCES") }, // SYNONYM
				{ Tuple.Create(20819, 4L), new SqlPermission("OBJECT", "REFERENCES") }, // QUEUE
				{ Tuple.Create(18004, 4L), new SqlPermission("OBJECT", "REFERENCES") }, // FUNCTION TABLE-VALUED SQL
				{ Tuple.Create(8277, 4L), new SqlPermission("OBJECT", "REFERENCES") }, // TABLE
				{ Tuple.Create(20038, 8L), new SqlPermission("OBJECT", "INSERT") }, // FUNCTION SCALAR SQL
				{ Tuple.Create(21318, 8L), new SqlPermission("OBJECT", "INSERT") }, // FUNCTION SCALAR ASSEMBLY 
				{ Tuple.Create(21574, 8L), new SqlPermission("OBJECT", "INSERT") }, // FUNCTION TABLE-VALUED ASSEMBLY 
				{ Tuple.Create(17993, 8L), new SqlPermission("OBJECT", "INSERT") }, // FUNCTION TABLE-VALUED INLINE SQL
				{ Tuple.Create(21321, 8L), new SqlPermission("OBJECT", "INSERT") }, // FUNCTION SCALAR INLINE SQL 
				{ Tuple.Create(17985, 8L), new SqlPermission("OBJECT", "INSERT") }, // AGGREGATE
				{ Tuple.Create(8275, 8L), new SqlPermission("OBJECT", "INSERT") }, // TABLE SYSTEM
				{ Tuple.Create(16975, 8L), new SqlPermission("OBJECT", "INSERT") }, // OBJECT
				{ Tuple.Create(8272, 8L), new SqlPermission("OBJECT", "INSERT") }, // STORED PROCEDURE
				{ Tuple.Create(17232, 8L), new SqlPermission("OBJECT", "INSERT") }, // STORED PROCEDURE ASSEMBLY
				{ Tuple.Create(8274, 8L), new SqlPermission("OBJECT", "INSERT") }, // RULE
				{ Tuple.Create(18002, 8L), new SqlPermission("OBJECT", "INSERT") }, // STORED PROCEDURE REPLICATION FILTER
				{ Tuple.Create(8260, 8L), new SqlPermission("OBJECT", "INSERT") }, // DEFAULT
				{ Tuple.Create(18004, 8L), new SqlPermission("OBJECT", "INSERT") }, // FUNCTION TABLE-VALUED SQL
				{ Tuple.Create(20051, 8L), new SqlPermission("OBJECT", "INSERT") }, // SYNONYM
				{ Tuple.Create(20819, 8L), new SqlPermission("OBJECT", "INSERT") }, // QUEUE
				{ Tuple.Create(8277, 8L), new SqlPermission("OBJECT", "INSERT") }, // TABLE
				{ Tuple.Create(8278, 8L), new SqlPermission("OBJECT", "INSERT") }, // VIEW
				{ Tuple.Create(8280, 8L), new SqlPermission("OBJECT", "INSERT") }, // STORED PROCEDURE EXTENDED
				{ Tuple.Create(17985, 16L), new SqlPermission("OBJECT", "DELETE") }, // AGGREGATE
				{ Tuple.Create(16975, 16L), new SqlPermission("OBJECT", "DELETE") }, // OBJECT
				{ Tuple.Create(8272, 16L), new SqlPermission("OBJECT", "DELETE") }, // STORED PROCEDURE
				{ Tuple.Create(17232, 16L), new SqlPermission("OBJECT", "DELETE") }, // STORED PROCEDURE ASSEMBLY
				{ Tuple.Create(8274, 16L), new SqlPermission("OBJECT", "DELETE") }, // RULE
				{ Tuple.Create(18002, 16L), new SqlPermission("OBJECT", "DELETE") }, // STORED PROCEDURE REPLICATION FILTER
				{ Tuple.Create(20038, 16L), new SqlPermission("OBJECT", "DELETE") }, // FUNCTION SCALAR SQL
				{ Tuple.Create(21318, 16L), new SqlPermission("OBJECT", "DELETE") }, // FUNCTION SCALAR ASSEMBLY 
				{ Tuple.Create(21574, 16L), new SqlPermission("OBJECT", "DELETE") }, // FUNCTION TABLE-VALUED ASSEMBLY 
				{ Tuple.Create(17993, 16L), new SqlPermission("OBJECT", "DELETE") }, // FUNCTION TABLE-VALUED INLINE SQL
				{ Tuple.Create(21321, 16L), new SqlPermission("OBJECT", "DELETE") }, // FUNCTION SCALAR INLINE SQL 
				{ Tuple.Create(8260, 16L), new SqlPermission("OBJECT", "DELETE") }, // DEFAULT
				{ Tuple.Create(18004, 16L), new SqlPermission("OBJECT", "DELETE") }, // FUNCTION TABLE-VALUED SQL
				{ Tuple.Create(8277, 16L), new SqlPermission("OBJECT", "DELETE") }, // TABLE
				{ Tuple.Create(8278, 16L), new SqlPermission("OBJECT", "DELETE") }, // VIEW
				{ Tuple.Create(8280, 16L), new SqlPermission("OBJECT", "DELETE") }, // STORED PROCEDURE EXTENDED
				{ Tuple.Create(8275, 16L), new SqlPermission("OBJECT", "DELETE") }, // TABLE SYSTEM
				{ Tuple.Create(20051, 16L), new SqlPermission("OBJECT", "DELETE") }, // SYNONYM
				{ Tuple.Create(20819, 16L), new SqlPermission("OBJECT", "DELETE") }, // QUEUE
				{ Tuple.Create(17985, 32L), new SqlPermission("OBJECT", "EXECUTE") }, // AGGREGATE
				{ Tuple.Create(16975, 32L), new SqlPermission("OBJECT", "EXECUTE") }, // OBJECT
				{ Tuple.Create(8272, 32L), new SqlPermission("OBJECT", "EXECUTE") }, // STORED PROCEDURE
				{ Tuple.Create(17232, 32L), new SqlPermission("OBJECT", "EXECUTE") }, // STORED PROCEDURE ASSEMBLY
				{ Tuple.Create(8274, 32L), new SqlPermission("OBJECT", "EXECUTE") }, // RULE
				{ Tuple.Create(18002, 32L), new SqlPermission("OBJECT", "EXECUTE") }, // STORED PROCEDURE REPLICATION FILTER
				{ Tuple.Create(20038, 32L), new SqlPermission("OBJECT", "EXECUTE") }, // FUNCTION SCALAR SQL
				{ Tuple.Create(21318, 32L), new SqlPermission("OBJECT", "EXECUTE") }, // FUNCTION SCALAR ASSEMBLY 
				{ Tuple.Create(21574, 32L), new SqlPermission("OBJECT", "EXECUTE") }, // FUNCTION TABLE-VALUED ASSEMBLY 
				{ Tuple.Create(17993, 32L), new SqlPermission("OBJECT", "EXECUTE") }, // FUNCTION TABLE-VALUED INLINE SQL
				{ Tuple.Create(21321, 32L), new SqlPermission("OBJECT", "EXECUTE") }, // FUNCTION SCALAR INLINE SQL 
				{ Tuple.Create(8278, 32L), new SqlPermission("OBJECT", "EXECUTE") }, // VIEW
				{ Tuple.Create(8280, 32L), new SqlPermission("OBJECT", "EXECUTE") }, // STORED PROCEDURE EXTENDED
				{ Tuple.Create(8260, 32L), new SqlPermission("OBJECT", "EXECUTE") }, // DEFAULT
				{ Tuple.Create(8275, 32L), new SqlPermission("OBJECT", "EXECUTE") }, // TABLE SYSTEM
				{ Tuple.Create(20051, 32L), new SqlPermission("OBJECT", "EXECUTE") }, // SYNONYM
				{ Tuple.Create(20819, 32L), new SqlPermission("OBJECT", "EXECUTE") }, // QUEUE
				{ Tuple.Create(18004, 32L), new SqlPermission("OBJECT", "EXECUTE") }, // FUNCTION TABLE-VALUED SQL
				{ Tuple.Create(8277, 32L), new SqlPermission("OBJECT", "EXECUTE") }, // TABLE
				{ Tuple.Create(17985, 64L), new SqlPermission("OBJECT", "RECEIVE") }, // AGGREGATE
				{ Tuple.Create(8260, 64L), new SqlPermission("OBJECT", "RECEIVE") }, // DEFAULT
				{ Tuple.Create(20038, 64L), new SqlPermission("OBJECT", "RECEIVE") }, // FUNCTION SCALAR SQL
				{ Tuple.Create(21318, 64L), new SqlPermission("OBJECT", "RECEIVE") }, // FUNCTION SCALAR ASSEMBLY 
				{ Tuple.Create(21574, 64L), new SqlPermission("OBJECT", "RECEIVE") }, // FUNCTION TABLE-VALUED ASSEMBLY 
				{ Tuple.Create(17993, 64L), new SqlPermission("OBJECT", "RECEIVE") }, // FUNCTION TABLE-VALUED INLINE SQL
				{ Tuple.Create(21321, 64L), new SqlPermission("OBJECT", "RECEIVE") }, // FUNCTION SCALAR INLINE SQL 
				{ Tuple.Create(16975, 64L), new SqlPermission("OBJECT", "RECEIVE") }, // OBJECT
				{ Tuple.Create(8272, 64L), new SqlPermission("OBJECT", "RECEIVE") }, // STORED PROCEDURE
				{ Tuple.Create(17232, 64L), new SqlPermission("OBJECT", "RECEIVE") }, // STORED PROCEDURE ASSEMBLY
				{ Tuple.Create(8274, 64L), new SqlPermission("OBJECT", "RECEIVE") }, // RULE
				{ Tuple.Create(18002, 64L), new SqlPermission("OBJECT", "RECEIVE") }, // STORED PROCEDURE REPLICATION FILTER
				{ Tuple.Create(8275, 64L), new SqlPermission("OBJECT", "RECEIVE") }, // TABLE SYSTEM
				{ Tuple.Create(20051, 64L), new SqlPermission("OBJECT", "RECEIVE") }, // SYNONYM
				{ Tuple.Create(20819, 64L), new SqlPermission("OBJECT", "RECEIVE") }, // QUEUE
				{ Tuple.Create(8278, 64L), new SqlPermission("OBJECT", "RECEIVE") }, // VIEW
				{ Tuple.Create(8280, 64L), new SqlPermission("OBJECT", "RECEIVE") }, // STORED PROCEDURE EXTENDED
				{ Tuple.Create(18004, 64L), new SqlPermission("OBJECT", "RECEIVE") }, // FUNCTION TABLE-VALUED SQL
				{ Tuple.Create(8277, 64L), new SqlPermission("OBJECT", "RECEIVE") }, // TABLE
				{ Tuple.Create(17985, 256L), new SqlPermission("OBJECT", "VIEW DEFINITION") }, // AGGREGATE
				{ Tuple.Create(20038, 256L), new SqlPermission("OBJECT", "VIEW DEFINITION") }, // FUNCTION SCALAR SQL
				{ Tuple.Create(21318, 256L), new SqlPermission("OBJECT", "VIEW DEFINITION") }, // FUNCTION SCALAR ASSEMBLY 
				{ Tuple.Create(21574, 256L), new SqlPermission("OBJECT", "VIEW DEFINITION") }, // FUNCTION TABLE-VALUED ASSEMBLY 
				{ Tuple.Create(17993, 256L), new SqlPermission("OBJECT", "VIEW DEFINITION") }, // FUNCTION TABLE-VALUED INLINE SQL
				{ Tuple.Create(21321, 256L), new SqlPermission("OBJECT", "VIEW DEFINITION") }, // FUNCTION SCALAR INLINE SQL 
				{ Tuple.Create(16975, 256L), new SqlPermission("OBJECT", "VIEW DEFINITION") }, // OBJECT
				{ Tuple.Create(8272, 256L), new SqlPermission("OBJECT", "VIEW DEFINITION") }, // STORED PROCEDURE
				{ Tuple.Create(17232, 256L), new SqlPermission("OBJECT", "VIEW DEFINITION") }, // STORED PROCEDURE ASSEMBLY
				{ Tuple.Create(8274, 256L), new SqlPermission("OBJECT", "VIEW DEFINITION") }, // RULE
				{ Tuple.Create(18002, 256L), new SqlPermission("OBJECT", "VIEW DEFINITION") }, // STORED PROCEDURE REPLICATION FILTER
				{ Tuple.Create(8275, 256L), new SqlPermission("OBJECT", "VIEW DEFINITION") }, // TABLE SYSTEM
				{ Tuple.Create(8277, 256L), new SqlPermission("OBJECT", "VIEW DEFINITION") }, // TABLE
				{ Tuple.Create(8278, 256L), new SqlPermission("OBJECT", "VIEW DEFINITION") }, // VIEW
				{ Tuple.Create(8280, 256L), new SqlPermission("OBJECT", "VIEW DEFINITION") }, // STORED PROCEDURE EXTENDED
				{ Tuple.Create(8260, 256L), new SqlPermission("OBJECT", "VIEW DEFINITION") }, // DEFAULT
				{ Tuple.Create(18004, 256L), new SqlPermission("OBJECT", "VIEW DEFINITION") }, // FUNCTION TABLE-VALUED SQL
				{ Tuple.Create(20051, 256L), new SqlPermission("OBJECT", "VIEW DEFINITION") }, // SYNONYM
				{ Tuple.Create(20819, 256L), new SqlPermission("OBJECT", "VIEW DEFINITION") }, // QUEUE
				{ Tuple.Create(17985, 512L), new SqlPermission("OBJECT", "ALTER") }, // AGGREGATE
				{ Tuple.Create(8260, 512L), new SqlPermission("OBJECT", "ALTER") }, // DEFAULT
				{ Tuple.Create(16975, 512L), new SqlPermission("OBJECT", "ALTER") }, // OBJECT
				{ Tuple.Create(8272, 512L), new SqlPermission("OBJECT", "ALTER") }, // STORED PROCEDURE
				{ Tuple.Create(17232, 512L), new SqlPermission("OBJECT", "ALTER") }, // STORED PROCEDURE ASSEMBLY
				{ Tuple.Create(8274, 512L), new SqlPermission("OBJECT", "ALTER") }, // RULE
				{ Tuple.Create(18002, 512L), new SqlPermission("OBJECT", "ALTER") }, // STORED PROCEDURE REPLICATION FILTER
				{ Tuple.Create(20038, 512L), new SqlPermission("OBJECT", "ALTER") }, // FUNCTION SCALAR SQL
				{ Tuple.Create(21318, 512L), new SqlPermission("OBJECT", "ALTER") }, // FUNCTION SCALAR ASSEMBLY 
				{ Tuple.Create(21574, 512L), new SqlPermission("OBJECT", "ALTER") }, // FUNCTION TABLE-VALUED ASSEMBLY 
				{ Tuple.Create(17993, 512L), new SqlPermission("OBJECT", "ALTER") }, // FUNCTION TABLE-VALUED INLINE SQL
				{ Tuple.Create(21321, 512L), new SqlPermission("OBJECT", "ALTER") }, // FUNCTION SCALAR INLINE SQL 
				{ Tuple.Create(8278, 512L), new SqlPermission("OBJECT", "ALTER") }, // VIEW
				{ Tuple.Create(8280, 512L), new SqlPermission("OBJECT", "ALTER") }, // STORED PROCEDURE EXTENDED
				{ Tuple.Create(18004, 512L), new SqlPermission("OBJECT", "ALTER") }, // FUNCTION TABLE-VALUED SQL
				{ Tuple.Create(8277, 512L), new SqlPermission("OBJECT", "ALTER") }, // TABLE
				{ Tuple.Create(8275, 512L), new SqlPermission("OBJECT", "ALTER") }, // TABLE SYSTEM
				{ Tuple.Create(20051, 512L), new SqlPermission("OBJECT", "ALTER") }, // SYNONYM
				{ Tuple.Create(20819, 512L), new SqlPermission("OBJECT", "ALTER") }, // QUEUE
				{ Tuple.Create(17985, 1024L), new SqlPermission("OBJECT", "TAKE OWNERSHIP") }, // AGGREGATE
				{ Tuple.Create(16975, 1024L), new SqlPermission("OBJECT", "TAKE OWNERSHIP") }, // OBJECT
				{ Tuple.Create(8272, 1024L), new SqlPermission("OBJECT", "TAKE OWNERSHIP") }, // STORED PROCEDURE
				{ Tuple.Create(17232, 1024L), new SqlPermission("OBJECT", "TAKE OWNERSHIP") }, // STORED PROCEDURE ASSEMBLY
				{ Tuple.Create(8274, 1024L), new SqlPermission("OBJECT", "TAKE OWNERSHIP") }, // RULE
				{ Tuple.Create(18002, 1024L), new SqlPermission("OBJECT", "TAKE OWNERSHIP") }, // STORED PROCEDURE REPLICATION FILTER
				{ Tuple.Create(20038, 1024L), new SqlPermission("OBJECT", "TAKE OWNERSHIP") }, // FUNCTION SCALAR SQL
				{ Tuple.Create(21318, 1024L), new SqlPermission("OBJECT", "TAKE OWNERSHIP") }, // FUNCTION SCALAR ASSEMBLY 
				{ Tuple.Create(21574, 1024L), new SqlPermission("OBJECT", "TAKE OWNERSHIP") }, // FUNCTION TABLE-VALUED ASSEMBLY 
				{ Tuple.Create(17993, 1024L), new SqlPermission("OBJECT", "TAKE OWNERSHIP") }, // FUNCTION TABLE-VALUED INLINE SQL
				{ Tuple.Create(21321, 1024L), new SqlPermission("OBJECT", "TAKE OWNERSHIP") }, // FUNCTION SCALAR INLINE SQL 
				{ Tuple.Create(8260, 1024L), new SqlPermission("OBJECT", "TAKE OWNERSHIP") }, // DEFAULT
				{ Tuple.Create(8275, 1024L), new SqlPermission("OBJECT", "TAKE OWNERSHIP") }, // TABLE SYSTEM
				{ Tuple.Create(20051, 1024L), new SqlPermission("OBJECT", "TAKE OWNERSHIP") }, // SYNONYM
				{ Tuple.Create(20819, 1024L), new SqlPermission("OBJECT", "TAKE OWNERSHIP") }, // QUEUE
				{ Tuple.Create(18004, 1024L), new SqlPermission("OBJECT", "TAKE OWNERSHIP") }, // FUNCTION TABLE-VALUED SQL
				{ Tuple.Create(8277, 1024L), new SqlPermission("OBJECT", "TAKE OWNERSHIP") }, // TABLE
				{ Tuple.Create(8278, 1024L), new SqlPermission("OBJECT", "TAKE OWNERSHIP") }, // VIEW
				{ Tuple.Create(8280, 1024L), new SqlPermission("OBJECT", "TAKE OWNERSHIP") }, // STORED PROCEDURE EXTENDED
				{ Tuple.Create(17985, 4096L), new SqlPermission("OBJECT", "CONTROL") }, // AGGREGATE
				{ Tuple.Create(20038, 4096L), new SqlPermission("OBJECT", "CONTROL") }, // FUNCTION SCALAR SQL
				{ Tuple.Create(21318, 4096L), new SqlPermission("OBJECT", "CONTROL") }, // FUNCTION SCALAR ASSEMBLY 
				{ Tuple.Create(21574, 4096L), new SqlPermission("OBJECT", "CONTROL") }, // FUNCTION TABLE-VALUED ASSEMBLY 
				{ Tuple.Create(17993, 4096L), new SqlPermission("OBJECT", "CONTROL") }, // FUNCTION TABLE-VALUED INLINE SQL
				{ Tuple.Create(21321, 4096L), new SqlPermission("OBJECT", "CONTROL") }, // FUNCTION SCALAR INLINE SQL 
				{ Tuple.Create(16975, 4096L), new SqlPermission("OBJECT", "CONTROL") }, // OBJECT
				{ Tuple.Create(8272, 4096L), new SqlPermission("OBJECT", "CONTROL") }, // STORED PROCEDURE
				{ Tuple.Create(17232, 4096L), new SqlPermission("OBJECT", "CONTROL") }, // STORED PROCEDURE ASSEMBLY
				{ Tuple.Create(8274, 4096L), new SqlPermission("OBJECT", "CONTROL") }, // RULE
				{ Tuple.Create(18002, 4096L), new SqlPermission("OBJECT", "CONTROL") }, // STORED PROCEDURE REPLICATION FILTER
				{ Tuple.Create(8275, 4096L), new SqlPermission("OBJECT", "CONTROL") }, // TABLE SYSTEM
				{ Tuple.Create(20051, 4096L), new SqlPermission("OBJECT", "CONTROL") }, // SYNONYM
				{ Tuple.Create(20819, 4096L), new SqlPermission("OBJECT", "CONTROL") }, // QUEUE
				{ Tuple.Create(8260, 4096L), new SqlPermission("OBJECT", "CONTROL") }, // DEFAULT
				{ Tuple.Create(18004, 4096L), new SqlPermission("OBJECT", "CONTROL") }, // FUNCTION TABLE-VALUED SQL
				{ Tuple.Create(8277, 4096L), new SqlPermission("OBJECT", "CONTROL") }, // TABLE
				{ Tuple.Create(8278, 4096L), new SqlPermission("OBJECT", "CONTROL") }, // VIEW
				{ Tuple.Create(8280, 4096L), new SqlPermission("OBJECT", "CONTROL") }, // STORED PROCEDURE EXTENDED
				{ Tuple.Create(17985, 8192L), new SqlPermission("OBJECT", "VIEW CHANGE TRACKING") }, // AGGREGATE
				{ Tuple.Create(20038, 8192L), new SqlPermission("OBJECT", "VIEW CHANGE TRACKING") }, // FUNCTION SCALAR SQL
				{ Tuple.Create(21318, 8192L), new SqlPermission("OBJECT", "VIEW CHANGE TRACKING") }, // FUNCTION SCALAR ASSEMBLY 
				{ Tuple.Create(21574, 8192L), new SqlPermission("OBJECT", "VIEW CHANGE TRACKING") }, // FUNCTION TABLE-VALUED ASSEMBLY 
				{ Tuple.Create(17993, 8192L), new SqlPermission("OBJECT", "VIEW CHANGE TRACKING") }, // FUNCTION TABLE-VALUED INLINE SQL
				{ Tuple.Create(21321, 8192L), new SqlPermission("OBJECT", "VIEW CHANGE TRACKING") }, // FUNCTION SCALAR INLINE SQL 
				{ Tuple.Create(16975, 8192L), new SqlPermission("OBJECT", "VIEW CHANGE TRACKING") }, // OBJECT
				{ Tuple.Create(8272, 8192L), new SqlPermission("OBJECT", "VIEW CHANGE TRACKING") }, // STORED PROCEDURE
				{ Tuple.Create(17232, 8192L), new SqlPermission("OBJECT", "VIEW CHANGE TRACKING") }, // STORED PROCEDURE ASSEMBLY
				{ Tuple.Create(8274, 8192L), new SqlPermission("OBJECT", "VIEW CHANGE TRACKING") }, // RULE
				{ Tuple.Create(18002, 8192L), new SqlPermission("OBJECT", "VIEW CHANGE TRACKING") }, // STORED PROCEDURE REPLICATION FILTER
				{ Tuple.Create(8260, 8192L), new SqlPermission("OBJECT", "VIEW CHANGE TRACKING") }, // DEFAULT
				{ Tuple.Create(8275, 8192L), new SqlPermission("OBJECT", "VIEW CHANGE TRACKING") }, // TABLE SYSTEM
				{ Tuple.Create(8278, 8192L), new SqlPermission("OBJECT", "VIEW CHANGE TRACKING") }, // VIEW
				{ Tuple.Create(8280, 8192L), new SqlPermission("OBJECT", "VIEW CHANGE TRACKING") }, // STORED PROCEDURE EXTENDED
				{ Tuple.Create(20051, 8192L), new SqlPermission("OBJECT", "VIEW CHANGE TRACKING") }, // SYNONYM
				{ Tuple.Create(20819, 8192L), new SqlPermission("OBJECT", "VIEW CHANGE TRACKING") }, // QUEUE
				{ Tuple.Create(18004, 8192L), new SqlPermission("OBJECT", "VIEW CHANGE TRACKING") }, // FUNCTION TABLE-VALUED SQL
				{ Tuple.Create(8277, 8192L), new SqlPermission("OBJECT", "VIEW CHANGE TRACKING") }, // TABLE
				{ Tuple.Create(20034, 1L), new SqlPermission("REMOTE SERVICE BINDING", "VIEW DEFINITION") }, // REMOTE SERVICE BINDING
				{ Tuple.Create(20034, 4L), new SqlPermission("REMOTE SERVICE BINDING", "ALTER") }, // REMOTE SERVICE BINDING
				{ Tuple.Create(20034, 8L), new SqlPermission("REMOTE SERVICE BINDING", "TAKE OWNERSHIP") }, // REMOTE SERVICE BINDING
				{ Tuple.Create(20034, 32L), new SqlPermission("REMOTE SERVICE BINDING", "CONTROL") }, // REMOTE SERVICE BINDING
				{ Tuple.Create(19538, 2L), new SqlPermission("ROLE", "VIEW DEFINITION") }, // ROLE
				{ Tuple.Create(19538, 8L), new SqlPermission("ROLE", "ALTER") }, // ROLE
				{ Tuple.Create(19538, 16L), new SqlPermission("ROLE", "TAKE OWNERSHIP") }, // ROLE
				{ Tuple.Create(19538, 64L), new SqlPermission("ROLE", "CONTROL") }, // ROLE
				{ Tuple.Create(21586, 1L), new SqlPermission("ROUTE", "VIEW DEFINITION") }, // ROUTE
				{ Tuple.Create(21586, 4L), new SqlPermission("ROUTE", "ALTER") }, // ROUTE
				{ Tuple.Create(21586, 8L), new SqlPermission("ROUTE", "TAKE OWNERSHIP") }, // ROUTE
				{ Tuple.Create(21586, 32L), new SqlPermission("ROUTE", "CONTROL") }, // ROUTE
				{ Tuple.Create(17235, 1L), new SqlPermission("SCHEMA", "SELECT") }, // SCHEMA
				{ Tuple.Create(17235, 2L), new SqlPermission("SCHEMA", "INSERT") }, // SCHEMA
				{ Tuple.Create(17235, 4L), new SqlPermission("SCHEMA", "UPDATE") }, // SCHEMA
				{ Tuple.Create(17235, 8L), new SqlPermission("SCHEMA", "DELETE") }, // SCHEMA
				{ Tuple.Create(17235, 16L), new SqlPermission("SCHEMA", "REFERENCES") }, // SCHEMA
				{ Tuple.Create(17235, 32L), new SqlPermission("SCHEMA", "EXECUTE") }, // SCHEMA
				{ Tuple.Create(17235, 128L), new SqlPermission("SCHEMA", "VIEW DEFINITION") }, // SCHEMA
				{ Tuple.Create(17235, 256L), new SqlPermission("SCHEMA", "CREATE SEQUENCE") }, // SCHEMA
				{ Tuple.Create(17235, 512L), new SqlPermission("SCHEMA", "ALTER") }, // SCHEMA
				{ Tuple.Create(17235, 1024L), new SqlPermission("SCHEMA", "TAKE OWNERSHIP") }, // SCHEMA
				{ Tuple.Create(17235, 4096L), new SqlPermission("SCHEMA", "CONTROL") }, // SCHEMA
				{ Tuple.Create(17235, 8192L), new SqlPermission("SCHEMA", "VIEW CHANGE TRACKING") }, // SCHEMA
				{ Tuple.Create(21075, 1L), new SqlPermission("SERVER", "CONNECT SQL") }, // SERVER
				{ Tuple.Create(21075, 2L), new SqlPermission("SERVER", "SHUTDOWN") }, // SERVER
				{ Tuple.Create(21075, 4L), new SqlPermission("SERVER", "ALTER ANY AVAILABILITY GROUP") }, // SERVER
				{ Tuple.Create(21075, 8L), new SqlPermission("SERVER", "CREATE AVAILABILITY GROUP") }, // SERVER
				{ Tuple.Create(21075, 32L), new SqlPermission("SERVER", "CREATE ENDPOINT") }, // SERVER
				{ Tuple.Create(21075, 64L), new SqlPermission("SERVER", "CREATE ANY DATABASE") }, // SERVER
				{ Tuple.Create(21075, 128L), new SqlPermission("SERVER", "ALTER ANY LOGIN") }, // SERVER
				{ Tuple.Create(21075, 256L), new SqlPermission("SERVER", "ALTER ANY CREDENTIAL") }, // SERVER
				{ Tuple.Create(21075, 512L), new SqlPermission("SERVER", "ALTER ANY ENDPOINT") }, // SERVER
				{ Tuple.Create(21075, 1024L), new SqlPermission("SERVER", "ALTER ANY LINKED SERVER") }, // SERVER
				{ Tuple.Create(21075, 2048L), new SqlPermission("SERVER", "ALTER ANY CONNECTION") }, // SERVER
				{ Tuple.Create(21075, 4096L), new SqlPermission("SERVER", "ALTER ANY DATABASE") }, // SERVER
				{ Tuple.Create(21075, 8192L), new SqlPermission("SERVER", "ALTER RESOURCES") }, // SERVER
				{ Tuple.Create(21075, 16384L), new SqlPermission("SERVER", "ALTER SETTINGS") }, // SERVER
				{ Tuple.Create(21075, 32768L), new SqlPermission("SERVER", "ALTER TRACE") }, // SERVER
				{ Tuple.Create(21075, 65536L), new SqlPermission("SERVER", "ADMINISTER BULK OPERATIONS") }, // SERVER
				{ Tuple.Create(21075, 131072L), new SqlPermission("SERVER", "AUTHENTICATE SERVER") }, // SERVER
				{ Tuple.Create(21075, 262144L), new SqlPermission("SERVER", "EXTERNAL ACCESS ASSEMBLY") }, // SERVER
				{ Tuple.Create(21075, 1048576L), new SqlPermission("SERVER", "VIEW ANY DATABASE") }, // SERVER
				{ Tuple.Create(21075, 2097152L), new SqlPermission("SERVER", "VIEW ANY DEFINITION") }, // SERVER
				{ Tuple.Create(21075, 4194304L), new SqlPermission("SERVER", "VIEW SERVER STATE") }, // SERVER
				{ Tuple.Create(21075, 8388608L), new SqlPermission("SERVER", "CREATE DDL EVENT NOTIFICATION") }, // SERVER
				{ Tuple.Create(21075, 16777216L), new SqlPermission("SERVER", "CREATE TRACE EVENT NOTIFICATION") }, // SERVER
				{ Tuple.Create(21075, 33554432L), new SqlPermission("SERVER", "ALTER ANY EVENT NOTIFICATION") }, // SERVER
				{ Tuple.Create(21075, 67108864L), new SqlPermission("SERVER", "ALTER SERVER STATE") }, // SERVER
				{ Tuple.Create(21075, 134217728L), new SqlPermission("SERVER", "UNSAFE ASSEMBLY") }, // SERVER
				{ Tuple.Create(21075, 268435456L), new SqlPermission("SERVER", "ALTER ANY SERVER AUDIT") }, // SERVER
				{ Tuple.Create(21075, 536870912L), new SqlPermission("SERVER", "CREATE SERVER ROLE") }, // SERVER
				{ Tuple.Create(21075, 1073741824L), new SqlPermission("SERVER", "ALTER ANY SERVER ROLE") }, // SERVER
				{ Tuple.Create(21075, 2147483648L), new SqlPermission("SERVER", "ALTER ANY EVENT SESSION") }, // SERVER
				{ Tuple.Create(21075, 4294967296L), new SqlPermission("SERVER", "CONTROL SERVER") }, // SERVER
				{ Tuple.Create(18259, 2L), new SqlPermission("SERVER ROLE", "VIEW DEFINITION") }, // SERVER ROLE
				{ Tuple.Create(18259, 8L), new SqlPermission("SERVER ROLE", "ALTER") }, // SERVER ROLE
				{ Tuple.Create(18259, 16L), new SqlPermission("SERVER ROLE", "TAKE OWNERSHIP") }, // SERVER ROLE
				{ Tuple.Create(18259, 64L), new SqlPermission("SERVER ROLE", "CONTROL") }, // SERVER ROLE
				{ Tuple.Create(22099, 1L), new SqlPermission("SERVICE", "SEND") }, // SERVICE
				{ Tuple.Create(22099, 2L), new SqlPermission("SERVICE", "VIEW DEFINITION") }, // SERVICE
				{ Tuple.Create(22099, 8L), new SqlPermission("SERVICE", "ALTER") }, // SERVICE
				{ Tuple.Create(22099, 16L), new SqlPermission("SERVICE", "TAKE OWNERSHIP") }, // SERVICE
				{ Tuple.Create(22099, 64L), new SqlPermission("SERVICE", "CONTROL") }, // SERVICE
				{ Tuple.Create(19283, 1L), new SqlPermission("SYMMETRIC KEY", "REFERENCES") }, // SYMMETRIC KEY
				{ Tuple.Create(19283, 2L), new SqlPermission("SYMMETRIC KEY", "VIEW DEFINITION") }, // SYMMETRIC KEY
				{ Tuple.Create(19283, 8L), new SqlPermission("SYMMETRIC KEY", "ALTER") }, // SYMMETRIC KEY
				{ Tuple.Create(19283, 16L), new SqlPermission("SYMMETRIC KEY", "TAKE OWNERSHIP") }, // SYMMETRIC KEY
				{ Tuple.Create(19283, 64L), new SqlPermission("SYMMETRIC KEY", "CONTROL") }, // SYMMETRIC KEY
				{ Tuple.Create(22868, 1L), new SqlPermission("TYPE", "REFERENCES") }, // TYPE
				{ Tuple.Create(22868, 2L), new SqlPermission("TYPE", "EXECUTE") }, // TYPE
				{ Tuple.Create(22868, 4L), new SqlPermission("TYPE", "VIEW DEFINITION") }, // TYPE
				{ Tuple.Create(22868, 16L), new SqlPermission("TYPE", "TAKE OWNERSHIP") }, // TYPE
				{ Tuple.Create(22868, 64L), new SqlPermission("TYPE", "CONTROL") }, // TYPE
				{ Tuple.Create(21333, 1L), new SqlPermission("USER", "IMPERSONATE") }, // USER
				{ Tuple.Create(21333, 2L), new SqlPermission("USER", "VIEW DEFINITION") }, // USER
				{ Tuple.Create(21333, 8L), new SqlPermission("USER", "ALTER") }, // USER
				{ Tuple.Create(21333, 32L), new SqlPermission("USER", "CONTROL") }, // USER
				{ Tuple.Create(22611, 1L), new SqlPermission("XML SCHEMA COLLECTION", "REFERENCES") }, // XML SCHEMA COLLECTION
				{ Tuple.Create(22611, 2L), new SqlPermission("XML SCHEMA COLLECTION", "EXECUTE") }, // XML SCHEMA COLLECTION
				{ Tuple.Create(22611, 4L), new SqlPermission("XML SCHEMA COLLECTION", "VIEW DEFINITION") }, // XML SCHEMA COLLECTION
				{ Tuple.Create(22611, 16L), new SqlPermission("XML SCHEMA COLLECTION", "ALTER") }, // XML SCHEMA COLLECTION
				{ Tuple.Create(22611, 32L), new SqlPermission("XML SCHEMA COLLECTION", "TAKE OWNERSHIP") }, // XML SCHEMA COLLECTION
				{ Tuple.Create(22611, 128L), new SqlPermission("XML SCHEMA COLLECTION", "CONTROL") }, // XML SCHEMA COLLECTION
            };
        #endregion

    }
}
