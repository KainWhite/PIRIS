using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BankingSystem.Core.Enums;
using BankingSystem.Domain.Entities;
using BankingSystem.EntityFramework.Repositories;
using Prism.Mvvm;

namespace BankingSystem.WPF.Models
{
    public class UserModel : BindableBase
    {
        #region public properties

        public User User
        {
            get => _user;
            set
            {
                _user = value;
                RaisePropertyChanged();
            }
        }
        public IEnumerable<City> Cities
        {
            get => _cities;
            set
            {
                _cities = value;
                RaisePropertyChanged();
            }
        }
        public IEnumerable<Disability> Disabilities
        {
            get => _disabilities;
            set
            {
                _disabilities = value;
                RaisePropertyChanged();
            }
        }
        public IEnumerable<Gender> Genders
        {
            get => _genders;
            set
            {
                _genders = value;
                RaisePropertyChanged();
            }
        }
        public IEnumerable<MaritalStatus> MaritalStatuses
        {
            get => _maritalStatuses;
            set
            {
                _maritalStatuses = value;
                RaisePropertyChanged();
            }
        }
        public IEnumerable<Nationality> Nationalities
        {
            get => _nationalities;
            set
            {
                _nationalities = value;
                RaisePropertyChanged();
            }
        }
        public SaveUserType SaveUserType
        {
            get => _saveUserType;
            set
            {
                _saveUserType = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region private

        private User _user;
        private IEnumerable<City> _cities;
        private IEnumerable<Disability> _disabilities;
        private IEnumerable<Gender> _genders;
        private IEnumerable<MaritalStatus> _maritalStatuses;
        private IEnumerable<Nationality> _nationalities;
        private SaveUserType _saveUserType;

        private readonly UserRepository _userRepository;
        private readonly CityRepository _cityRepository;
        private readonly DisabilityRepository _disabilityRepository;
        private readonly GenderRepository _genderRepository;
        private readonly MaritalStatusRepository _maritalStatusRepository;
        private readonly NationalityRepository _nationalityRepository;

        #endregion

        public UserModel(
            int userId,
            UserRepository userRepository,
            CityRepository cityRepository,
            DisabilityRepository disabilityRepository,
            GenderRepository genderRepository,
            MaritalStatusRepository maritalStatusRepository,
            NationalityRepository nationalityRepository)
        {
            _userRepository = userRepository;
            _cityRepository = cityRepository;
            _disabilityRepository = disabilityRepository;
            _genderRepository = genderRepository;
            _maritalStatusRepository = maritalStatusRepository;
            _nationalityRepository = nationalityRepository;

            FillData(userId);
        }

        public async Task CreateUser()
        {
            User = await _userRepository.CreateAsync(User);
            SaveUserType = SaveUserType.Update;
        }

        public async Task<User> GetUser(int userId)
        {
            var user = await _userRepository
                .GetAsync(userId, new List<Expression<Func<User, object>>>
                {
                    x => x.Gender,
                    x => x.ResidenceCity,
                    x => x.Disability,
                    x => x.MaritalStatus,
                    x => x.Nationality,
                    x => x.Passports
                })
                .ConfigureAwait(false);
            if (user.Passports.Count == 0)
            {
                user.Passports.Add(new Passport());
            }
            return user;
        }

        public async Task UpdateUser()
        {
            User = await _userRepository.UpdateAsync(User).ConfigureAwait(false);
        }

        public async Task DeleteUser()
        {
            await _userRepository.DeleteAsync(User.Id).ConfigureAwait(false);
        }

        public void FillData(int userId)
        {
            _cityRepository.GetAllAsync().ContinueWith(task => Cities = task.Result);
            _disabilityRepository.GetAllAsync().ContinueWith(task =>
            {
                var disabilities = task.Result.ToList();
                disabilities.Add(new Disability()
                {
                    Id = -1
                });
                Disabilities = disabilities;
            });
            _genderRepository.GetAllAsync().ContinueWith(task => Genders = task.Result);
            _maritalStatusRepository.GetAllAsync().ContinueWith(task => MaritalStatuses = task.Result);
            _nationalityRepository.GetAllAsync().ContinueWith(task => Nationalities = task.Result);

            if (userId > 0)
            {
                //GetUser(userId).ContinueWith(task =>
                //{
                //    Application.Current.Dispatcher.Invoke(() => User = task.Result);
                //});
                GetUser(userId).ContinueWith(task => User = task.Result);
                SaveUserType = SaveUserType.Update;
            }
            else
            {
                User = new User();
                User.Passports.Add(new Passport());
                SaveUserType = SaveUserType.Create;
            }
        }
    }
}
