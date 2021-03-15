:r .\tables\passport\passport.drop.sql
:r .\tables\contract\contract.drop.sql

if exists (select * from sys.tables where [name] = 'user') begin
	drop table [user];
	print 'Table ''user'' was dropped.';
end
go