use master;
use banking_system;

merge into disability t
using (
	values 
		('First group'),
		('Second group'),
		('Third group')
) as s ([name])
on t.[name] = s.[name]
when not matched then
	insert (
		[name]
	)
	values (
		s.[name]
	);
		