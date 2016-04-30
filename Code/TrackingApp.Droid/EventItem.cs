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
using Microsoft.WindowsAzure.Storage.Table;

namespace TrackingApp.Droid
{
    public class EventItem : TableEntity
    {
        public EventItem(string action)
        {
            PartitionKey = action;
            RowKey = Guid.NewGuid().ToString();
        }
        public string Type { get; set; }
        public string Currency { get; set; }
        public string Category { get; set; }
        public string DeviceTimeZone { get; set; }
        public DateTime LocalTmestamp { get; set; }
        public double Value { get; set; }

        public static EventItem FromTextResult(TextParseResult result)
        {
            var item = new EventItem(result.Action)
            {
                Category = result.Category,
                Currency = result.Curency,
                LocalTmestamp = DateTime.Now,
                Type = result.Type,
                Value = result.Value
            };
            return item;
        }

    }
}