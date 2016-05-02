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
using TrackingApp.Droid.Library;
using TrackingApp.Droid.Library.Models;
using TrackingApp.Droid.Library.DataAccess;

namespace TrackingApp.Droid
{
    public class MainPresenter : PresenterBase
    {
        private TextParser _parser;
        private VoiceProvider _voiceHelper = null;

        public MainPresenter(IVoiceActivity activity, ITextParserService parserService) : base(activity)
        {
            _parser = new TextParser(parserService);
        }

        public VoiceProvider VoiceProvider { get { return _voiceHelper ?? (new VoiceProvider((IVoiceActivity)Activity)); } }

        public override void BindView()
        {
            Activity.FindViewById<Button>(Resource.Id.SpeechButton).Click += OnSpeechButtonClick;
        }

        public void RegisterEvent()
        {
            var result = VoiceProvider.ListenAsync();
            result.ContinueWith((t) => {
                DoProcessEvent(t.Result);
            });
        }

        private void DoProcessEvent(string requestText)
        {
            if (!PersistResult(_parser.Parse(requestText)))
                VoiceProvider.Speak("No se ha podido procesar el evento");
            else
                VoiceProvider.Speak("El evento ha sido registrado");
        }

        private void OnActivityResult(object sender, ActivityResultArgs e)
        {
            if (e.RequestCode != (int)Request.Voice) return;
            if (e.ResultCode != Result.Ok) return;
            var results = e.Data.GetStringArrayListExtra(RecognizerIntent.ExtraResults);
            if (!results.Any()) return;
            var result = string.Join(" ", results);
            if (!PersistResult(_parser.Parse(result)))
                VoiceProvider.Speak("No se ha podido procesar el evento");
            else
                VoiceProvider.Speak("El evento ha sido registrado");
            return;
        }

        private bool PersistResult(TextParseResult result)
        {
            var item = Event.FromTextResult(result);
            if (item == null) return false;
            var store = new EventDataStore(new TableAdapter("events", StringSettings.Setttings));
            return store.Add(item);
        }


        private void OnSpeechButtonClick(object sender, EventArgs e)
        {
            var button = (Button)sender;
            if (!Validator.MicCheck(button.Context)) return;
            RegisterEvent();
        }
    }
}