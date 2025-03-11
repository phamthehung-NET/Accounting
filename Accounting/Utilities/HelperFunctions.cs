using Accounting.Pages;
using Microsoft.Extensions.Localization;
using Microsoft.JSInterop;
using System.Drawing.Imaging;
using System.Drawing;
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
            
            if(isLeapYear)
            {
                month--;
            }
            return string.Join("/", new int[] { day, month, year });
        }

        public static string ConvertSolarToLunar(DateTime solarDate, int timeZone = 7)
        {
            int dayNumber = JulianDayNumber(solarDate.Year, solarDate.Month, solarDate.Day);
            int k = (int)((dayNumber - 2415021.076998695) / 29.530588853);
            DateTime newMoonDate = GetNewMoonDate(k + 1, timeZone);
            DateTime prevNewMoonDate = GetNewMoonDate(k, timeZone);

            if (dayNumber < JulianDayNumber(prevNewMoonDate.Year, prevNewMoonDate.Month, prevNewMoonDate.Day))
            {
                newMoonDate = prevNewMoonDate;
                prevNewMoonDate = GetNewMoonDate(k - 1, timeZone);
            }

            int lunarDay = dayNumber - JulianDayNumber(newMoonDate.Year, newMoonDate.Month, newMoonDate.Day) + 1;
            int lunarMonth = (newMoonDate.Month > prevNewMoonDate.Month) ? newMoonDate.Month : newMoonDate.Month + 12;
            int lunarYear = newMoonDate.Year;

            return string.Join("/", new int[] { lunarDay, lunarMonth, lunarYear });
        }

        // Hàm tính số Julian Day Number (JDN)
        private static int JulianDayNumber(int year, int month, int day)
        {
            if (month < 3)
            {
                year--;
                month += 12;
            }
            int A = year / 100;
            int B = 2 - A + A / 4;
            return (int)(365.25 * (year + 4716)) + (int)(30.6001 * (month + 1)) + day + B - 1524;
        }

        // Hàm tìm ngày Sóc (New Moon)
        private static DateTime GetNewMoonDate(int k, int timeZone)
        {
            double T = k / 1236.85;
            double Jd = 2415020.75933 + 29.53058868 * k + 0.0001178 * T * T;
            Jd -= 0.000000155 * T * T * T;
            Jd += 0.00033 * Math.Sin(166.56 + 132.87 * T - 0.009173 * T * T);
            return JulianToDate(Jd + timeZone / 24.0);
        }

        // Chuyển đổi Julian Date về Dương lịch
        private static DateTime JulianToDate(double jd)
        {
            int Z = (int)(jd + 0.5);
            double F = jd + 0.5 - Z;
            int A = Z < 2299161 ? Z : (int)((Z - 1867216.25) / 36524.25) + 1 + Z;
            int B = A + 1524;
            int C = (int)((B - 122.1) / 365.25);
            int D = (int)(365.25 * C);
            int E = (int)((B - D) / 30.6001);
            int day = B - D - (int)(30.6001 * E) + (int)F;
            int month = (E < 14) ? E - 1 : E - 13;
            int year = (month > 2) ? C - 4716 : C - 4715;
            return new DateTime(year, month, day);
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
