using Accounting.Utilities;

namespace Accounting.Shared
{
    public static class Routes
    {
        public static readonly Dictionary<string, (Type Page, bool RequiresAuth)> Pages = new()
        {
            {"/", (typeof(Pages.Index), false) },
            {Constants.UpdatePrice, (typeof(Pages.Index), true) },
            {Constants.UnAuthorized, (typeof(Pages.Index), true) },
            {Constants.Dashboad, (typeof(Pages.Dashboard.Dashboard), true) },
            {Constants.MeatList, (typeof(Pages.ManageMeat.MeatList), true) },
            {Constants.PeopleMng, (typeof(Pages.PeopleManagement.PeopleList), true) },
            {Constants.EntryMng, (typeof(Pages.EntryManagement.EntryList), true) },
            {Constants.SaleMng, (typeof(Pages.SaleManagement.SaleList), true) },
            {Constants.RecycleBin, (typeof(Pages.RecycleBin.RecycleBinList), true) },
            {Constants.SysSettings, (typeof(Pages.SystemSettings.SystemSetting), true) },
            {Constants.SysInfo, (typeof(Pages.Shared.SystemInfo), false) },
            {Constants.SysLog, (typeof(Pages.SystemLog.LogList), true) },
            //{Constants.SeedData, (typeof(Pages.Shared.SeedData), true) },
        };

        public static (Type Page, bool RequiresAuth) GetPage(string url)
        {
            return Pages.GetValueOrDefault(url, (typeof(Pages.NotFound), false));
        }
    }
}
