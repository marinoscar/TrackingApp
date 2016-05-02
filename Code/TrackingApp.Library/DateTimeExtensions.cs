using System;
using System.Globalization;

namespace TrackingApp.Droid.Library
{
    public static class DateTimeExtensions
    {
        public static string ToJson(this DateTime d)
        {
            return d.ToString("yyyy-MM-ddThh:mm:ssZ");
        }

        public static string ToHeader(this DateTime d)
        {
            return d.ToString("R", CultureInfo.InvariantCulture);
        }
    }
}