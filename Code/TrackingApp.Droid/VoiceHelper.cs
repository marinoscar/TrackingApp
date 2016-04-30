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

namespace TrackingApp.Droid
{

    enum Request { Voice = 100 }

    public class VoiceHelper: TextToSpeech.IOnInitListener
    {
        TextToSpeech speaker;
        string toSpeak;

        public VoiceHelper(IActivity activity)
        {
            Activity = activity;
        }

        public IActivity Activity { get; private set; }

        public IntPtr Handle { get { return default(IntPtr); } }

        public void Dispose()
        {
        }

        public void Listen()
        {
            var voiceIntent = new Intent(RecognizerIntent.ActionRecognizeSpeech);
            voiceIntent.PutExtra(RecognizerIntent.ExtraLanguageModel, RecognizerIntent.LanguageModelFreeForm);
            voiceIntent.PutExtra(RecognizerIntent.ExtraPrompt, Application.Context.GetString(Resource.String.SpeechMessage));
            voiceIntent.PutExtra(RecognizerIntent.ExtraSpeechInputCompleteSilenceLengthMillis, 1500);
            voiceIntent.PutExtra(RecognizerIntent.ExtraSpeechInputPossiblyCompleteSilenceLengthMillis, 1500);
            voiceIntent.PutExtra(RecognizerIntent.ExtraSpeechInputMinimumLengthMillis, 15000);
            voiceIntent.PutExtra(RecognizerIntent.ExtraMaxResults, 1);
            voiceIntent.PutExtra(RecognizerIntent.ExtraLanguage, "es-ES");
            Activity.StartActivityForResult(voiceIntent, (int)Request.Voice);
        }

        public void OnInit([GeneratedEnum] OperationResult status)
        {
            if (status.Equals(OperationResult.Success))
            {
                speaker.Speak(toSpeak, QueueMode.Flush, null, null);
            }
        }
        public void Speak(string text)
        {
            if (speaker == null) speaker = new TextToSpeech(Application.Context, this);
            toSpeak = text;
            speaker.SetLanguage(new Locale("es", "ES"));
            speaker.Speak(toSpeak, QueueMode.Flush, null, null);
        }
    }
}