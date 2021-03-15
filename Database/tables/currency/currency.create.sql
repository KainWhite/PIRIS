create table currency (
	id int identity(1, 1),
	code nvarchar(100) not null,
	primary key (id),
);

print 'Table ''currency'' was created.';
