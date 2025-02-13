using Accounting.Model;
using Microsoft.EntityFrameworkCore;

namespace Accounting.Data
{
    public class AccountingDbContext : DbContext
    {
        public AccountingDbContext(DbContextOptions<AccountingDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Year year = new()
            {
                Id = 1,
                IsLeapYear = false,
                Name = DateTime.Now.Year,
            };
            modelBuilder.Entity<Year>().HasData(year);
        }

        public DbSet<Meat> Meats { get; set; }

        public DbSet<Person> People { get; set; }

        public DbSet<MeatPrice> MeatPrices { get; set; }

        public DbSet<MeatBillPrice> MeatBillPrices { get; set; }

        public DbSet<Bill> Bills { get; set; }

        public DbSet<RecycleBin> RecycleBins { get; set; }

        public DbSet<History> Histories { get; set; }

        public DbSet<Log> Logs { get; set; }

        public DbSet<Setting> Settings { get; set; }

        public DbSet<Year> YearSettings { get; set; }
    }
}
