use master;
use banking_system;

merge into account t
using (
	values
        (0, 0, '1010000000002', 1),
        (0, 100000000000, '7327000000009', 2)
) as s (
    debit,
    credit,
    number,
    account_type_id
)
on t.number = s.number
when matched then
    update set
        debit = s.debit,
        credit = s.credit,
        account_type_id = s.account_type_id
when not matched then
	insert (
		debit,
        credit,
        number,
        account_type_id
	)
	values (
		s.debit,
        s.credit,
        s.number,
        s.account_type_id
	);
		