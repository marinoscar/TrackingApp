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
using System.Net;

namespace TrackingApp.Droid
{
    public class ApiRequestor
    {
        public ApiResult<T> Execute<T>(IRestClient client, IRestRequest request) where T : new()
        {
            return Execute<T>(client, request, null);
        }

        public ApiResult<T> Execute<T>(IRestClient client, IRestRequest request, Action<T, IRestResponse> validation) where T : new()
        {
            var result = new ApiResult<T>();
            IRestResponse<T> response = null;
            try
            {
                response = client.Execute<T>(request);
                result.Message = response.StatusDescription;
                result.Result = response.Data;
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    result.HasErrors = true;
                }
            }
            catch(Exception ex)
            {
                result.HasErrors = true;
                result.Exception = ex;
                if(response != null)
                    result.Message = response.StatusDescription;
            }
            validation?.Invoke(result.Result, response);
            return result;
        }
    }
}