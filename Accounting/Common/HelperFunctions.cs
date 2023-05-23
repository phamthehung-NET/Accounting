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

        public static string RenderMeatType(IStringLocalizer<Resource> LRes, int type)
        {
            if (type == (int)Constants.MeatType.Buffalo)
            {
                return LRes["Buffalo"];
            }
            return LRes["Beef"];
        }

        public static void NavigateUrl(NavigationManager navigation, string url)
        {
            navigation.NavigateTo(url);
        }

        public async static Task ShowNotification(IJSRuntime jsRuntime, string bootstrapColor, string content)
        {
            await jsRuntime.InvokeVoidAsync("showToast", bootstrapColor, content);
        }

        public static string GetLunarDate(DateTime dateInput)
        {
            ChineseLunisolarCalendar lunarCalendar = new();
            var day = lunarCalendar.GetDayOfMonth(dateInput);
            var month = lunarCalendar.GetMonth(dateInput);
            return string.Join("/", new int[] { day, month });
        }

        public static async Task TriggerBtn(IJSRuntime jSRuntime, string btnId)
        {
            await jSRuntime.InvokeVoidAsync("triggerBtn", btnId);
        }
    }
}
