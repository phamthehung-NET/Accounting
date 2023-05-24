namespace Accounting.Common
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
        Person
    }

    public enum PriceType
    {
        Entry,
        Sale
    }
}
