:r .\tables\user\user.drop.sql

if exists (select * from sys.tables where [name] = 'disability') begin
	drop table disability;
	print 'Table ''disability'' was dropped.';
end
go