using System;
using BankingSystem.Domain.Entities;
using BankingSystem.EntityFramework;
using BankingSystem.EntityFramework.Repositories;

namespace BankingSystem.TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new BankingSystemContextFactory();
            var cityService = new Repository<City>(factory);
            var disabilityService = new Repository<Disability>(factory);
            var genderService = new Repository<Gender>(factory);
            var maritalStatusService = new Repository<MaritalStatus>(factory);
            var nationalityService = new Repository<Nationality>(factory);
            var passportService = new Repository<Passport>(factory);
            var userService = new Repository<User>(factory);
            var accountService = new AccountRepository(factory);
            var x = accountService.GetAllAsync().Result;
            foreach (var entity in x)
            {
                Console.WriteLine(entity.Number);
                foreach (var entityContract in entity.Contracts)
                {
                    Console.WriteLine(entityContract.Number);
                }
            }
        }
    }
}
