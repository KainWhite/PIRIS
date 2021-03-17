using BankingSystem.Core;
using BankingSystem.Core.Enums;
using BankingSystem.Core.Extensions;
using BankingSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BankingSystem.EntityFramework
{
    public class BankingSystemContext : DbContext
    {
        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<AccountType> AccountTypes { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Contract> Contracts { get; set; }
        public virtual DbSet<Currency> Currencies { get; set; }
        public virtual DbSet<DateChange> DateChanges { get; set; }
        public virtual DbSet<Disability> Disabilities { get; set; }
        public virtual DbSet<Gender> Genders { get; set; }
        public virtual DbSet<MaritalStatus> MaritalStatuses { get; set; }
        public virtual DbSet<Nationality> Nationalities { get; set; }
        public virtual DbSet<Passport> Passports { get; set; }
        public virtual DbSet<Program> Programs { get; set; }
        public virtual DbSet<ProgramType> ProgramTypes { get; set; }
        public virtual DbSet<User> Users { get; set; }

        public BankingSystemContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var entities = builder.Model.GetEntityTypes();

            foreach (var entity in entities)
            {
                // Replace table names
                entity.SetTableName(entity.GetTableName().ToSnakeCase());

                // Replace column names            
                foreach (var property in entity.GetProperties())
                {
                    property.SetColumnName(property.GetColumnName().ToSnakeCase());
                }
            }

            builder.Entity<AccountType>().Property(x => x.Name).HasConversion(new ValueConverter<AccountTypeEnum,string>(
                v => v.GetDescription(),
                v => v.GetEnumValue<AccountTypeEnum>()));
            builder.Entity<ProgramType>().Property(x => x.Name).HasConversion<string>();
        }
    }
}
