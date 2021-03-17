using System;
using System.Collections.Generic;
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
    public class ContractViewModel : ViewModelBase
    {
        #region public properties

        public Contract Contract => _model.Contract;
        public IEnumerable<Program> Programs => _model.Programs;
        public ICommand GoBackCommand { get; set; }
        public ICommand SaveContractCommand { get; set; }

        #endregion

        #region private

        private readonly ContractModel _model;

        #endregion
        
        public ContractViewModel(ContractModel model, INavigator navigator)
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

            SaveContractCommand = new SaveContractCommand(this, parameter =>
            {
                _model.CreateContract();
                GoBackCommand.Execute(parameter);
            });
        }
    }
}
