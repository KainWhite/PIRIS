using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankingSystem.Core.Enums;
using BankingSystem.WPF.Commands.UpdateCurrentViewModelCommand;
using BankingSystem.WPF.State.Navigators;

namespace BankingSystem.WPF.ViewModels.Factories
{
    public interface IViewModelAbstractFactory
    {
        public ViewModelBase CreateViewModel(UpdateCurrentViewModelCommandParameter parameter, INavigator navigator);
    }
}
