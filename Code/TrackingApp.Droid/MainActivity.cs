﻿using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Speech;
using System.Threading;
using System.Threading.Tasks;

namespace TrackingApp.Droid
{
    [Activity(Label = "Activity Tracker", MainLauncher = true, Icon = "@drawable/radar")]
    public class MainActivity : Activity, IVoiceActivity
    {
        public event EventHandler<ActivityResultArgs> ActivityResult;
        private Task<string> _onVoiceResult;
        private string _voiceResultValue;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            ActionBar.Hide();
            var mainPresenter = new MainPresenter(this, new TextParserService(StringSettings.Setttings));
        }

        public Task<string> DoVoiceRequest()
        {
            _onVoiceResult = new Task<string>(() => { return _voiceResultValue; });
            var voiceIntent = new Intent(RecognizerIntent.ActionRecognizeSpeech);
            voiceIntent.PutExtra(RecognizerIntent.ExtraLanguageModel, RecognizerIntent.LanguageModelFreeForm);
            voiceIntent.PutExtra(RecognizerIntent.ExtraPrompt, Application.Context.GetString(Resource.String.SpeechMessage));
            voiceIntent.PutExtra(RecognizerIntent.ExtraSpeechInputCompleteSilenceLengthMillis, 1000);
            voiceIntent.PutExtra(RecognizerIntent.ExtraSpeechInputPossiblyCompleteSilenceLengthMillis, 1000);
            voiceIntent.PutExtra(RecognizerIntent.ExtraSpeechInputMinimumLengthMillis, 3000);
            voiceIntent.PutExtra(RecognizerIntent.ExtraMaxResults, 1);
            voiceIntent.PutExtra(RecognizerIntent.ExtraLanguage, "es-ES");
            StartActivityForResult(voiceIntent, (int)Request.Voice);
            return _onVoiceResult;
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            if (requestCode != (int)Request.Voice) return;
            if (resultCode != Result.Ok) return;
            var results = data.GetStringArrayListExtra(RecognizerIntent.ExtraResults);
            if (results.Count <= 0) return;
            var result = string.Join(" ", results);

            _voiceResultValue = result;
            if (_onVoiceResult != null)
                _onVoiceResult.Start();

            if (ActivityResult != null)
            {
                ActivityResult.Invoke(this, new ActivityResultArgs() { RequestCode = requestCode, ResultCode = resultCode, Data = data });
            }
        }
    }
}

