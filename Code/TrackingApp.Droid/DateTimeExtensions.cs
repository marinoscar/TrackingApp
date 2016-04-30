using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Globalization;

namespace TrackingApp.Droid
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