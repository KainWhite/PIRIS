create table passport (
	id int identity(1, 1),
    [user_id] int not null,
	series nvarchar(2) not null,
	number nvarchar(7) not null,
	authority nvarchar(100) not null,
	date_of_issue datetime2 not null,
	identification_number varchar(14) not null,

	primary key (id),
    constraint FK_passport_user foreign key ([user_id]) references [user](id)
        on delete cascade
        on update cascade,
    constraint ux_passport_identification_number UNIQUE (identification_number),
	constraint ux_passport_series_number UNIQUE (series, number)
);

print 'Table ''passport'' was created.';
