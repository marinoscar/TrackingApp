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
        public event EventHandler<ActivityResultArgs> ActivityResult;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            var mainPresenter = new MainPresenter(this);
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if(ActivityResult != null)
            {
                ActivityResult.Invoke(this, new ActivityResultArgs() { RequestCode = requestCode, ResultCode = resultCode, Data = data });
            }
        }
    }
}

