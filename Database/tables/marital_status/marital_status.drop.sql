:r .\tables\user\user.drop.sql

if exists (select * from sys.tables where [name] = 'marital_status') begin
	drop table marital_status;
	print 'Table ''marital_status'' was dropped.';
end
go
