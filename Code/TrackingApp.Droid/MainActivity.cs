using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace TrackingApp.Droid
{
    [Activity(Label = "Activity Tracker", MainLauncher = true, Icon = "@drawable/radar")]
    public class MainActivity : Activity, IActivity
    {
        int count = 1;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            var mainPresenter = new MainPresenter(this);
        }
    }
}

