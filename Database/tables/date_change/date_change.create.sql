create table date_change (
	id int identity(1, 1),
	[current_date] datetime2 not null,
	primary key (id),
);

print 'Table ''date_change'' was created.';
