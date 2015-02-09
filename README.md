# SQL Permissions Tool

SqlPermissions is a tool used to calculate the precise minimal permissions necessary for an application using a database. It works in an online fashion by monitoring active connections, or offline by using a sql trace; in both cases each trace event is used to determine a permission grant for every monitored statement. It currently is targeted to work against SQL Server 2012 and 2014, though _should_ be backward compatible with most older SQL Server products after 2005. It does not work with other DBMS's. Though the approach is sound, a different implementation would be required to work with other database platforms.

## Using

The tool is very simple and straightforward to use. It can be done from an online mode or from a trace file that has already been collected. The trace file provides the most performant way to do the trace, and limits the risk in a production scenario. It may also be preferred by DBAs who, rightly, may be concerned about running a random program off the internet on their database servers. _Note, the author of this guide does not warrant against any risks, especially when using this tool in production. I wouldn't, neither should you._ 

### Online monitoring

By passing a connection string and tdf file, the tool can connect to SQL Server and monitor existing traffic to calculate a privilege set. Note, this requires elevated privileges on the SQL server. When using integrated security, the windows user running the program needs those privileges. Integrated security is preferred to prevent entering user names and passwords in different places.

```
SqlPermissions.exe -c 'Data Source=.;Integrated Security=SSPI;' -t .\example_tdf_file.tdf -o output.sql
```

### Offline trace file

For longer running traces or when using production machines, a trace file should be prepared in advance. The .tdf file used for online monitoring can be imported into SQL Profiler. SQL Tracing can be used directly against the database server, without the use of SQL Profiler, but will be left as an exercise to the reader. Once a trace file has been generated, it can be processed by SqlPermissions.exe as follows.

```
SqlPermissions.exe -f .\previously_completed_trace.trc -o output.sql
```

## Development

This release covers much of the most commonly used sql commands, but does not process all possible queries. Queries which are not interpreted are collated into an unprocessed queries section at the end of the sql output. Beside bug fixes, the most likely additional development will be around completing translations for events that are not yet implemented.

The tool uses code generation, based on T4 templates extensively. Much of the data access code is generated based on SQL schema information. It is possible, if not even likely that to support different versions some regeneration of possible events is necessary. Fortunately this should be as simple as targetting the latest SQL version and regenerating the available templates, as long as the IDs are not reused across versions. The program will work fine with events that are unused, but will fail with event ids that are present in a trace but for which no code has been generated.

### Adding a new event

1. Add a new partial class to SqlPermissions.Core.Trace.Event named {EventName}Event. The naming must match the generated class name.
2. Implement the partial class to override BuildPermissionInternal or BuildPermissionsInternal, depending on if a given statement will require a single GRANT (common case) or multiple permission GRANTs.
    - The properties of the originating event are available as code generated members of the class.
    - Use the static GenerateBaseGrant member of the GenericAccessStatement in the common case where the translation is easy, as demonstrated in SchemaObjectTakeOwnershipEvent.

### Structure

The structure of the application is broken into two assemblies:

1. A core library that hosts the majority of the translation logic to convert a given sql trace element into an example permission grant. This logic should be reusable no matter what front-end is desired. This class has a dependency on the Sql ConnectionInfo* framework, that provides the trace functionality. To modify this code, the T4 Toolbox is also helpful to perform code generation. Code generation has hard-coded connection strings using SSPI and a local unnamed server with the master database. Though it is possible some of these definitions may change between versions, necessitating a versioning scheme, this was not done. The mapping logic exists in the Trace.Event namespace, with code-generation creating a stub-partial class that will essentially report the event as unprocessed. To process the event, another partial class must be created for the event that correctly translates the Sql Trace event into a permission grant.

2. A front end command-line based application that will process the sql trace events, whether from a running server or already captured trace file. The front end utilizes the Rx framework to easily present permission grants as they occur. The Rx library is just a nice to have eventing framework, but similar results could be had using a more traditional structure. 

### Libraries / dependencies

- Reactive Extensions (Rx) Framework
- SQL Server ConnectionInfo and ConnectionInfoExtended assembly. _Note, this comes with SQL Server Profiler and is not distributed with this tool._
- T4 Toolbox - Template engine for VS to allow easy code generation.

