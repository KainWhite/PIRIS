using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Prism.Mvvm;

namespace BankingSystem.WPF.ViewModels
{
    public class ViewModelBase : BindableBase
    {
        public void ShowErrorMessages(List<string> errors)
        {
            string errorMessage = string.Join(Environment.NewLine, errors);
            MessageBox.Show(errorMessage, "BankingSystem", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
