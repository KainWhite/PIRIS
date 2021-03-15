using System;
using System.Windows;
using BankingSystem.EntityFramework;
using BankingSystem.EntityFramework.Repositories;
using BankingSystem.WPF.Models;
using BankingSystem.WPF.State.Navigators;
using BankingSystem.WPF.ViewModels;
using BankingSystem.WPF.ViewModels.Factories;
using BankingSystem.WPF.Views;
using Microsoft.Extensions.DependencyInjection;

namespace BankingSystem.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var serviceProvider = CreateServiceProvider();

            Window window = serviceProvider.GetRequiredService<MainView>();
            window.DataContext = serviceProvider.GetRequiredService<MainViewModel>();
            window.Show();

            base.OnStartup(e);
        }

        private IServiceProvider CreateServiceProvider()
        {
            IServiceCollection services = new ServiceCollection();

            services.AddSingleton<BankingSystemContextFactory>();
            services.AddSingleton<CityRepository>();
            services.AddSingleton<DisabilityRepository>();
            services.AddSingleton<GenderRepository>();
            services.AddSingleton<MaritalStatusRepository>();
            services.AddSingleton<NationalityRepository>();
            services.AddSingleton<PassportRepository>();
            services.AddSingleton<UserRepository>();

            services.AddSingleton<IViewModelAbstractFactory, ViewModelAbstractFactory>();

            services.AddScoped<INavigator, Navigator>();

            services.AddScoped<UserListModel>();
            services.AddScoped<MainViewModel>();
            services.AddScoped<MainView>();

            return services.BuildServiceProvider();
        }
    }
}
