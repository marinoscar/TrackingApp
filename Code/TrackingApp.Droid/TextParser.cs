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
            return GetFromApiResult(Service.Parse(text));
        }

        private TextParseResult GetFromApiResult(ApiResult<TextParserServiceResult> result)
        {
            var value = new TextParseResult();
            var itm = result.Result;
            if (result.HasErrors) return null;
            var intent = itm.IntentsResults.OrderByDescending(i => i.Score).FirstOrDefault();
            if (!(intent != null && intent.Name == "RegisterEvent")) return null;
            if (itm.EntitiesResults.Any(i => i.Name == "Type"))
                value.Type = itm.EntitiesResults.FirstOrDefault(i => i.Name == "Type").Word;
            if (itm.EntitiesResults.Any(i => i.Name == "Category"))
                value.Category = itm.EntitiesResults.FirstOrDefault(i => i.Name == "Category").Word;
            if (itm.EntitiesResults.Any(i => i.Name == "Action"))
                value.Action = itm.EntitiesResults.FirstOrDefault(i => i.Name == "Action").Word;
            if (itm.EntitiesResults.Any(i => i.Name == "number"))
                value.Value = Convert.ToDouble(itm.EntitiesResults.FirstOrDefault(i => i.Name == "number").Word);
            if (itm.EntitiesResults.Any(i => i.Name == "Curency"))
                value.Curency = itm.EntitiesResults.FirstOrDefault(i => i.Name == "Curency").Word;
            return value;
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