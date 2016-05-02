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
using Android.Speech.Tts;
using Java.Util;
using System.Threading.Tasks;
using TrackingApp.Library;

namespace TrackingApp.Droid
{

    enum Request { Voice = 100 }

    public class VoiceProvider: Java.Lang.Object, TextToSpeech.IOnInitListener, IVoiceProvider
    {
        TextToSpeech _speaker;
        string _toSpeak;

        public VoiceProvider(IVoiceActivity activity)
        {
            Activity = activity;
        }

        public IVoiceActivity Activity { get; private set; }

        public Task<string> ListenAsync()
        {
            var task = Activity.DoVoiceRequest();
            return task;
        }

        public string Listen()
        {
            var task = ListenAsync();
            while (task.Status != TaskStatus.Running)
            {
            }
            task.Wait();
            return task.Result;
        }

        public void OnInit([GeneratedEnum] OperationResult status)
        {
            if (status.Equals(OperationResult.Success))
            {
                DoSpeak(_toSpeak);
            }
        }
        public void Speak(string text)
        {
            if (_speaker == null) _speaker = new TextToSpeech(Application.Context, this);
            _toSpeak = text;
            DoSpeak(_toSpeak);
        }

        private void DoSpeak(string text)
        {
            _speaker.SetLanguage(new Locale("es", "ES"));
            var p = new Dictionary<string, string>();
            _speaker.Speak(text, QueueMode.Flush, p);
        }
    }
}