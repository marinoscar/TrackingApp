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
    public static class Validator
    {
        public static bool MicCheck(Context context)
        {
            var rec = Android.Content.PM.PackageManager.FeatureMicrophone;
            if (rec != "android.hardware.microphone")
            {
                var alert = new AlertDialog.Builder(context);
                alert.SetTitle("No cuenta con el microfono instalado");
                alert.SetPositiveButton("OK", (sender, e) =>
                {
                    return;
                });
                alert.Show();
                return false;
            }
            return true;
        }
    }
}