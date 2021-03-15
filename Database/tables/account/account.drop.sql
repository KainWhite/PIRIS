:r .\tables\contract\contract.drop.sql

if exists (select * from sys.tables where [name] = 'account') begin
	drop table account;
	print 'Table ''account'' was dropped.';
end
go