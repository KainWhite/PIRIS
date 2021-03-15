create table marital_status (
	id int identity(1, 1),
	[description] nvarchar(100) not null,
	primary key (id),
);

print 'Table ''marital_status'' was created.';
