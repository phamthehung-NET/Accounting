using Accounting.Model;
using Accounting.Utilities;

namespace Accounting.Repositories.Interfaces
{
    public interface ISettingRepository
    {
        Pagination<Setting> GetAllSettings(int pageIndex, int pageSize);

        string GetSettingValue(string settingName);

        bool ChangeSetting(string settingName, string value);

        IEnumerable<Year> GetListYear();

        bool UpdateLeapYear(int year, bool isLeapYear);

        bool IsLeapYear(int year);

        List<Year> GetLeapYear(IEnumerable<int> years);
    }
}
