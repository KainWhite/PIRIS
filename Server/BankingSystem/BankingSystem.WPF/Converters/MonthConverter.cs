using System;
using System.Globalization;
using System.Windows.Data;
using BankingSystem.Core.Extensions;

namespace BankingSystem.WPF.Converters
{
    public class MonthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.IsNumeric())
            {
                var count = (int)value;
                return $"{count} " + (count == 1 ? "month" : "months");
            }
            throw new NotImplementedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
