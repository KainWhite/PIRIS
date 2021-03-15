create table city (
	id int identity(1, 1),
	[name] nvarchar(100) not null,
	primary key (id),
);

print 'Table ''city'' was created.';
