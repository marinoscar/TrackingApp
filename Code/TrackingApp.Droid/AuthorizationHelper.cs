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
using RestSharp;
using System.Collections.Specialized;
using System.Collections;
using System.Security.Cryptography;

namespace TrackingApp.Droid
{
    public class AuthorizationHelper
    {
        public AuthorizationHelper(string account, string key)
        {
            Account = account;
            Key = key;
        }

        public string Account { get; private set; }
        public string Key { get; private set; }

        public string Get(Method method, DateTime time, string resource)
        {
            var methodName = Enum.GetName(typeof(Method), method).ToUpperInvariant();
            var signature = string.Format("{0}\n\n{1}\n{2}\n{3}",
                    methodName,
                    "application/json",
                    time.ToString("R", System.Globalization.CultureInfo.InvariantCulture),
                    GetCanonicalizedResource(resource, Account)
                    );
            var header = "SharedKey {0}:{1}";
            var bytes = Encoding.UTF8.GetBytes(signature);
            using (var hash = new HMACSHA256(Convert.FromBase64String(Key)))
            {
                header = string.Format(header, Account, Convert.ToBase64String(hash.ComputeHash(bytes)));
            }
            return header;
        }

        private string GetCanonicalizedResource(string resource, string accountName)
        {
            var sanitized = resource;
            if (sanitized.StartsWith("/")) sanitized = sanitized.Remove(0, 1);
            return string.Format("/{0}/{1}", accountName, sanitized);
        }
    }
}