use master;
use banking_system;

merge into program_type t
using (
	values
        ('Deposit'),
		('Credit')
) as s ([name])
on t.[name] = s.[name]
when not matched then
	insert (
		[name]
	)
	values (
		s.[name]
	);
		