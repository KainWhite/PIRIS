using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BankingSystem.Core.Enums;
using BankingSystem.Domain.Entities;
using BankingSystem.EntityFramework.Repositories;
using BankingSystem.WPF.Commands;
using BankingSystem.WPF.Commands.UpdateCurrentViewModelCommand;
using BankingSystem.WPF.Models;
using BankingSystem.WPF.State.Navigators;
using Prism.Commands;

namespace BankingSystem.WPF.ViewModels
{
    public class UserViewModel : ViewModelBase
    {
        #region public properties

        public User User => _userModel.User;
        public IEnumerable<City> Cities => _userModel.Cities;
        public IEnumerable<Disability> Disabilities => _userModel.Disabilities;
        public IEnumerable<Gender> Genders => _userModel.Genders;
        public IEnumerable<MaritalStatus> MaritalStatuses => _userModel.MaritalStatuses;
        public IEnumerable<Nationality> Nationalities => _userModel.Nationalities;
        public SaveUserType SaveUserType => _userModel.SaveUserType;
        public ICommand GoBackCommand { get; set; }
        public ICommand SaveUserCommand { get; set; }
        public ICommand DeleteUserCommand { get; set; }

        #endregion

        #region private

        private readonly UserModel _userModel;

        #endregion

        public UserViewModel(UserModel userModel, INavigator navigator)
        {
            _userModel = userModel;
            _userModel.PropertyChanged += (sender, e) => RaisePropertyChanged(e.PropertyName);

            GoBackCommand = new DelegateCommand<object>(parameter =>
            {
                navigator.UpdateCurrentViewModelCommand.Execute(new UpdateCurrentViewModelCommandParameter
                {
                    ViewType = ViewType.UserList,
                    Parameter = parameter,
                });
            });

            SaveUserCommand = new SaveUserCommand(this, parameter =>
            {
                switch (SaveUserType)
                {
                    case SaveUserType.Create:
                        _userModel.CreateUser().ContinueWith(ContinueSavingUser);
                        break;
                    case SaveUserType.Update:
                        _userModel.UpdateUser().ContinueWith(ContinueSavingUser);
                        break;
                }
            });

            DeleteUserCommand = new DelegateCommand<object>(parameter =>
            {
                _userModel.DeleteUser().Wait();
                navigator.UpdateCurrentViewModelCommand.Execute(new UpdateCurrentViewModelCommandParameter
                {
                    ViewType = ViewType.UserList,
                    Parameter = parameter,
                });
            });
        }

        private void ContinueSavingUser(Task task)
        {
            if (task.Status == TaskStatus.Faulted)
            {
                ShowErrorMessages(new List<string>()
                {
                    $"Cannot save user with passport with series {User.Passports[0].Series} {User.Passports[0].Number} and identification number {User.Passports[0].IdentificationNumber} because such passport already belongs to another client"
                });
            }
        }
    }
}
