:r .\tables\user\user.drop.sql

if exists (select * from sys.tables where [name] = 'nationality') begin
	drop table nationality;
	print 'Table ''nationality'' was dropped.';
end
go
