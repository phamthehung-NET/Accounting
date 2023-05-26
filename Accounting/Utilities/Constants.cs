namespace Accounting.Utilities
{
    public class Constants
    {
        public const string ToastError = "danger";
        public const string ToastSuccess = "success";
        public const string ToastInfo = "info";
        public const string ToastWarning = "warning";
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
