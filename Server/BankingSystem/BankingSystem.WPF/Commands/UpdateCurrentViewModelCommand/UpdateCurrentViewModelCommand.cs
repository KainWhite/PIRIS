using System;
using System.Windows.Input;
using BankingSystem.Core.Enums;
using BankingSystem.WPF.State.Navigators;
using BankingSystem.WPF.ViewModels.Factories;

namespace BankingSystem.WPF.Commands.UpdateCurrentViewModelCommand
{
    public class UpdateCurrentViewModelCommand : ICommand
    {
        private readonly Action<object> _actionOnExecute;

        public UpdateCurrentViewModelCommand(Action<object> actionOnExecute)
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
