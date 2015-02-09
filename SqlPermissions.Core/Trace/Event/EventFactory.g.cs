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

namespace SqlPermissions.Core.Trace.Event
{
    public partial class EventFactory
    {
        private static readonly SortedList<String, Tuple<CreateLoader, CreateEvent>> Creation
            = new SortedList<String, Tuple<CreateLoader, CreateEvent>>
                  {
                      {
                          "Audit Login", 
                          Tuple.Create(new CreateLoader(record => new LoginEventLoaderInfo(record)),
                                       new CreateEvent((record, loadInfoBase) => new LoginEvent(record, loadInfoBase as LoginEventLoaderInfo)))
                      },
                      {
                          "Audit Logout", 
                          Tuple.Create(new CreateLoader(record => new LogoutEventLoaderInfo(record)),
                                       new CreateEvent((record, loadInfoBase) => new LogoutEvent(record, loadInfoBase as LogoutEventLoaderInfo)))
                      },
                      {
                          "Audit Server Starts And Stops", 
                          Tuple.Create(new CreateLoader(record => new ServerStartAndStopEventLoaderInfo(record)),
                                       new CreateEvent((record, loadInfoBase) => new ServerStartAndStopEvent(record, loadInfoBase as ServerStartAndStopEventLoaderInfo)))
                      },
                      {
                          "Audit Login Failed", 
                          Tuple.Create(new CreateLoader(record => new LoginFailedEventLoaderInfo(record)),
                                       new CreateEvent((record, loadInfoBase) => new LoginFailedEvent(record, loadInfoBase as LoginFailedEventLoaderInfo)))
                      },
                      {
                          "Audit Database Scope GDR Event", 
                          Tuple.Create(new CreateLoader(record => new DatabaseScopeGdrEventLoaderInfo(record)),
                                       new CreateEvent((record, loadInfoBase) => new DatabaseScopeGdrEvent(record, loadInfoBase as DatabaseScopeGdrEventLoaderInfo)))
                      },
                      {
                          "Audit Schema Object GDR Event", 
                          Tuple.Create(new CreateLoader(record => new SchemaObjectGdrEventLoaderInfo(record)),
                                       new CreateEvent((record, loadInfoBase) => new SchemaObjectGdrEvent(record, loadInfoBase as SchemaObjectGdrEventLoaderInfo)))
                      },
                      {
                          "Audit Addlogin Event", 
                          Tuple.Create(new CreateLoader(record => new AddLoginEventLoaderInfo(record)),
                                       new CreateEvent((record, loadInfoBase) => new AddLoginEvent(record, loadInfoBase as AddLoginEventLoaderInfo)))
                      },
                      {
                          "Audit Login GDR Event", 
                          Tuple.Create(new CreateLoader(record => new LoginGdrEventLoaderInfo(record)),
                                       new CreateEvent((record, loadInfoBase) => new LoginGdrEvent(record, loadInfoBase as LoginGdrEventLoaderInfo)))
                      },
                      {
                          "Audit Login Change Property Event", 
                          Tuple.Create(new CreateLoader(record => new LoginChangePropertyEventLoaderInfo(record)),
                                       new CreateEvent((record, loadInfoBase) => new LoginChangePropertyEvent(record, loadInfoBase as LoginChangePropertyEventLoaderInfo)))
                      },
                      {
                          "Audit Login Change Password Event", 
                          Tuple.Create(new CreateLoader(record => new LoginChangePasswordEventLoaderInfo(record)),
                                       new CreateEvent((record, loadInfoBase) => new LoginChangePasswordEvent(record, loadInfoBase as LoginChangePasswordEventLoaderInfo)))
                      },
                      {
                          "Audit Add Login to Server Role Event", 
                          Tuple.Create(new CreateLoader(record => new AddLoginToServerRoleEventLoaderInfo(record)),
                                       new CreateEvent((record, loadInfoBase) => new AddLoginToServerRoleEvent(record, loadInfoBase as AddLoginToServerRoleEventLoaderInfo)))
                      },
                      {
                          "Audit Add DB User Event", 
                          Tuple.Create(new CreateLoader(record => new AddDbUserEventLoaderInfo(record)),
                                       new CreateEvent((record, loadInfoBase) => new AddDbUserEvent(record, loadInfoBase as AddDbUserEventLoaderInfo)))
                      },
                      {
                          "Audit Add Member to DB Role Event", 
                          Tuple.Create(new CreateLoader(record => new AddMemberToDbRoleEventLoaderInfo(record)),
                                       new CreateEvent((record, loadInfoBase) => new AddMemberToDbRoleEvent(record, loadInfoBase as AddMemberToDbRoleEventLoaderInfo)))
                      },
                      {
                          "Audit Add Role Event", 
                          Tuple.Create(new CreateLoader(record => new AddRoleEventLoaderInfo(record)),
                                       new CreateEvent((record, loadInfoBase) => new AddRoleEvent(record, loadInfoBase as AddRoleEventLoaderInfo)))
                      },
                      {
                          "Audit App Role Change Password Event", 
                          Tuple.Create(new CreateLoader(record => new AppRoleChangePasswordEventLoaderInfo(record)),
                                       new CreateEvent((record, loadInfoBase) => new AppRoleChangePasswordEvent(record, loadInfoBase as AppRoleChangePasswordEventLoaderInfo)))
                      },
                      {
                          "Audit Statement Permission Event", 
                          Tuple.Create(new CreateLoader(record => new StatementPermissionEventLoaderInfo(record)),
                                       new CreateEvent((record, loadInfoBase) => new StatementPermissionEvent(record, loadInfoBase as StatementPermissionEventLoaderInfo)))
                      },
                      {
                          "Audit Schema Object Access Event", 
                          Tuple.Create(new CreateLoader(record => new SchemaObjectAccessEventLoaderInfo(record)),
                                       new CreateEvent((record, loadInfoBase) => new SchemaObjectAccessEvent(record, loadInfoBase as SchemaObjectAccessEventLoaderInfo)))
                      },
                      {
                          "Audit Backup/Restore Event", 
                          Tuple.Create(new CreateLoader(record => new BackupRestoreEventLoaderInfo(record)),
                                       new CreateEvent((record, loadInfoBase) => new BackupRestoreEvent(record, loadInfoBase as BackupRestoreEventLoaderInfo)))
                      },
                      {
                          "Audit DBCC Event", 
                          Tuple.Create(new CreateLoader(record => new DbccEventLoaderInfo(record)),
                                       new CreateEvent((record, loadInfoBase) => new DbccEvent(record, loadInfoBase as DbccEventLoaderInfo)))
                      },
                      {
                          "Audit Change Audit Event", 
                          Tuple.Create(new CreateLoader(record => new ChangeAuditEventLoaderInfo(record)),
                                       new CreateEvent((record, loadInfoBase) => new ChangeAuditEvent(record, loadInfoBase as ChangeAuditEventLoaderInfo)))
                      },
                      {
                          "Audit Object Derived Permission Event", 
                          Tuple.Create(new CreateLoader(record => new ObjectDerivedPermissionEventLoaderInfo(record)),
                                       new CreateEvent((record, loadInfoBase) => new ObjectDerivedPermissionEvent(record, loadInfoBase as ObjectDerivedPermissionEventLoaderInfo)))
                      },
                      {
                          "Audit Database Management Event", 
                          Tuple.Create(new CreateLoader(record => new DatabaseManagementEventLoaderInfo(record)),
                                       new CreateEvent((record, loadInfoBase) => new DatabaseManagementEvent(record, loadInfoBase as DatabaseManagementEventLoaderInfo)))
                      },
                      {
                          "Audit Database Object Management Event", 
                          Tuple.Create(new CreateLoader(record => new DatabaseObjectManagementEventLoaderInfo(record)),
                                       new CreateEvent((record, loadInfoBase) => new DatabaseObjectManagementEvent(record, loadInfoBase as DatabaseObjectManagementEventLoaderInfo)))
                      },
                      {
                          "Audit Database Principal Management Event", 
                          Tuple.Create(new CreateLoader(record => new DatabasePrincipalManagementEventLoaderInfo(record)),
                                       new CreateEvent((record, loadInfoBase) => new DatabasePrincipalManagementEvent(record, loadInfoBase as DatabasePrincipalManagementEventLoaderInfo)))
                      },
                      {
                          "Audit Schema Object Management Event", 
                          Tuple.Create(new CreateLoader(record => new SchemaObjectManagementEventLoaderInfo(record)),
                                       new CreateEvent((record, loadInfoBase) => new SchemaObjectManagementEvent(record, loadInfoBase as SchemaObjectManagementEventLoaderInfo)))
                      },
                      {
                          "Audit Server Principal Impersonation Event", 
                          Tuple.Create(new CreateLoader(record => new ServerPrincipalImpersonationEventLoaderInfo(record)),
                                       new CreateEvent((record, loadInfoBase) => new ServerPrincipalImpersonationEvent(record, loadInfoBase as ServerPrincipalImpersonationEventLoaderInfo)))
                      },
                      {
                          "Audit Database Principal Impersonation Event", 
                          Tuple.Create(new CreateLoader(record => new DatabasePrincipalImpersonationEventLoaderInfo(record)),
                                       new CreateEvent((record, loadInfoBase) => new DatabasePrincipalImpersonationEvent(record, loadInfoBase as DatabasePrincipalImpersonationEventLoaderInfo)))
                      },
                      {
                          "Audit Server Object Take Ownership Event", 
                          Tuple.Create(new CreateLoader(record => new ServerObjectTakeOwnershipEventLoaderInfo(record)),
                                       new CreateEvent((record, loadInfoBase) => new ServerObjectTakeOwnershipEvent(record, loadInfoBase as ServerObjectTakeOwnershipEventLoaderInfo)))
                      },
                      {
                          "Audit Database Object Take Ownership Event", 
                          Tuple.Create(new CreateLoader(record => new DatabaseObjectTakeOwnershipEventLoaderInfo(record)),
                                       new CreateEvent((record, loadInfoBase) => new DatabaseObjectTakeOwnershipEvent(record, loadInfoBase as DatabaseObjectTakeOwnershipEventLoaderInfo)))
                      },
                      {
                          "Audit Change Database Owner", 
                          Tuple.Create(new CreateLoader(record => new ChangeDatabaseOwnerEventLoaderInfo(record)),
                                       new CreateEvent((record, loadInfoBase) => new ChangeDatabaseOwnerEvent(record, loadInfoBase as ChangeDatabaseOwnerEventLoaderInfo)))
                      },
                      {
                          "Audit Schema Object Take Ownership Event", 
                          Tuple.Create(new CreateLoader(record => new SchemaObjectTakeOwnershipEventLoaderInfo(record)),
                                       new CreateEvent((record, loadInfoBase) => new SchemaObjectTakeOwnershipEvent(record, loadInfoBase as SchemaObjectTakeOwnershipEventLoaderInfo)))
                      },
                      {
                          "Audit Database Mirroring Login", 
                          Tuple.Create(new CreateLoader(record => new DatabaseMirroringLoginEventLoaderInfo(record)),
                                       new CreateEvent((record, loadInfoBase) => new DatabaseMirroringLoginEvent(record, loadInfoBase as DatabaseMirroringLoginEventLoaderInfo)))
                      },
                      {
                          "Audit Broker Conversation", 
                          Tuple.Create(new CreateLoader(record => new BrokerConversationEventLoaderInfo(record)),
                                       new CreateEvent((record, loadInfoBase) => new BrokerConversationEvent(record, loadInfoBase as BrokerConversationEventLoaderInfo)))
                      },
                      {
                          "Audit Broker Login", 
                          Tuple.Create(new CreateLoader(record => new BrokerLoginEventLoaderInfo(record)),
                                       new CreateEvent((record, loadInfoBase) => new BrokerLoginEvent(record, loadInfoBase as BrokerLoginEventLoaderInfo)))
                      },
                      {
                          "Audit Server Scope GDR Event", 
                          Tuple.Create(new CreateLoader(record => new ServerScopeGdrEventLoaderInfo(record)),
                                       new CreateEvent((record, loadInfoBase) => new ServerScopeGdrEvent(record, loadInfoBase as ServerScopeGdrEventLoaderInfo)))
                      },
                      {
                          "Audit Server Object GDR Event", 
                          Tuple.Create(new CreateLoader(record => new ServerObjectGdrEventLoaderInfo(record)),
                                       new CreateEvent((record, loadInfoBase) => new ServerObjectGdrEvent(record, loadInfoBase as ServerObjectGdrEventLoaderInfo)))
                      },
                      {
                          "Audit Database Object GDR Event", 
                          Tuple.Create(new CreateLoader(record => new DatabaseObjectGdrEventLoaderInfo(record)),
                                       new CreateEvent((record, loadInfoBase) => new DatabaseObjectGdrEvent(record, loadInfoBase as DatabaseObjectGdrEventLoaderInfo)))
                      },
                      {
                          "Audit Server Operation Event", 
                          Tuple.Create(new CreateLoader(record => new ServerOperationEventLoaderInfo(record)),
                                       new CreateEvent((record, loadInfoBase) => new ServerOperationEvent(record, loadInfoBase as ServerOperationEventLoaderInfo)))
                      },
                      {
                          "Audit Server Alter Trace Event", 
                          Tuple.Create(new CreateLoader(record => new ServerAlterTraceEventLoaderInfo(record)),
                                       new CreateEvent((record, loadInfoBase) => new ServerAlterTraceEvent(record, loadInfoBase as ServerAlterTraceEventLoaderInfo)))
                      },
                      {
                          "Audit Server Object Management Event", 
                          Tuple.Create(new CreateLoader(record => new ServerObjectManagementEventLoaderInfo(record)),
                                       new CreateEvent((record, loadInfoBase) => new ServerObjectManagementEvent(record, loadInfoBase as ServerObjectManagementEventLoaderInfo)))
                      },
                      {
                          "Audit Server Principal Management Event", 
                          Tuple.Create(new CreateLoader(record => new ServerPrincipalManagementEventLoaderInfo(record)),
                                       new CreateEvent((record, loadInfoBase) => new ServerPrincipalManagementEvent(record, loadInfoBase as ServerPrincipalManagementEventLoaderInfo)))
                      },
                      {
                          "Audit Database Operation Event", 
                          Tuple.Create(new CreateLoader(record => new DatabaseOperationEventLoaderInfo(record)),
                                       new CreateEvent((record, loadInfoBase) => new DatabaseOperationEvent(record, loadInfoBase as DatabaseOperationEventLoaderInfo)))
                      },
                      {
                          "Audit Database Object Access Event", 
                          Tuple.Create(new CreateLoader(record => new DatabaseObjectAccessEventLoaderInfo(record)),
                                       new CreateEvent((record, loadInfoBase) => new DatabaseObjectAccessEvent(record, loadInfoBase as DatabaseObjectAccessEventLoaderInfo)))
                      },
                      {
                          "Audit Fulltext", 
                          Tuple.Create(new CreateLoader(record => new FulltextEventLoaderInfo(record)),
                                       new CreateEvent((record, loadInfoBase) => new FulltextEvent(record, loadInfoBase as FulltextEventLoaderInfo)))
                      },
                  };

