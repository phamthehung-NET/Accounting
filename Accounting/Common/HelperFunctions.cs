using Accounting.Pages;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Microsoft.JSInterop;
using System.Globalization;

namespace Accounting.Common
{
    public class HelperFunctions
    {
        //public static Pagination<T> GetPaging<T>(int? pageIndex, int? itemPerPage, IQueryable<T> items)
        //{
        //    return new Pagination<T>(items.Count(), pageIndex, itemPerPage, items);
        //}

        public static string RenderMeatType(IStringLocalizer<Resource> LRes, int? type)
        {
            if(type == null)
            {
                return string.Empty;
            }
            else
            {
                if (type == (int)MeatType.Buffalo)
                {
                    return LRes["Buffalo"];
                }
                return LRes["Beef"];
            }
        }

        public async static Task ShowNotification(IJSRuntime jsRuntime, ToastType toastType, string content)
        {
            var bootstrapColor = "info";
            switch (toastType)
            {
                case ToastType.Error:
                    bootstrapColor = "danger";
                    break;
                case ToastType.Notification:
                    bootstrapColor = "info";
                    break;
                case ToastType.Success:
                    bootstrapColor = "success";
                    break;
                case ToastType.Warning:
                    bootstrapColor = "warning";
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
    }
}
