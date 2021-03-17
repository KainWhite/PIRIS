using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BankingSystem.Core;
using BankingSystem.Core.Enums;
using BankingSystem.Domain.Entities;
using BankingSystem.WPF.Commands.UpdateCurrentViewModelCommand;
using BankingSystem.WPF.Models;
using BankingSystem.WPF.State.Navigators;
using Prism.Commands;

namespace BankingSystem.WPF.ViewModels
{
    public class AccountViewModel : ViewModelBase
    {
        public Account Account => _model.Account;
        public Account RelatedAccount => _model.RelatedAccount;
        public BindingList<DebtScheduleItem> DebtSchedule => _model.DebtSchedule;
        public ICommand GoBackCommand { get; set; }
        public ICommand WithdrawCommand { get; set; }
        public bool ShowDebtSchedule => _model.ShowDebtSchedule;
        public bool ShowWithdraw => _model.ShowWithdraw;

        private readonly AccountModel _model;

        public AccountViewModel(AccountModel model, INavigator navigator)
        {
            _model = model;
            _model.PropertyChanged += (sender, e) => RaisePropertyChanged(e.PropertyName);

            GoBackCommand = new DelegateCommand<object>(parameter =>
            {
                navigator.UpdateCurrentViewModelCommand.Execute(new UpdateCurrentViewModelCommandParameter
                {
                    ViewType = ViewType.AccountList,
                    Parameter = parameter,
                });
            });

            WithdrawCommand = new DelegateCommand<object>(parameter => { _model.WithdrawAccount(); });
        }
    }
}
