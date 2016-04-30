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
    public interface IEntity
    {
        string PartitionKey { get; set; }
        string RowKey { get; set; }
        DateTimeOffset Timestamp { get; set; }
        string ToOData();
    }
}