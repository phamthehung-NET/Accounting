using ChartJs.Blazor.Util;
using System.Drawing;

namespace Accounting.Utilities
{
    public class Constants
    {
        public const string ErrorColor = "danger";
        public const string SuccessColor = "success";
        public const string InfoColor = "info";
        public const string WarningColor = "warning";
        public const string PrimaryColor = "primary";

        public const string GoodChartColor = "rgba(13, 157, 255, 0.8)";
        public const string WarningChartColor = "rgba(255, 237, 13, 0.8)";
        public const string NotGoodChartColor = "rgba(255, 12, 12, 0.8)";

        public const string IsLeapYearSetting = "IsLeapYear";

        #region Routes

        public const string SystemPrefix = "/accounting";

        public const string Dashboad = $"{SystemPrefix}/dashboard";
        public const string UpdatePrice = $"{SystemPrefix}/updatePrice";
        public const string UnAuthorized = $"{SystemPrefix}/unauthorized";
        public const string MeatList = $"{SystemPrefix}/manage-meat";
        public const string PeopleMng = $"{SystemPrefix}/people-management";
        public const string EntryMng = $"{SystemPrefix}/manage-entry";
        public const string SaleMng = $"{SystemPrefix}/manage-sale";
        public const string RecycleBin = $"{SystemPrefix}/recycle-bin";
        public const string SysSettings = $"{SystemPrefix}/system-setting";
        public const string SysInfo = $"{SystemPrefix}/system-info";
        public const string SysLog = $"{SystemPrefix}/log";
        public const string SeedData = $"{SystemPrefix}/seed-data";
        public const string Login = $"{SystemPrefix}/login";
        public const string Logout = $"{SystemPrefix}/logout";

        #endregion
    }

    public enum MeatType
    {
        Beef,
        Buffalo,
        Calves,
    }

    public enum ToastType
    {
        Success,
        Notification,
        Error,
        Warning,
    }

    public enum RecycleBinObjectType
    {
        Meat,
        Person,
        Bill,
    }

    public enum PriceType
    {
        Entry,
        Sale
    }

    public enum HistoryType
    {
        Bill,
    }

    public enum HistoryAction
    {
        Create,
        Remove,
        Pay,
        RemoveItem,
        AddItem,
        EditItemPrice,
        Restore,
    }
}
