use master;
use banking_system;

merge into account_type t
using (
	values
        ('Cashier'),
        ('Bank development fund'),
		('Current account'),
		('Percent account')
) as s ([name])
on t.[name] = s.[name]
when not matched then
	insert (
		[name]
	)
	values (
		s.[name]
	);
		