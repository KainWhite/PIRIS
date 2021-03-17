create table [contract] (
    id int identity(1, 1),
    [user_id] int not null,
    current_account_id int not null,
    percent_account_id int not null,
    program_id int not null,
    number int not null,
    conclusion_date datetime2 not null,
    amount money not null,

	primary key (id),
    constraint FK_contract_user foreign key ([user_id]) references [user](id),
	constraint FK_contract_current_account foreign key (current_account_id) references account(id),
	constraint FK_contract_percent_account foreign key (percent_account_id) references account(id),
	constraint FK_contract_program foreign key (program_id) references program(id),
);

print 'Table ''contract'' was created.';
