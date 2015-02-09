USE [PermTest]
GO


if (exists(select * from sys.schemas s where s.name = 'Foo'))
	DROP SCHEMA [Foo];
GO

CREATE SCHEMA [Foo];
GO

if (exists(select * from sys.database_principals p where p.type = N'R' and p.name = 'Role1'))
	DROP ROLE [Role1];
CREATE ROLE [Role1] AUTHORIZATION [dbo]


GRANT CONTROL ON SCHEMA::[Foo] TO [Role1];
REVOKE CONTROL ON SCHEMA::[Foo] TO [Role1];
DENY CONTROL ON SCHEMA::[Foo] TO [Role1];

if (exists( select 1 from sys.tables t join sys.schemas s on t.schema_id = s.schema_id where t.name = 't1' and s.name = 'Foo') )
	drop table [Foo].t1;

create table [Foo].t1 ( id int, first varchar(255), last varchar(255), created datetime);

if (exists( select 1 from sys.views v join sys.schemas s on v.schema_id = s.schema_id where v.name = 'v1' and s.name = 'Foo') )
	drop view [Foo].t1;
go
create view [Foo].v1 as select * from [Foo].t1;
go


GRANT SELECT ON [Foo].v1 TO [Role1];
GRANT SELECT ON [Foo].t1 TO [Role1];
GRANT INSERT ON [Foo].t1 TO [Role1];
GRANT UPDATE ON [Foo].t1 TO [Role1];
GRANT DELETE ON [Foo].t1 TO [Role1];


insert into [Foo].t1 values ( 1, 'tim', 'test1', getdate() );

update [Foo].t1 set created = getdate(); -- update all rows

delete from [Foo].t1; -- all rows

truncate table [Foo].t1;

drop view [Foo].v1;
drop table [Foo].t1;

DROP SCHEMA [Foo];
DROP ROLE [Role1];

