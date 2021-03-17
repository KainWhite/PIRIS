if exists (select * from sys.tables where [name] = 'date_change') begin
	drop table date_change;
	print 'Table ''date_change'' was dropped.';
end
go