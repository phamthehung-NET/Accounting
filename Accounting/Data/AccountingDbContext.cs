using Accounting.Common;
using Accounting.Model;
using Microsoft.EntityFrameworkCore;

namespace Accounting.Data
{
    public class AccountingDbContext : DbContext
    {
        public AccountingDbContext(DbContextOptions<AccountingDbContext> options) : base(options)
        {
        }

        public DbSet<Meat> Meats { get; set; }

        public DbSet<Person> People { get; set; }

        public DbSet<MeatPrice> MeatPrices { get; set; }

        public DbSet<MeatBillPrice> MeatBillPrices { get; set; }

        public DbSet<Bill> Bills { get; set; }
        
        public DbSet<RecycleBin> RecycleBins { get; set; }
    }
}
