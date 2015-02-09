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
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SqlTest
{
    [TestClass]
    public class SchemaTests
    {
        private SqlConnectionInfo GetConnection()
        {
            return new SqlConnectionInfo("Data Source=.;Initial Catalog=master;Integrated Security=SSPI;");
        }

        [TestMethod]
        public void GrantAccessToSchemaObjects()
        {
            var helper = new TestHelper();
            try
            {
                var schema = helper.GetSchema();
                //schema.Owner = helper.GetUser().Name;
                //schema.Alter();

                var table = new Table(helper.GetDatabase(), "Table1", schema.Name);
                table.Columns.Add(new Column(table, "Col1", DataType.Int));
                table.Columns.Add(new Column(table, "Col2", DataType.NVarCharMax));
                table.Create();
                helper.AddCleanup(table);

                var view = new View(helper.GetDatabase(), "View1", schema.Name)
                    {
                        TextMode = false,
                        TextBody = String.Format("SELECT Col1, Col2 FROM [{0}].[{1}]", table.Schema, table.Name)
                    };
                //view.TextHeader = String.Format("CREATE VIEW [{0}].[{1}] AS", view.Schema, view.Name);
                view.Create();
                helper.AddCleanup(view);

                var scalarTsqlFn = new UserDefinedFunction(helper.GetDatabase(), "ScalarTsqlFunction", schema.Name)
                    {
                        TextMode = false,
                        DataType = DataType.DateTime,
                        ExecutionContext = ExecutionContext.Caller,
                        FunctionType = UserDefinedFunctionType.Scalar,
                        ImplementationType = ImplementationType.TransactSql,
                        TextBody = "BEGIN RETURN GETDATE() END"
                    };
                scalarTsqlFn.Create();
                helper.AddCleanup(scalarTsqlFn);

                var inlineTsqlFn = new UserDefinedFunction(helper.GetDatabase(), "InlineTsqlFunction", schema.Name)
                    {
                        TextMode = false,
                        ExecutionContext = ExecutionContext.Caller,
                        FunctionType = UserDefinedFunctionType.Inline,
                        ImplementationType = ImplementationType.TransactSql,
                        TextBody = String.Format("RETURN SELECT * FROM [{0}].[{1}]", view.Schema, view.Name)
                    };
                inlineTsqlFn.Create();
                helper.AddCleanup(inlineTsqlFn);

                // TODO: Create table valued function

                // TODO: Create Clr scalar func

                // TODO: Create Clr inline func (Exists?)

                // TODO: Create Clr table valued func

                // TODO: Create Clr Aggregate

                var proc = new StoredProcedure(helper.GetDatabase(), "sproc1", schema.Name)
                    {
                        TextMode = false,
                        AnsiNullsStatus = false,
                        QuotedIdentifierStatus = false,
                        TextBody = String.Format("SELECT * FROM [{0}].[{1}]()", inlineTsqlFn.Schema, inlineTsqlFn.Name)
                    };
                proc.Create();
                helper.AddCleanup(proc);

                // TODO: Create Clr Sproc



                // TODO: Create Constraint
                // TODO: Create Queue
                // TODO: Create Statistic
                // TODO: Create Synonym


                var user = helper.GetUser();


                var permissable = new IObjectPermission[]
                    {
                        table,
                        view,
                        scalarTsqlFn,
                        inlineTsqlFn,
                        proc,
                    };

                permissable.Do(tg => tg.GrantAll(user.Name));
                permissable.Do(tg => tg.DenyAll(user.Name));
                permissable.Do(tg => tg.RevokeAll(user.Name));

                // change all owners
                table.Owner = user.Name;
                table.Alter();

                view.Owner = user.Name;
                view.Alter();

                scalarTsqlFn.Owner = user.Name;
                scalarTsqlFn.Alter();

                inlineTsqlFn.Owner = user.Name;
                inlineTsqlFn.Alter();

                proc.Owner = user.Name;
                proc.Alter();
            }
            finally
            {
                helper.Cleanup();
            }
        }
    }
}
