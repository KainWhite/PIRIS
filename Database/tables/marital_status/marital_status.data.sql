use master;
use banking_system;

merge into marital_status t
using (
	values 
		('Single'),
		('Married'),
		('Separated'),
		('Divorced'),
		('Widowed')
) as s ([description])
on t.[description] = s.[description]
when not matched then
	insert (
		[description]
	)
	values (
		s.[description]
	);
		