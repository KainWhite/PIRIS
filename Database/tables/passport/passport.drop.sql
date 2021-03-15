if exists (select * from sys.tables where [name] = 'passport') begin
	drop table passport;
	print 'Table ''passport'' was dropped.';
end
go