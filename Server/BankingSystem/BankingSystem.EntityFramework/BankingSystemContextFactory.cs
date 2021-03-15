using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace BankingSystem.EntityFramework
{
    public class BankingSystemContextFactory : IDesignTimeDbContextFactory<BankingSystemContext>
    {
        public BankingSystemContext CreateDbContext(string[] args = null)
        {
            var options = new DbContextOptionsBuilder<BankingSystemContext>();
            options.UseSqlServer(
                @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=banking_system;Trusted_Connection=True;");
            return new BankingSystemContext(options.Options);
        }
    }
}
