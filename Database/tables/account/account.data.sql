use master;
use banking_system;

merge into account t
using (
	values
        (0, 0, '1010000000002', 1, 0, 0),
        (0, 100000000000, '7327000000009', 2, 0, 0)
) as s (
    debit,
    credit,
    number,
    account_type_id,
    debt,
    debt_percents
)
on t.number = s.number
when matched then
    update set
        debit = s.debit,
        credit = s.credit,
        account_type_id = s.account_type_id,
        debt = s.debt,
        debt_percents = s.debt_percents
when not matched then
	insert (
		debit,
        credit,
        number,
        account_type_id,
        debt,
        debt_percents
	)
	values (
		s.debit,
        s.credit,
        s.number,
        s.account_type_id,
        s.debt,
        s.debt_percents
	);
		