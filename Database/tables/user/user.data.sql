use master;
use banking_system;

merge into [user] t
using (
	select 
        'firstname' first_name,
        'secondname' second_name,
        'thirdname' third_name,
        '2000-01-01' date_of_birth,
        g.id gender_id,
        c.id residence_city_id,
        'Nesavisimiosti pt., 1' residence_address,
        '7777777' home_phone_number,
        '1234567' mobile_phone_number,
        'a@a.com' email,
        'Nesavisimiosti pt., 2' registration_address,
        ms.id marital_status_id,
        n.id nationality_id,
        null disability_id,
        0 is_retiree,
        2500 monthly_income
    from gender g
    cross join city c
    cross join marital_status ms
    cross join nationality n
    cross join disability d
    where g.code = 'M'
        and c.[name] = 'Minsk'
        and ms.[description] = 'Single'
        and n.[name] = 'Belarusian'
        and d.[name] = 'First group'
) as s (
    first_name,
	second_name,
	third_name,
	date_of_birth,
	gender_id,
	residence_city_id,
	residence_address,
	home_phone_number,
	mobile_phone_number,
	email,
	registration_address,
	marital_status_id,
	nationality_id,
	disability_id,
	is_retiree,
	monthly_income
)
on t.first_name = s.first_name
    and t.second_name = s.second_name
    and t.third_name = s.third_name
    and t.date_of_birth = s.date_of_birth
when matched then
    update set
        first_name = s.first_name,
	    second_name = s.second_name,
	    third_name = s.third_name,
	    date_of_birth = s.date_of_birth,
	    gender_id = s.gender_id,
	    residence_city_id = s.residence_city_id,
	    residence_address = s.residence_address,
	    home_phone_number = s.home_phone_number,
	    mobile_phone_number = s.mobile_phone_number,
	    email = s.email,
	    registration_address = s.registration_address,
	    marital_status_id = s.marital_status_id,
	    nationality_id = s.nationality_id,
	    disability_id = s.disability_id,
	    is_retiree = s.is_retiree,
	    monthly_income = s.monthly_income
when not matched then
	insert (
		first_name,
	    second_name,
	    third_name,
	    date_of_birth,
	    gender_id,
	    residence_city_id,
	    residence_address,
	    home_phone_number,
	    mobile_phone_number,
	    email,
	    registration_address,
	    marital_status_id,
	    nationality_id,
	    disability_id,
	    is_retiree,
	    monthly_income
	)
	values (
		s.first_name,
	    s.second_name,
	    s.third_name,
	    s.date_of_birth,
	    s.gender_id,
	    s.residence_city_id,
	    s.residence_address,
	    s.home_phone_number,
	    s.mobile_phone_number,
	    s.email,
	    s.registration_address,
	    s.marital_status_id,
	    s.nationality_id,
	    s.disability_id,
	    s.is_retiree,
	    s.monthly_income
	);
		