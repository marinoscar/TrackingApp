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
using System.Net;

namespace TrackingApp.Droid
{
    public class ApiResult<T> where T : new()
    {
        public T Result { get; set; }
        public HttpStatusCode Code { get; set; }
        public Exception Exception { get; set; }
        public string Message { get; set; }
        public bool HasErrors { get; set; }
    }
}