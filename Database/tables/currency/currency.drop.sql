:r .\tables\program\program.drop.sql

if exists (select * from sys.tables where [name] = 'currency') begin
	drop table currency;
	print 'Table ''currency'' was dropped.';
end
go