        private static readonly SortedList<Int32, String> IdLookup
            = new SortedList<Int32, String>
                  {
                      { 14, "Audit Login" },
                      { 15, "Audit Logout" },
                      { 18, "Audit Server Starts And Stops" },
                      { 20, "Audit Login Failed" },
                      { 102, "Audit Database Scope GDR Event" },
                      { 103, "Audit Schema Object GDR Event" },
                      { 104, "Audit Addlogin Event" },
                      { 105, "Audit Login GDR Event" },
                      { 106, "Audit Login Change Property Event" },
                      { 107, "Audit Login Change Password Event" },
                      { 108, "Audit Add Login to Server Role Event" },
                      { 109, "Audit Add DB User Event" },
                      { 110, "Audit Add Member to DB Role Event" },
                      { 111, "Audit Add Role Event" },
                      { 112, "Audit App Role Change Password Event" },
                      { 113, "Audit Statement Permission Event" },
                      { 114, "Audit Schema Object Access Event" },
                      { 115, "Audit Backup/Restore Event" },
                      { 116, "Audit DBCC Event" },
                      { 117, "Audit Change Audit Event" },
                      { 118, "Audit Object Derived Permission Event" },
                      { 128, "Audit Database Management Event" },
                      { 129, "Audit Database Object Management Event" },
                      { 130, "Audit Database Principal Management Event" },
                      { 131, "Audit Schema Object Management Event" },
                      { 132, "Audit Server Principal Impersonation Event" },
                      { 133, "Audit Database Principal Impersonation Event" },
                      { 134, "Audit Server Object Take Ownership Event" },
                      { 135, "Audit Database Object Take Ownership Event" },
                      { 152, "Audit Change Database Owner" },
                      { 153, "Audit Schema Object Take Ownership Event" },
                      { 154, "Audit Database Mirroring Login" },
                      { 158, "Audit Broker Conversation" },
                      { 159, "Audit Broker Login" },
                      { 170, "Audit Server Scope GDR Event" },
                      { 171, "Audit Server Object GDR Event" },
                      { 172, "Audit Database Object GDR Event" },
                      { 173, "Audit Server Operation Event" },
                      { 175, "Audit Server Alter Trace Event" },
                      { 176, "Audit Server Object Management Event" },
                      { 177, "Audit Server Principal Management Event" },
                      { 178, "Audit Database Operation Event" },
                      { 180, "Audit Database Object Access Event" },
                      { 235, "Audit Fulltext" },
                  };

	}
}
