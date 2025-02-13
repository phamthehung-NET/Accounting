using Accounting.Data;
using Accounting.Model;
using Accounting.Repositories.Interfaces;
using Accounting.Utilities;

namespace Accounting.Repositories.Implementations
{
    public class SettingRepository : ISettingRepository
    {
        private readonly AccountingDbContext context;

        public SettingRepository(AccountingDbContext _context)
        {
            context = _context;
        }

        public Pagination<Setting> GetAllSettings(int pageIndex, int pageSize)
        {
            return context.Settings.Paginate(pageIndex, pageSize);
        }

        public string GetSettingValue(string settingName)
        {
            var setting = context.Settings.FirstOrDefault(x => x.Name.Equals(settingName));
            return setting?.Value;
        }

        public bool ChangeSetting(string settingName, string value)
        {
            var setting = context.Settings.FirstOrDefault(x => x.Name.Equals(settingName));
            if (setting != null)
            {
                setting.Value = value;
                context.SaveChanges();
                return true;
            }
            return false;
        }

        public IEnumerable<Year> GetListYear()
        {
            return context.YearSettings.OrderByDescending(x => x.Name).ToList();
        }

        public bool UpdateLeapYear(int year, bool isLeapYear)
        {
            var record = context.YearSettings.FirstOrDefault(x => x.Name == year);
            if (record != null)
            {
                record.IsLeapYear = isLeapYear;
                context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool IsLeapYear(int year)
        {
            var record = context.YearSettings.FirstOrDefault(x => x.Name == year);
            if (record != null)
            {
                return record.IsLeapYear;
            }
            return false;
        }

        public List<Year> GetLeapYear(IEnumerable<int> years)
        {
            return context.YearSettings.Where(x => years.Contains(x.Name)).OrderBy(x => x.Name).ToList();
        }
    }
}
