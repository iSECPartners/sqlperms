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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;

namespace SqlTest
{
    class TestHelper
    {
        private readonly IList<IDroppable> _toDrop = new List<IDroppable>();

        private SqlConnectionInfo _connectionInfo;
        public SqlConnectionInfo GetConnection()
        {
            if (null == _connectionInfo)
                _connectionInfo = new SqlConnectionInfo("Data Source=.;Integrated Security=SSPI;");
            return _connectionInfo;
        }

        private ServerConnection _serverConnection;

        public ServerConnection GetServerConnection()
        {
            if (null == _serverConnection)
            {
                _serverConnection = new ServerConnection(GetConnection());
            }
            return _serverConnection;
        }

        private Server _server;

        public Server GetServer()
        {
            if (null == _server)
            {
                _server = new Server();
            }
            return _server;
        }

        private Database _database;

        public Database GetDatabase()
        {
            if (null == _database)
            {
                _database = new Database(GetServer(), "Test_" + Guid.NewGuid());
                _database.Create();
                _toDrop.Add(_database);
            }
            return _database;
        }

        private Schema _schema;

        public Schema GetSchema()
        {
            if (null == _schema)
            {
                _schema = new Schema(GetDatabase(), "Schema1");
                _schema.Create();
                _toDrop.Add(_schema);
            }
            return _schema;
        }

        private Login _login;

        public Login GetLogin()
        {
            const String LoginName = "Login1";
            if (null == _login)
            {
                var srv = GetServer();
                _login = srv.Logins[LoginName];
                if (null == _login)
                {
                    _login = new Login(GetServer(), LoginName)
                        {
                            LoginType = LoginType.SqlLogin
                        };
                    _login.Create("Password1");
                }
                _toDrop.Add(_login);
            }
            return _login;
        }

        private User _user;

        public User GetUser()
        {
            if (null == _user)
            {
                _user = new User(GetDatabase(), "User1");
                _user.Login = GetLogin().Name;
                _user.Create();
                _toDrop.Add(_user);
            }
            return _user;
        }

        public void AddCleanup(IDroppable toDrop)
        {
            _toDrop.Add(toDrop);
        }

        public void Cleanup()
        {
            foreach (var d in _toDrop.Reverse())
            {
                try
                {
                    d.Drop();
                }
                catch (Exception)
                {
                }
            }
        }
    }
}
