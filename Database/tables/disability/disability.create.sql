create table disability (
	id int identity(1, 1),
	[name] nvarchar(100) not null,
	[description] nvarchar(100) null,
	primary key (id),
);

print 'Table ''disability'' was created.';
