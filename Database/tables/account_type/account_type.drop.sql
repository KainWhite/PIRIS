:r .\tables\account\account.drop.sql

if exists (select * from sys.tables where [name] = 'account_type') begin
	drop table account_type;
	print 'Table ''account_type'' was dropped.';
end
go
