using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Globalization;

namespace Accounting.Common
{
    public class HelperFunctions
    {
        public static Pagination<T> GetPaging<T>(int? pageIndex, int? itemPerPage, List<T> items)
        {
            return new Pagination<T>(items.Count(), pageIndex, itemPerPage, items);
        }

        public static string RenderMeatType(int type)
        {
            if (type == Constants.BUFFALO_TYPE)
            {
                return "Trâu";
            }
            return "Bò";
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
            return string.Join("/", new int[] {day, month} );
        }
    }
}
