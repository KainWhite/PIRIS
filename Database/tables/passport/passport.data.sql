use master;
use banking_system;

merge into passport t
using (
	select 
        u.id [user_id],
        'MP' series,
        '7777777' number,
        N'Фрунзенское РУВД г. Минска' authority,
        '2019-08-15' date_of_issue,
        '1234567A123AB1' identification_number
    from [user] u
    where u.first_name = 'firstname'
        and u.second_name = 'secondname'
        and u.third_name = 'thirdname'
        and u.date_of_birth = '2000-01-01'
) as s (
    [user_id],
    series,
    number,
    authority,
    date_of_issue,
    identification_number
)
on t.identification_number = s.identification_number
when matched then
    update set
        [user_id] = s.[user_id],
        series = s.series,
        number = s.number,
        authority = s.authority,
        date_of_issue = s.date_of_issue
when not matched then
	insert (
		[user_id],
        series,
        number,
        authority,
        date_of_issue,
        identification_number
	)
	values (
		s.[user_id],
        s.series,
        s.number,
        s.authority,
        s.date_of_issue,
        s.identification_number
	);
		