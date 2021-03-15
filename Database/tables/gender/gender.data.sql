use master;
use banking_system;

merge into gender t
using (
	values 
		('m', 'Male'),
		('f', 'Female')
) as s (code, [description])
on t.code = s.code
when matched then
    update set
        [description] = s.[description]
when not matched then
	insert (
		code,
        [description]
	)
	values (
		s.code,
        s.[description]
	);
		