USE [master]

GO



if (exists(select * from sys.server_principals p where p.type = N'S' and p.name = 'testuser1'))
	DROP LOGIN [testuser1];

CREATE LOGIN [testuser1] WITH PASSWORD=N'password', DEFAULT_DATABASE=[master], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF



if (exists (select 1 from sys.server_principals p where p.type = N'R' and p.name = N'ServerRole-20131112-115856'))
	DROP SERVER ROLE [ServerRole-20131112-115856];


CREATE SERVER ROLE [ServerRole-20131112-115856] AUTHORIZATION [sysadmin]


GRANT ALTER,CONNECT,CONTROL,TAKE OWNERSHIP,VIEW DEFINITION
	ON ENDPOINT::[Dedicated Admin Connection] 
	TO [ServerRole-20131112-115856]


exec sp_addsrvrolemember 'TestUser1', 'ServerRole-20131112-115856';

exec sp_dropsrvrolemember 'TestUser1', 'ServerRole-20131112-115856';

DROP LOGIN [testuser1];
DROP SERVER ROLE [ServerRole-20131112-115856];