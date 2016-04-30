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
        private VoiceHelper _voiceHelper;

        public MainPresenter(IActivity activity, ITextParserService parserService) : base(activity)
        {
            _parser = new TextParser(parserService);
        }

        public VoiceHelper VoiceHelper { get { return _voiceHelper ?? (new VoiceHelper(Activity)); } }

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
            if (!PersistResult(_parser.Parse(result)))
                VoiceHelper.Speak("No se ha podido procesar el evento");
            else
                VoiceHelper.Speak("El evento ha sido registrado");
            return;
        }

        private bool PersistResult(TextParseResult result)
        {
            var item = Event.FromTextResult(result);
            if (item == null) return false;
            var store = new EventDataStore(new TableAdapter("events", StringSettings.Setttings));
            return store.Add(item);
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
            VoiceHelper.Listen();
        }
    }
}