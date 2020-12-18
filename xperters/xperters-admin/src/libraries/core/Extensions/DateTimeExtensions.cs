using System;
using System.Globalization;

// ReSharper disable once CheckNamespace
namespace Xperters
{
    public static class DateTimeExtensions
    {
        private const string Iso8601UtcDateTimeFormat = "yyyy-MM-dd HH:mm:ss.FFFFFFFK";
        private const string Iso8601LocalDateTimeFormat = "yyyy-MM-dd HH:mm:ss.FFFFFFF";

        // ReSharper disable once InconsistentNaming
        public static string ToISO8601UtcString(this DateTime dateTime)
        {
            return dateTime.ToString(Iso8601UtcDateTimeFormat, CultureInfo.InvariantCulture);
        }

        // ReSharper disable once InconsistentNaming
        public static string ToISO8601LocalString(this DateTime dateTime)
        {
            return dateTime.ToString(Iso8601LocalDateTimeFormat, CultureInfo.InvariantCulture);
        }
    }
}
