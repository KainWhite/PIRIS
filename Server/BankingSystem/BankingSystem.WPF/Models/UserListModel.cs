using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Windows;
using BankingSystem.Domain.Entities;
using BankingSystem.EntityFramework.Repositories;
using BankingSystem.WPF.Commands;
using Prism.Mvvm;

namespace BankingSystem.WPF.Models
{
    public class UserListModel : BindableBase
    {
        public BindingList<User> Users { get; set; } = new BindingList<User>();

        private readonly UserRepository _userRepository;

        public UserListModel(UserRepository userRepository)
        {
            _userRepository = userRepository;

            GetUsers().ContinueWith(task =>
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    Users.AddRange(task.Result.OrderBy(x => x.SecondName));
                });
            });
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            var users = await _userRepository
                .GetAllAsync(new List<Expression<Func<User, object>>>()
                {
                    x => x.Passports,
                })
                .ConfigureAwait(false);
            return users;
        }
    }
}
