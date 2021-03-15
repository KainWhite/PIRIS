using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BankingSystem.WPF.Commands
{
    public class DataGridEditCommand : ICommand
    {
        private readonly Action<object> _actionOnExecute;

        public DataGridEditCommand(Action<object> actionOnExecute)
        {
            _actionOnExecute = actionOnExecute;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _actionOnExecute(parameter);
        }

        public event EventHandler CanExecuteChanged;
    }
}
