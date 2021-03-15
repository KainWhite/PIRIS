if exists (select * from sys.tables where [name] = 'contract') begin
	drop table [contract];
	print 'Table ''contract'' was dropped.';
end
go