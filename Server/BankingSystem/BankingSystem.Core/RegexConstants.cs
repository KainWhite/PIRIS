namespace BankingSystem.Core
{
    public static class RegexConstants
    {
        public const string UserName = @"^[a-zA-Zа-яА-Я]+$";
        public const string HomePhoneNumber = @"^(8017)?[1-9]{7}$";
        public const string MobilePhoneNumber = @"^(\+375)?[1-9]{9}$";
        public const string PassportSeries = @"^[A-Z]{2}$";
        public const string PassportNumber = @"^[1-9]{7}$";
        public const string PassportIdentificationNumber = @"^[1-9]{7}[A-Z]{1}[1-9]{3}[A-Z]{2}[1-9]{1}$";
    }
}
