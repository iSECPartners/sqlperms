USE [PermTest]
GO

if (exists(select * from sys.schemas s where s.name = 'Foo'))
	DROP SCHEMA [Foo];
GO

CREATE SCHEMA [Foo];
GO


if (exists(select * from sys.server_principals p where p.type = N'S' and p.name = 'testuser1'))
	DROP LOGIN [testuser1];
CREATE LOGIN [testuser1] WITH PASSWORD=N'password', DEFAULT_DATABASE=[master], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF

if (exists(select * from sys.database_principals p where p.type = N'S' and p.name = 'TestUser1'))
	DROP USER [TestUser1];
CREATE USER [TestUser1] FOR LOGIN [TestUser1] WITH DEFAULT_SCHEMA=[dbo]

DROP USER [TestUser1];

exec sp_adduser N'testuser1', N'testuser1';
exec sp_dropuser N'TestUser1';

exec sp_grantdbaccess N'testuser1', N'testuser1';
exec sp_revokedbaccess N'TestUser1';

CREATE USER [TestUser1] FOR LOGIN [TestUser1] WITH DEFAULT_SCHEMA=[dbo]

if (exists(select * from sys.database_principals p where p.type = N'R' and p.name = 'Role1'))
	DROP ROLE [Role1];
CREATE ROLE [Role1] AUTHORIZATION [dbo]

ALTER AUTHORIZATION ON SCHEMA::[Foo] TO [Role1]

exec sp_addrolemember 'Role1', 'TestUser1';

exec sp_droprolemember 'Role1', 'TestUser1';

DROP SCHEMA [Foo];
DROP ROLE [Role1]

DROP USER [TestUser1];
DROP LOGIN [testuser1];

