namespace Accounting.Utilities
{
    public class Constants
    {
        public const string ErrorColor = "danger";
        public const string SuccessColor = "success";
        public const string InfoColor = "info";
        public const string WarningColor = "warning";
        public const string PrimaryColor = "primary";
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
