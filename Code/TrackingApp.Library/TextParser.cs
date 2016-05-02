using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TrackingApp.Droid.Library.Models;

namespace TrackingApp.Droid.Library
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
}