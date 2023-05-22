namespace Accounting.Common
{
    public class Constants
    {
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
        }

        public enum RecycleBinObjectType
        {
            Meat,
        }

        public enum PriceType
        {
            Entry,
            Sale
        }

        public const string ToastError = "danger";
        public const string ToastSuccess = "success";
        public const string ToastInfo = "info";
        public const string ToastWarning = "warning";
    }
}
