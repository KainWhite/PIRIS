using System;
using BankingSystem.Core.Enums;
using BankingSystem.EntityFramework.Repositories;
using BankingSystem.WPF.Commands.UpdateCurrentViewModelCommand;
using BankingSystem.WPF.Models;
using BankingSystem.WPF.State.Navigators;

namespace BankingSystem.WPF.ViewModels.Factories
{
    public class ViewModelAbstractFactory : IViewModelAbstractFactory
    {
        private readonly UserRepository _userRepository;
        private readonly CityRepository _cityRepository;
        private readonly DisabilityRepository _disabilityRepository;
        private readonly GenderRepository _genderRepository;
        private readonly MaritalStatusRepository _maritalStatusRepository;
        private readonly NationalityRepository _nationalityRepository;
        private readonly AccountRepository _accountRepository;
        private readonly ContractRepository _contractRepository;
        private readonly ProgramRepository _programRepository;
        private readonly DateChangeRepository _dateChangeRepository;

        public ViewModelAbstractFactory(
            UserRepository userRepository,
            CityRepository cityRepository,
            DisabilityRepository disabilityRepository,
            GenderRepository genderRepository,
            MaritalStatusRepository maritalStatusRepository,
            NationalityRepository nationalityRepository,
            AccountRepository accountRepository,
            ContractRepository contractRepository,
            ProgramRepository programRepository,
            DateChangeRepository dateChangeRepository)
        {
            _userRepository = userRepository;
            _cityRepository = cityRepository;
            _disabilityRepository = disabilityRepository;
            _genderRepository = genderRepository;
            _maritalStatusRepository = maritalStatusRepository;
            _nationalityRepository = nationalityRepository;
            _accountRepository = accountRepository;
            _contractRepository = contractRepository;
            _programRepository = programRepository;
            _dateChangeRepository = dateChangeRepository;
        }

        public ViewModelBase CreateViewModel(UpdateCurrentViewModelCommandParameter parameter, INavigator navigator)
        {
            try
            {
                switch (parameter.ViewType)
                {
                    case ViewType.UserList:
                        return new UserListViewModel(new UserListModel(_userRepository), navigator);
                    case ViewType.User:
                        return new UserViewModel(
                            new UserModel(
                                (int) parameter.Parameter,
                                _userRepository,
                                _cityRepository,
                                _disabilityRepository,
                                _genderRepository,
                                _maritalStatusRepository,
                                _nationalityRepository),
                            navigator);
                    case ViewType.AccountList:
                        return new AccountListViewModel(new AccountListModel(_accountRepository, _dateChangeRepository), navigator);
                    case ViewType.Contract:
                        return new ContractViewModel(
                            new ContractModel(_contractRepository, _programRepository, _accountRepository, _dateChangeRepository),
                            navigator);
                    case ViewType.Account:
                        return new AccountViewModel(
                            new AccountModel((int)parameter.Parameter, _accountRepository),
                            navigator);
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            catch
            {
                return new UserListViewModel(new UserListModel(_userRepository), navigator);
            }
        }
    }
}
