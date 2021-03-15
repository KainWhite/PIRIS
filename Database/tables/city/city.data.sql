use master;
use banking_system;

merge into city t
using (
	values 
		('Minsk'),
		('Vitebsk'),
		('Grodno'),
		('Gomel'),
		('Brest'),
		('Mogilyov')
) as s ([name])
on t.[name] = s.[name]
when not matched then
	insert (
		[name]
	)
	values (
		s.[name]
	);
		