create table program (
	id int identity(1, 1),
    currency_id int not null,
    program_type_id int not null,
    [name] nvarchar(100) not null, 
    [percent] decimal(5, 2) not null,
    term_month_count int not null,
    date_start datetime2 not null,
    date_end datetime2 not null,
	
	primary key (id),
    constraint FK_program_currency foreign key (currency_id) references currency(id),
    constraint FK_program_program_type foreign key (program_type_id) references program_type(id),
);

print 'Table ''program'' was created.';
