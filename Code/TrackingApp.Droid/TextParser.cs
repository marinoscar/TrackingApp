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
    public class TextParser
    {
        public TextParser(ITextParserService service)
        {
            Service = service;
        }

        public ITextParserService Service { get;  private set; }

        public TextParseResult Parse(string text)
        {
            var result = new TextParseResult();
            var apiResult = Service.Parse(text);
            if (apiResult.HasErrors) return result;
            result.Action = "Great";
            return result;
        }

    }

    public class TextParseResult
    {
        public string Type { get; set; }
        public string Category { get; set; }
        public string Action { get; set; }
        public string Curency { get; set; }
        public double Value { get; set; }

    }
}