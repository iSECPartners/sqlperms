Use master;
go

use PermTest
go

if (exists( select 1 from sys.tables t where t.name = 't1') )
	drop table t1;

create table t1 ( id int, first varchar(255), last varchar(255), created datetime);

insert into t1 values ( 1, 'tim', 'test1', getdate() );

select * from t1;

update t1 set created = getdate(); -- update all rows

delete from t1; -- all rows

truncate table t1;

drop table t1;