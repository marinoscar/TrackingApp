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
using System.Threading.Tasks;

namespace TrackingApp.Droid
{
    public class EventDataStore
    {
        public EventDataStore(ITableAdapter adapter)
        {
            Adapter = adapter;
        }

        public ITableAdapter Adapter { get; private set; }

        public bool Add(Event item)
        {
            var result = Adapter.Add(item);
            return !result.HasErrors;
        }
    }
}