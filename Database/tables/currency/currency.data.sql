use master;
use banking_system;

merge into currency t
using (
	values 
		('BYN')
) as s (code)
on t.code = s.code
when not matched then
	insert (
		code
	)
	values (
		s.code
	);
		