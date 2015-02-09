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

DROP SCHEMA [Foo];
DROP ROLE [Role1];
