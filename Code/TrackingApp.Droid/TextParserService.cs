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
    public class TextParserService
    {
        
    }

    public class TextParserServiceResult
    {
        public TextParserServiceResult()
        {
            IntentResults = new List<IntentResult>();
            EntitiesResults = new List<EntityResult>();
            TokenizedText = new List<string>();
        }

        public List<IntentResult> IntentResults { get; set; }
        public List<EntityResult> EntitiesResults { get; set; }
        public string UtteranceText { get; set; }

        public List<string> TokenizedText { get; set; }
    }

    public class IntentResult
    {
        public string Name { get; set; }
        public string Label { get; set; }
        public double Score { get; set; }
    }

    public class EntityResult
    {
        public string Name { get; set; }
        public string Word { get; set; }
        public string Color { get; set; }
        public bool IsBuiltInExtractor { get; set; }
    }

    public class Indices
    {
        public int StartToken { get; set; }
        public int EndToken { get; set; }
    }
}