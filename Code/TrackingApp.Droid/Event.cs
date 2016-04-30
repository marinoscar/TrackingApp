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
    public class Event : EntityBase
    {
        public Event(string action)
        {
            PartitionKey = action;
        }

        public override string ToOData()
        {
            var template = @"
    ""PartitionKey"":""{0}"",
    ""RowKey"":""{1}"",
    ""Type"":""{2}"",
    ""Currency"":""{3}"",
    ""Category"":""{4}"",
    ""LocalTimestamp@odata.type"":""Edm.DateTime"",
    ""LocalTimestamp"":""{5}"",
    ""Value"":{6}  
";
            return "{" + string.Format(template,
                PartitionKey, RowKey, Type, Currency, Category, LocalTimestamp.ToJson(), Value) + "}";
        }

        public string Type { get; set; }
        public string Currency { get; set; }
        public string Category { get; set; }
        public DateTime LocalTimestamp { get; set; }
        public double Value { get; set; }

        public static Event FromTextResult(TextParseResult result)
        {
            if (result == null) return null;
            var item = new Event(result.Action)
            {
                Category = result.Category,
                Currency = result.Curency,
                LocalTimestamp = DateTime.Now,
                Type = result.Type,
                Value = result.Value
            };
            return item;
        }
    }
}