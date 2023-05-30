using Accounting.Pages;
using Microsoft.Extensions.Localization;
using Microsoft.JSInterop;
using System.Globalization;

namespace Accounting.Utilities
{
    public class HelperFunctions
    {
        //public static Pagination<T> GetPaging<T>(int? pageIndex, int? itemPerPage, IQueryable<T> items)
        //{
        //    return new Pagination<T>(items.Count(), pageIndex, itemPerPage, items);
        //}

        public static string RenderMeatType(IStringLocalizer<Resource> LRes, int? type)
        {
            return type switch
            {
                (int)MeatType.Buffalo => LRes["Buffalo"],
                (int)MeatType.Beef => LRes["Beef"],
                (int)MeatType.Calves => LRes["Calves"],
                _ => string.Empty,
            };
        }

        public async static Task ShowNotification(IJSRuntime jsRuntime, ToastType toastType, string content)
        {
            var bootstrapColor = Constants.InfoColor;
            switch (toastType)
            {
                case ToastType.Error:
                    bootstrapColor = Constants.ErrorColor;
                    break;
                case ToastType.Notification:
                    bootstrapColor = Constants.InfoColor;
                    break;
                case ToastType.Success:
                    bootstrapColor = Constants.SuccessColor;
                    break;
                case ToastType.Warning:
                    bootstrapColor = Constants.WarningColor;
                    break;
                default:
                    break;
            }
            await jsRuntime.InvokeVoidAsync("showToast", bootstrapColor, content);
        }

        public static string GetLunarDate(DateTime dateInput)
        {
            ChineseLunisolarCalendar lunarCalendar = new();
            var day = lunarCalendar.GetDayOfMonth(dateInput);
            var month = lunarCalendar.GetMonth(dateInput) - 1;
            return string.Join("/", new int[] { day, month });
        }

        public static async Task TriggerBtn(IJSRuntime jSRuntime, string btnId)
        {
            await jSRuntime.InvokeVoidAsync("triggerBtn", btnId);
        }

        public static async Task HideModal(IJSRuntime jSRuntime, string modalId)
        {
            await jSRuntime.InvokeVoidAsync("hideModal", modalId);
        }

        public static async Task AddIndicator(IJSRuntime jSRuntime)
        {
            await jSRuntime.InvokeVoidAsync("addIndicator");
        }

        public static async Task RemoveIndicator(IJSRuntime jSRuntime)
        {
            await jSRuntime.InvokeVoidAsync("removeIndicator");
        }

        public static string HandleDisplayPersonType(IStringLocalizer<Resource> LRes, bool? type)
        {
            if (type != null && (bool)type)
            {
                return LRes["Seller"];
            }
            return LRes["Buyer"];
        }

        public static async Task ToggleSideBar(IJSRuntime jSRuntime)
        {
            await jSRuntime.InvokeVoidAsync("toggleSideBar");
        }
    }
}
