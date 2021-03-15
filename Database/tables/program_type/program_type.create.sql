create table program_type (
	id int identity(1, 1),
	[name] nvarchar(100) not null,
	primary key (id),
);

print 'Table ''program_type'' was created.';
