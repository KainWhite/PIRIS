using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BankingSystem.WPF.ViewModels;

namespace BankingSystem.WPF.Commands
{
    public class SaveContractCommand : ICommand
    {
        private readonly ContractViewModel _contractViewModel;
        private readonly Action<object> _actionOnExecute;

        public SaveContractCommand(ContractViewModel contractViewModel, Action<object> actionOnExecute)
        {
            _contractViewModel = contractViewModel;
            _actionOnExecute = actionOnExecute;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var errors = new List<string>();

            errors.AddRange(_contractViewModel.Contract.Validate());

            if (errors.Count > 0)
            {
                _contractViewModel.ShowErrorMessages(errors);
                return;
            }

            _actionOnExecute(parameter);
        }

        public event EventHandler CanExecuteChanged;
    }
}
