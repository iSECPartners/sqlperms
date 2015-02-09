USE [PermTest]
GO

if (exists(select * from sys.schemas s where s.name = 'Foo'))
	DROP SCHEMA [Foo];
GO

CREATE SCHEMA [Foo];
GO

if (exists(select * from sys.database_principals p where p.type = N'A' and p.name = 'appRole'))
	DROP APPLICATION ROLE [appRole];
CREATE APPLICATION ROLE [appRole] WITH DEFAULT_SCHEMA = [dbo], PASSWORD = N'password'

ALTER APPLICATION ROLE [appRole] WITH PASSWORD = N'Password1';

ALTER AUTHORIZATION ON SCHEMA::[Foo] TO [appRole]


DROP SCHEMA [Foo];
DROP APPLICATION ROLE [appRole];
