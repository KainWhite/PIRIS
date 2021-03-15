using System;
using System.Collections.Generic;

#nullable disable

namespace BankingSystem.EntityFramework.Models
{
    public partial class User
    {
        public User()
        {
            Contracts = new HashSet<Contract>();
            Passports = new HashSet<Passport>();
        }

        public int Id { get; set; }
        public int GenderId { get; set; }
        public int ResidenceCityId { get; set; }
        public int MaritalStatusId { get; set; }
        public int NationalityId { get; set; }
        public int? DisabilityId { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string ResidenceAddress { get; set; }
        public string HomePhoneNumber { get; set; }
        public string MobilePhoneNumber { get; set; }
        public string Email { get; set; }
        public string RegistrationAddress { get; set; }
        public bool IsRetiree { get; set; }
        public decimal? MonthlyIncome { get; set; }

        public virtual Disability Disability { get; set; }
        public virtual Gender Gender { get; set; }
        public virtual MaritalStatus MaritalStatus { get; set; }
        public virtual Nationality Nationality { get; set; }
        public virtual City ResidenceCity { get; set; }
        public virtual ICollection<Contract> Contracts { get; set; }
        public virtual ICollection<Passport> Passports { get; set; }
    }
}
