using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BankingSystem.Core.Enums;
using BankingSystem.Domain.Entities;
using BankingSystem.WPF.Commands;
using BankingSystem.WPF.Commands.UpdateCurrentViewModelCommand;
using BankingSystem.WPF.Models;
using BankingSystem.WPF.State.Navigators;
using Prism.Commands;

namespace BankingSystem.WPF.ViewModels
{
    public class AccountListViewModel : ViewModelBase
    {
        public BindingList<Account> Accounts => _model.Accounts;
        public DateChange DateChange => _model.DateChange;

        public ICommand AccountClickCommand { get; set; }
        public ICommand CreateContractCommand { get; set; }
        public ICommand CurrentDateChangedCommand { get; set; }

        private readonly AccountListModel _model;

        public AccountListViewModel(AccountListModel model, INavigator navigator)
        {
            _model = model;
            _model.PropertyChanged += (sender, e) => RaisePropertyChanged(e.PropertyName);

            AccountClickCommand = new DelegateCommand<object>(parameter =>
            {
                navigator.UpdateCurrentViewModelCommand.Execute(new UpdateCurrentViewModelCommandParameter
                {
                    ViewType = ViewType.Account,
                    Parameter = parameter,
                });
            });

            CreateContractCommand = new DelegateCommand<object>(parameter =>
            {
                navigator.UpdateCurrentViewModelCommand.Execute(new UpdateCurrentViewModelCommandParameter
                {
                    ViewType = ViewType.Contract,
                    Parameter = 0,
                });
            });

            CurrentDateChangedCommand = new DelegateCommand<object>(parameter =>
            {
                if (parameter is DateTime currentDate)
                {
                    _model.ShiftTimeToCurrentDate(currentDate);
                }
            });
        }
    }
}
