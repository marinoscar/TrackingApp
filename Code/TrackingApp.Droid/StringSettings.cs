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
using TrackingApp.Library.Interfaces;

namespace TrackingApp.Droid
{
    public class StringSettings : IStringSettings
    {
        private static StringSettings _settings;
        private IDictionary<string, string> _values;

        public static IStringSettings Setttings
        {
            get
            {
                if (_settings == null)
                {
                    _settings = new StringSettings();
                    _settings.LoadSettings();
                }
                return _settings;
            }
        }

        public string this[string key]
        {
            get
            {
                if (_settings == null) LoadSettings();
                return _settings[key];
            }
        }


        public void LoadSettings()
        {
            
            _values = new Dictionary<string, string>()
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