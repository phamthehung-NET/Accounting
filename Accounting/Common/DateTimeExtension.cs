using System;

namespace Accounting.Common
{
    public static class DateTimeExtension
    {
        public static bool LaterThan(this DateTime dateTime, DateTime comparedDateTime)
        {
            return dateTime.CompareTo(comparedDateTime) > 0;
        }

        public static bool EqualTo(this DateTime dateTime, DateTime comparedDateTime)
        {
            return dateTime.CompareTo(comparedDateTime) == 0;
        }

        public static bool EarlierThan(this DateTime dateTime, DateTime comparedDateTime)
        {
            return dateTime.CompareTo(comparedDateTime) < 0;
        }

        public static bool LaterThanOrEqualTo(this DateTime dateTime, DateTime comparedDateTime)
        {
            return dateTime.CompareTo(comparedDateTime) >= 0;
        }

        public static bool EarlierThanOrEqualTo(this DateTime dateTime, DateTime comparedDateTime)
        {
            return dateTime.CompareTo(comparedDateTime) <= 0;
        }
    }
}
