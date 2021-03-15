using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using BankingSystem.Core.Enums;
using BankingSystem.Domain.Entities;
using BankingSystem.WPF.Commands;
using BankingSystem.WPF.Commands.UpdateCurrentViewModelCommand;
using BankingSystem.WPF.Models;
using BankingSystem.WPF.State.Navigators;
using Prism.Commands;

namespace BankingSystem.WPF.ViewModels
{
    public class UserListViewModel : ViewModelBase
    {
        public BindingList<User> Users => _userListModel.Users;

        public ICommand UserClickCommand { get; set; }
        public ICommand CreateUserCommand { get; set; }

        //private readonly INavigator _navigator;
        private readonly UserListModel _userListModel;

        public UserListViewModel(UserListModel userListModel, INavigator navigator)
        {
            _userListModel = userListModel;
            //_navigator = navigator;

            UserClickCommand = new UserClickCommand(parameter =>
            {
                navigator.UpdateCurrentViewModelCommand.Execute(new UpdateCurrentViewModelCommandParameter
                {
                    ViewType = ViewType.User,
                    Parameter = parameter,
                });
            });

            CreateUserCommand = new DelegateCommand<object>(parameter =>
            {
                navigator.UpdateCurrentViewModelCommand.Execute(new UpdateCurrentViewModelCommandParameter
                {
                    ViewType = ViewType.User,
                    Parameter = 0,
                });
            });
        }
    }
}