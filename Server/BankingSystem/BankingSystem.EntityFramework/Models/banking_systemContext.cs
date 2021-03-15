using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace BankingSystem.EntityFramework.Models
{
    public partial class banking_systemContext : DbContext
    {
        public banking_systemContext()
        {
        }

        public banking_systemContext(DbContextOptions<banking_systemContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<AccountType> AccountTypes { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Contract> Contracts { get; set; }
        public virtual DbSet<Currency> Currencies { get; set; }
        public virtual DbSet<Disability> Disabilities { get; set; }
        public virtual DbSet<Gender> Genders { get; set; }
        public virtual DbSet<MaritalStatus> MaritalStatuses { get; set; }
        public virtual DbSet<Nationality> Nationalities { get; set; }
        public virtual DbSet<Passport> Passports { get; set; }
        public virtual DbSet<Program> Programs { get; set; }
        public virtual DbSet<ProgramType> ProgramTypes { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=banking_system;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("account");

                entity.HasIndex(e => e.Number, "UX_account_number")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AccountTypeId).HasColumnName("account_type_id");

                entity.Property(e => e.Credit)
                    .HasColumnType("money")
                    .HasColumnName("credit");

                entity.Property(e => e.Debit)
                    .HasColumnType("money")
                    .HasColumnName("debit");

                entity.Property(e => e.Number)
                    .IsRequired()
                    .HasMaxLength(13)
                    .HasColumnName("number");

                entity.HasOne(d => d.AccountType)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.AccountTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_account_account_type");
            });

            modelBuilder.Entity<AccountType>(entity =>
            {
                entity.ToTable("account_type");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<City>(entity =>
            {
                entity.ToTable("city");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Contract>(entity =>
            {
                entity.ToTable("contract");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Amount)
                    .HasColumnType("money")
                    .HasColumnName("amount");

                entity.Property(e => e.CurrentAccountId).HasColumnName("current_account_id");

                entity.Property(e => e.Number).HasColumnName("number");

                entity.Property(e => e.PercentAccountId).HasColumnName("percent_account_id");

                entity.Property(e => e.ProgramId).HasColumnName("program_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.CurrentAccount)
                    .WithMany(p => p.ContractCurrentAccounts)
                    .HasForeignKey(d => d.CurrentAccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_contract_current_account");

                entity.HasOne(d => d.PercentAccount)
                    .WithMany(p => p.ContractPercentAccounts)
                    .HasForeignKey(d => d.PercentAccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_contract_percent_account");

                entity.HasOne(d => d.Program)
                    .WithMany(p => p.Contracts)
                    .HasForeignKey(d => d.ProgramId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_contract_program");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Contracts)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_contract_user");
            });

            modelBuilder.Entity<Currency>(entity =>
            {
                entity.ToTable("currency");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("code");
            });

            modelBuilder.Entity<Disability>(entity =>
            {
                entity.ToTable("disability");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .HasColumnName("description");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Gender>(entity =>
            {
                entity.ToTable("gender");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(2)
                    .HasColumnName("code");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("description");
            });

            modelBuilder.Entity<MaritalStatus>(entity =>
            {
                entity.ToTable("marital_status");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("description");
            });

            modelBuilder.Entity<Nationality>(entity =>
            {
                entity.ToTable("nationality");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Passport>(entity =>
            {
                entity.ToTable("passport");

                entity.HasIndex(e => e.IdentificationNumber, "ux_passport_identification_number")
                    .IsUnique();

                entity.HasIndex(e => new { e.Series, e.Number }, "ux_passport_series_number")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Authority)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("authority");

                entity.Property(e => e.DateOfIssue).HasColumnName("date_of_issue");

                entity.Property(e => e.IdentificationNumber)
                    .IsRequired()
                    .HasMaxLength(14)
                    .IsUnicode(false)
                    .HasColumnName("identification_number");

                entity.Property(e => e.Number)
                    .IsRequired()
                    .HasMaxLength(7)
                    .HasColumnName("number");

                entity.Property(e => e.Series)
                    .IsRequired()
                    .HasMaxLength(2)
                    .HasColumnName("series");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Passports)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_passport_user");
            });

            modelBuilder.Entity<Program>(entity =>
            {
                entity.ToTable("program");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CurrencyId).HasColumnName("currency_id");

                entity.Property(e => e.DateEnd).HasColumnName("date_end");

                entity.Property(e => e.DateStart).HasColumnName("date_start");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("name");

                entity.Property(e => e.Percent)
                    .HasColumnType("decimal(5, 2)")
                    .HasColumnName("percent");

                entity.Property(e => e.ProgramTypeId).HasColumnName("program_type_id");

                entity.Property(e => e.TermMonthCount).HasColumnName("term_month_count");

                entity.HasOne(d => d.Currency)
                    .WithMany(p => p.Programs)
                    .HasForeignKey(d => d.CurrencyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_program_currency");

                entity.HasOne(d => d.ProgramType)
                    .WithMany(p => p.Programs)
                    .HasForeignKey(d => d.ProgramTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_program_program_type");
            });

            modelBuilder.Entity<ProgramType>(entity =>
            {
                entity.ToTable("program_type");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DateOfBirth).HasColumnName("date_of_birth");

                entity.Property(e => e.DisabilityId).HasColumnName("disability_id");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("email");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("first_name");

                entity.Property(e => e.GenderId).HasColumnName("gender_id");

                entity.Property(e => e.HomePhoneNumber)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("home_phone_number");

                entity.Property(e => e.IsRetiree).HasColumnName("is_retiree");

                entity.Property(e => e.MaritalStatusId).HasColumnName("marital_status_id");

                entity.Property(e => e.MobilePhoneNumber)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("mobile_phone_number");

                entity.Property(e => e.MonthlyIncome)
                    .HasColumnType("money")
                    .HasColumnName("monthly_income");

                entity.Property(e => e.NationalityId).HasColumnName("nationality_id");

                entity.Property(e => e.RegistrationAddress)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("registration_address");

                entity.Property(e => e.ResidenceAddress)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("residence_address");

                entity.Property(e => e.ResidenceCityId).HasColumnName("residence_city_id");

                entity.Property(e => e.SecondName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("second_name");

                entity.Property(e => e.ThirdName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("third_name");

                entity.HasOne(d => d.Disability)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.DisabilityId)
                    .HasConstraintName("FK_user_disability");

                entity.HasOne(d => d.Gender)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.GenderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_user_gender");

                entity.HasOne(d => d.MaritalStatus)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.MaritalStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_user_marital_status");

                entity.HasOne(d => d.Nationality)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.NationalityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_user_nationality");

                entity.HasOne(d => d.ResidenceCity)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.ResidenceCityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_user_residence_city");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
