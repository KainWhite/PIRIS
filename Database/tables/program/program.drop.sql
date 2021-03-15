:r .\tables\contract\contract.drop.sql

if exists (select * from sys.tables where [name] = 'program') begin
	drop table program;
	print 'Table ''program'' was dropped.';
end
go