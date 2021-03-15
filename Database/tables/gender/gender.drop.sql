:r .\tables\user\user.drop.sql

if exists (select * from sys.tables where [name] = 'gender') begin
	drop table gender;
	print 'Table ''gender'' was dropped.';
end
go
