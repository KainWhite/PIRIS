using System;
using System.Collections.Generic;
using System.Linq.Expressions;
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
            var accountTypeService = new AccountTypeRepository(factory);
            var programService = new ProgramRepository(factory);
            var programTypeService = new ProgramTypeRepository(factory);
            var currencyService = new CurrencyRepository(factory);
            //var x = accountService.GetAllAsync().Result;
            var currency = currencyService.GetAsync(1).Result;
            var program = new Domain.Entities.Program()
            {
                Currency = currency,
                DateStart = DateTime.Now,
                DateEnd = DateTime.Now,
                Name = "lol",
                Percent = 13,
                ProgramTypeId = 1,
                TermMonthCount = 12
            };
            var x = programService.CreateAsync(program).Result;
            Console.WriteLine(x.Id);
            //foreach (var entity in x)
            //{
            //    Console.WriteLine(entity.Name);
            //    //foreach (var entityContract in entity.Programs)
            //    //{
            //    //    Console.WriteLine(entityContract.Name);
            //    //}
            //}
        }
    }
}
