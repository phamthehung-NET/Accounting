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
            if(setting != null)
            {
                setting.Value = value;
                context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
