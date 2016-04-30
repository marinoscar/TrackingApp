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
using Android.Speech;

namespace TrackingApp.Droid
{
    public class MainPresenter : PresenterBase
    {
        private int _count;
        private TextParser _parser;
        public MainPresenter(IActivity activity, ITextParserService parserService) : base(activity)
        {
            _parser = new TextParser(parserService);
        }

        public override void BindView()
        {
            Activity.FindViewById<Button>(Resource.Id.MyButton).Click += OnBasicButtonClick;
            Activity.FindViewById<Button>(Resource.Id.SpeechButton).Click += OnSpeechButtonClick;
            Activity.ActivityResult += OnActivityResult;
        }

        private void OnActivityResult(object sender, ActivityResultArgs e)
        {
            if (e.RequestCode != (int)Request.Voice) return;
            if (e.ResultCode != Result.Ok) return;
            var results = e.Data.GetStringArrayListExtra(RecognizerIntent.ExtraResults);
            if (!results.Any()) return;
            var result = string.Join(" ", results);
            PersistResult(_parser.Parse(result));
            return;
        }

        private void PersistResult(TextParseResult result)
        {
            var item = EventItem.FromTextResult(result);
            var store = new EventDataStore(new TableStore<EventItem>("events"));
            var res = store.AddAsync(item);
            res.Wait();
            var vals = res.Result;
        }

        private void OnBasicButtonClick(object sender, EventArgs e)
        {
            var button = (Button)sender;
            button.Text = string.Format("{0} clicks!", _count++);
        }

        private void OnSpeechButtonClick(object sender, EventArgs e)
        {
            var button = (Button)sender;
            if (!Validator.MicCheck(button.Context)) return;
            var voice = new VoiceRecognition(Activity);
            voice.Start();
        }
    }
}