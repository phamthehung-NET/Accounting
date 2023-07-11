using Accounting.Model;
using Accounting.Utilities;

namespace Accounting.Repositories.Interfaces
{
    public interface ISettingRepository
    {
        Pagination<Setting> GetAllSettings(int pageIndex, int pageSize);

        string GetSettingValue(string settingName);

        bool ChangeSetting(string settingName, string value);
    }
}
