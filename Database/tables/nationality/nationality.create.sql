create table nationality (
	id int identity(1, 1),
	[name] nvarchar(100) not null,
	primary key (id),
);

print 'Table ''nationality'' was created.';
