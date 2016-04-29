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
    public class PresenterBase
    {
        public PresenterBase(IActivity activity)
        {
            Activity = activity;
        }

        public virtual IActivity Activity { get; private set; }
    }
}