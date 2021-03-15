using System;
using System.Globalization;
using System.Windows.Data;
using BankingSystem.Core.Enums;
using BankingSystem.WPF.Commands.UpdateCurrentViewModelCommand;

namespace BankingSystem.WPF.Converters
{
    public class UpdateCurrentViewModelCommandParameterConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length > 1 && values[0] is ViewType viewType)
            {
                return new UpdateCurrentViewModelCommandParameter
                {
                    ViewType = viewType,
                    Parameter = values[1],
                };
            }
            throw new ArgumentException("Unable to convert passed values to UpdateCurrentViewModelCommandParameter.");
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
