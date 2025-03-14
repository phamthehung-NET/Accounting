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

        public const string Dashboad = $"/dashboard";
        public const string UpdatePrice = $"/updatePrice";
        public const string UnAuthorized = $"/unauthorized";
        public const string MeatList = $"/manage-meat";
        public const string PeopleMng = $"/people-management";
        public const string EntryMng = $"/manage-entry";
        public const string SaleMng = $"/manage-sale";
        public const string RecycleBin = $"/recycle-bin";
        public const string SysSettings = $"/system-setting";
        public const string SysInfo = $"/system-info";
        public const string SysLog = $"/log";
        public const string SeedData = $"/seed-data";
        public const string Login = $"/login";
        public const string Logout = $"/logout";

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

    public enum DasboardDebtFilterType
    {
        MostDebt,
        LongestDebt,
    }
}
