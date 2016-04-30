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
    public static class StringSettings
    {
        private static IDictionary<string, string> _settings;

        public static IDictionary<string, string> Setttings
        {
            get
            {
                if (_settings == null) LoadSettings();
                return _settings;
            }
        }

        public static void LoadSettings()
        {
            _settings = new Dictionary<string, string>()
            {
                {"ApplicationName",  Application.Context.GetString(Resource.String.ApplicationName)},
                {"Hello",  Application.Context.GetString(Resource.String.Hello)},
                {"LuisEndPoint",  Application.Context.GetString(Resource.String.LuisEndPoint)},
                {"LuisId",  Application.Context.GetString(Resource.String.LuisId)},
                {"LuisKey",  Application.Context.GetString(Resource.String.LuisKey)},
                {"SpeechButton_Text",  Application.Context.GetString(Resource.String.SpeechButton_Text)},
                {"SpeechMessage",  Application.Context.GetString(Resource.String.SpeechMessage)},
                {"TableStore",  Application.Context.GetString(Resource.String.TableStore)},
                {"TableStoreKey",  Application.Context.GetString(Resource.String.TableStoreKey)},
            };
        }
    }
}