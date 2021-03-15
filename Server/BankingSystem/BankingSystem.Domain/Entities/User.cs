using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Windows.Input;
using BankingSystem.Core;
using Prism.Commands;

namespace BankingSystem.Domain.Entities
{
    [Table("user")]
    public class User : BaseEntity
    {
        public User()
        {
            Passports = new List<Passport>();
        }

        public int GenderId { get; set; } = 1;
        public int ResidenceCityId { get; set; } = 1;
        public int MaritalStatusId { get; set; } = 1;
        public int NationalityId { get; set; } = 1;

        public int? DisabilityId
        {
            get => _disabilityId;
            set => _disabilityId = value == -1 ? null : value;
        }

        public string FirstName { get; set; } = "Иванов";
        public string SecondName { get; set; } = "Иван";
        public string ThirdName { get; set; } = "Иванович";
        public DateTime DateOfBirth { get; set; } = new DateTime(2000, 1, 1);
        public string ResidenceAddress { get; set; } = "пр-т Независимости, 6";
        public string HomePhoneNumber { get; set; } = "1234567";
        public string MobilePhoneNumber { get; set; } = "121234567";
        public string Email { get; set; } = "a@a.com";
        public string RegistrationAddress { get; set; } = "пр-т Независимости, 7";
        public bool IsRetiree { get; set; } = true;
        public decimal? MonthlyIncome { get; set; } = null;

        public Gender Gender { get; set; }
        public City ResidenceCity { get; set; }
        public MaritalStatus MaritalStatus { get; set; }
        public Nationality Nationality { get; set; }
        public Disability Disability { get; set; }
        public List<Passport> Passports { get; set; }

        private int? _disabilityId = 1;

        public override List<string> Validate()
        {
            var errors = new List<string>();

            if (string.IsNullOrEmpty(FirstName))
            {
                errors.Add($"First name must be specified");
            }
            else if (!Regex.IsMatch(FirstName, RegexConstants.UserName))
            {
                errors.Add($"First name {FirstName} is invalid");
            }

            if (string.IsNullOrEmpty(SecondName))
            {
                errors.Add($"Second name must be specified");
            }
            else if (!Regex.IsMatch(SecondName, RegexConstants.UserName))
            {
                errors.Add($"Second name {SecondName} is invalid");
            }

            if (string.IsNullOrEmpty(ThirdName))
            {
                errors.Add($"Third name must be specified");
            }
            else if (!Regex.IsMatch(ThirdName, RegexConstants.UserName))
            {
                errors.Add($"Third name {ThirdName} is invalid");
            }

            if (string.IsNullOrEmpty(ResidenceAddress))
            {
                errors.Add($"Residence address must be specified");
            }
            if (string.IsNullOrEmpty(RegistrationAddress))
            {
                errors.Add($"Registration address must be specified");
            }

            //if (string.IsNullOrEmpty(HomePhoneNumber))
            //{
            //    errors.Add($"Home phone number must be specified");
            //}
            //else
            if (!string.IsNullOrEmpty(HomePhoneNumber) && !Regex.IsMatch(HomePhoneNumber, RegexConstants.HomePhoneNumber))
            {
                errors.Add($"Home phone number {HomePhoneNumber} is invalid");
            }

            //if (string.IsNullOrEmpty(MobilePhoneNumber))
            //{
            //    errors.Add($"Mobile phone number must be specified");
            //}
            //else
            if (!string.IsNullOrEmpty(MobilePhoneNumber) && !Regex.IsMatch(MobilePhoneNumber, RegexConstants.MobilePhoneNumber))
            {
                errors.Add($"Mobile phone number {MobilePhoneNumber} is invalid");
            }

            //if (string.IsNullOrEmpty(Email))
            //{
            //    errors.Add($"Email must be specified");
            //}
            if (!string.IsNullOrEmpty(Email))
            {
                try
                {
                    MailAddress m = new MailAddress(Email);

                }
                catch (FormatException)
                {
                    errors.Add($"Email {Email} is invalid");
                }
            }

            if (Passports.Count > 0)
            {
                foreach (var passport in Passports)
                {
                    errors.AddRange(passport.Validate());
                }
            }
            else
            {
                errors.Add($"User must have a passport");
            }

            return errors;
        }
    }
}
