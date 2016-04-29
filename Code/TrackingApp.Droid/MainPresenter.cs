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
    public class MainPresenter : PresenterBase
    {
        private int _count;

        public MainPresenter(IActivity activity) : base(activity)
        {

        }

        public override void BindView()
        {
            var button = Activity.FindViewById<Button>(Resource.Id.MyButton);
            button.Click += OnButtonClick;
        }

        private void OnButtonClick(object sender, EventArgs e)
        {
            var button = (Button)sender;
            button.Text = string.Format("{0} clicks!", _count++);
        }
    }
}