using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BankingSystem.Core.Enums;
using BankingSystem.WPF.Commands;
using BankingSystem.WPF.Commands.UpdateCurrentViewModelCommand;
using BankingSystem.WPF.ViewModels;
using BankingSystem.WPF.ViewModels.Factories;
using Prism.Mvvm;

namespace BankingSystem.WPF.State.Navigators
{
    public class Navigator : BindableBase, INavigator
    {
        public ViewModelBase CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel = value;
                RaisePropertyChanged();
            }
        }
        public ICommand UpdateCurrentViewModelCommand { get; set; }

        private ViewModelBase _currentViewModel;

        public Navigator(IViewModelAbstractFactory viewModelAbstractFactory)
        {
            UpdateCurrentViewModelCommand = new UpdateCurrentViewModelCommand(parameter =>
            {
                CurrentViewModel = viewModelAbstractFactory.CreateViewModel(parameter as UpdateCurrentViewModelCommandParameter, this);
            });

            CurrentViewModel = viewModelAbstractFactory.CreateViewModel(new UpdateCurrentViewModelCommandParameter
            {
                ViewType = ViewType.UserList,
                Parameter = null,
            }, this);
        }
    }
}
