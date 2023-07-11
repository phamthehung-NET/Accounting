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
