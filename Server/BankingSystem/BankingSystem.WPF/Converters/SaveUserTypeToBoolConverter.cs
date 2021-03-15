using System;
using System.Globalization;
using System.Windows.Data;
using BankingSystem.Core.Enums;

namespace BankingSystem.WPF.Converters
{
    public class SaveUserTypeToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is SaveUserType saveUserType)
            {
                switch (saveUserType)
                {
                    case SaveUserType.Create:
                        return false;
                    case SaveUserType.Update:
                        return true;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            throw new NotImplementedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool deleteEnabled)
            {
                switch (deleteEnabled)
                {
                    case true:
                        return SaveUserType.Update;
                    default:
                        return SaveUserType.Create;
                }
            }
            throw new NotImplementedException();
        }
    }
}
