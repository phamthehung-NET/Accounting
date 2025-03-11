using Accounting.Pages;
using Lunar;
using Microsoft.Extensions.Localization;
using Microsoft.JSInterop;
using System.Drawing;
using System.Drawing.Imaging;
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

        public static string GetLunarDate(bool isLeapYear, DateTime dateInput)
        {
            ChineseLunisolarCalendar lunarCalendar = new();
            var day = lunarCalendar.GetDayOfMonth(dateInput);
            var month = lunarCalendar.GetMonth(dateInput);
            var year = lunarCalendar.GetYear(dateInput);

            if (isLeapYear)
            {
                month--;
            }
            return string.Join("/", new int[] { day, month, year });
        }

        public static string ConvertSolarToLunar(DateTime solarDate, int timeZone = 7)
        {
            Solar solar = new(solarDate.Year, solarDate.Month, solarDate.Day);
            Lunar.Lunar lunar = solar.Lunar;

            return string.Join("/", new string[] { lunar.Day.ToString(), lunar.Month <= 0 ? $"{-lunar.Month}NH" : lunar.Month.ToString(), lunar.Year.ToString() });
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

        public static Image SetImageOpacityAndRotate(string imagePath, float opacity, float angle)
        {
            Image img = Image.FromFile(imagePath);
            int width = img.Width;
            int height = img.Height;

            Bitmap bmp = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.Transparent);
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                g.TranslateTransform(width / 2, height / 2);
                g.RotateTransform(angle);
                g.TranslateTransform(-width / 2, -height / 2);

                ColorMatrix matrix = new ColorMatrix();
                matrix.Matrix33 = opacity;
                ImageAttributes attributes = new ImageAttributes();
                attributes.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

                g.DrawImage(img, new Rectangle(0, 0, width, height), 0, 0, width, height, GraphicsUnit.Pixel, attributes);
            }
            return bmp;
        }
    }
}
