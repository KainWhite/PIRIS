create table account (
	id int identity(1, 1),
    debit money not null,
    credit money not null,
    number nvarchar(13) not null,
    account_type_id int not null,

    primary key (id),
    constraint FK_account_account_type foreign key (account_type_id) references account_type(id),
    constraint UX_account_number UNIQUE (number),
);

print 'Table ''account'' was created.';
