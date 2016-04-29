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

        public MainPresenter(IActivity activity) : base(activity)
        {

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