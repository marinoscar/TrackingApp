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

namespace TrackingApp.Droid
{
    public static class DateTimeExtensions
    {
        public static string ToJson(this DateTime d)
        {
            return d.ToString("yyyy-MM-ddThh:mm:ssZ");
        }
    }
}