create table gender (
	id int identity(1, 1),
	code nvarchar(2) not null,
	[description] nvarchar(100) not null,
	primary key (id),
);

print 'Table ''gender'' was created.';
