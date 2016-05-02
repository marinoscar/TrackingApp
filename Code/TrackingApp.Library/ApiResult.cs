using System;
using System.Net;

namespace TrackingApp.Droid.Library
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