using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;
using BankingSystem.Core;

namespace BankingSystem.Domain.Entities
{
    [Table("passport")]
    public class Passport : BaseEntity
    {
        public int UserId { get; set; }
        public string Series { get; set; } = "MP";
        public string Number { get; set; } = "1234567";
        public string Authority { get; set; } = "Фрунзенское РУВД, г. Минска";
        public DateTime DateOfIssue { get; set; } = new DateTime(2019, 1, 1);
        public string IdentificationNumber { get; set; } = "7777777A123AB1";

        public User User { get; set; }

        public override List<string> Validate()
        {
            var errors = new List<string>();

            if (string.IsNullOrEmpty(Authority))
            {
                errors.Add($"Passport authority must be specified");
            }
            if (DateOfIssue.AddYears(10) < DateTime.Now)
            {
                errors.Add($"Passport with date of issue {DateOfIssue.Date} has expired already");
            }

            if (string.IsNullOrEmpty(Series))
            {
                errors.Add($"Passport series must be specified");
            }
            else if (!Regex.IsMatch(Series, RegexConstants.PassportSeries))
            {
                errors.Add($"Passport series {Series} is invalid");
            }

            if (string.IsNullOrEmpty(Number))
            {
                errors.Add($"Passport number must be specified");
            }
            else if (!Regex.IsMatch(Number, RegexConstants.PassportNumber))
            {
                errors.Add($"Passport number {Number} is invalid");
            }

            if (string.IsNullOrEmpty(IdentificationNumber))
            {
                errors.Add($"Passport identificetion number must be specified");
            }
            else if (!Regex.IsMatch(IdentificationNumber, RegexConstants.PassportIdentificationNumber))
            {
                errors.Add($"Passport identificetion number {IdentificationNumber} is invalid");
            }

            return errors;
        }
    }
}
