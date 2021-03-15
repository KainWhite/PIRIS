:r .\tables\user\user.drop.sql

if exists (select * from sys.tables where [name] = 'city') begin
	drop table city;
	print 'Table ''city'' was dropped.';
end
go
