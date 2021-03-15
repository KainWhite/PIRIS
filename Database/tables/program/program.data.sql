use master;
use banking_system;

merge into program t
using (
	values
        (1, 1, 'Irrevocable deposit', 17.5, 3, '2020-01-01', '2030-01-01'),
        (1, 1, 'Revocable deposit', 18.2, 6, '2020-01-01', '2030-01-01'),
        (1, 2, 'Credit with annuity repayment method', 17, 12, '2020-01-01', '2030-01-01'),
        (1, 2, 'Credit with differential repayment method', 17, 12, '2020-01-01', '2030-01-01')
) as s (
    currency_id,
    program_type_id,
    [name],
    [percent],
    term_month_count,
    date_start,
    date_end
)
on t.[name] = s.[name]
when matched then
    update set
        currency_id = s.currency_id,
        program_type_id = s.program_type_id,
        [percent] = s.[percent],
        term_month_count = s.term_month_count,
        date_start = s.date_start,
        date_end = s.date_end
when not matched then
	insert (
		currency_id,
        program_type_id,
        [name],
        [percent],
        term_month_count,
        date_start,
        date_end
	)
	values (
		s.currency_id,
        s.program_type_id,
        s.[name],
        s.[percent],
        s.term_month_count,
        s.date_start,
        s.date_end
	);
		