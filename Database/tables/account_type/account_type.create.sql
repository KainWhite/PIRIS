create table account_type (
	id int identity(1, 1),
	[name] nvarchar(100) not null,
	primary key (id),
);

print 'Table ''account_type'' was created.';
