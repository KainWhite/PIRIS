using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BankingSystem.Core.Enums;
using BankingSystem.Domain.Entities;
using BankingSystem.WPF.Models;
using BankingSystem.WPF.ViewModels;

namespace BankingSystem.WPF.Commands
{
    public class SaveUserCommand : ICommand
    {
        private readonly UserViewModel _userViewModel;
        private readonly Action<object> _actionOnExecute;

        public SaveUserCommand(UserViewModel userViewModel, Action<object> actionOnExecute)
        {
            _userViewModel = userViewModel;
            _actionOnExecute = actionOnExecute;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var genderIds = _userViewModel.Genders.Select(x => x.Id).ToList();
            var cityIds = _userViewModel.Cities.Select(x => x.Id).ToList();
            var maritalStatusIds = _userViewModel.MaritalStatuses.Select(x => x.Id).ToList();
            var nationalityIds = _userViewModel.Nationalities.Select(x => x.Id).ToList();
            var disabilityIds = _userViewModel.Disabilities.Select(x => x.Id).ToList();

            var errors = new List<string>();

            if (!genderIds.Contains(_userViewModel.User.GenderId))
            {
                errors.Add($"There is no gender with ID {_userViewModel.User.GenderId}");
            }
            if (!cityIds.Contains(_userViewModel.User.ResidenceCityId))
            {
                errors.Add($"There is no city with ID {_userViewModel.User.ResidenceCityId}");
            }
            if (!maritalStatusIds.Contains(_userViewModel.User.MaritalStatusId))
            {
                errors.Add($"There is no marital status with ID {_userViewModel.User.MaritalStatusId}");
            }
            if (!nationalityIds.Contains(_userViewModel.User.NationalityId))
            {
                errors.Add($"There is no nationality with ID {_userViewModel.User.NationalityId}");
            }
            if (_userViewModel.User.DisabilityId.HasValue && !disabilityIds.Contains(_userViewModel.User.DisabilityId.Value))
            {
                errors.Add($"There is no gender with ID {_userViewModel.User.DisabilityId.Value}");
            }

            errors.AddRange(_userViewModel.User.Validate());

            if (errors.Count > 0)
            {
                _userViewModel.ShowErrorMessages(errors);
                return;
            }

            _actionOnExecute(parameter);
        }

        public event EventHandler CanExecuteChanged;
    }
}
