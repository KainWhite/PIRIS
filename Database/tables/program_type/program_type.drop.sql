:r .\tables\program\program.drop.sql

if exists (select * from sys.tables where [name] = 'program_type') begin
	drop table program_type;
	print 'Table ''program_type'' was dropped.';
end
go
