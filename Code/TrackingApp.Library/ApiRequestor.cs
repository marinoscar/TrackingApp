using System;
using RestSharp;
using System.Net;
using Newtonsoft.Json;

namespace TrackingApp.Droid.Library
{
    public class ApiRequestor
    {
        public ApiResult<T> Execute<T>(IRestClient client, IRestRequest request) where T : new()
        {
            return Execute<T>(client, request, null);
        }

        public ApiResult<T> Execute<T>(IRestClient client, IRestRequest request, Action<T, IRestResponse> validation) where T : new()
        {
            return Execute<T>(client, request, HttpStatusCode.OK, validation);
        }

        public ApiResult<T> Execute<T>(IRestClient client, IRestRequest request, HttpStatusCode expectedResult, Action<T, IRestResponse> validation) where T : new()
        {
            var result = new ApiResult<T>();
            IRestResponse response = null;
            try
            {
                response = client.Execute(request);
                result.Message = response.StatusDescription;
                if (response.StatusCode != expectedResult)
                {
                    result.HasErrors = true;
                    result.Result = default(T);
                }
                else
                {
                    result.Result = JsonConvert
                    .DeserializeObject<T>(response.Content, new JsonSerializerSettings() { EqualityComparer = StringComparer.CurrentCultureIgnoreCase });
                }
            }
            catch (Exception ex)
            {
                result.HasErrors = true;
                result.Exception = ex;
                if (response != null)
                    result.Message = response.StatusDescription;
            }
            validation?.Invoke(result.Result, response);
            return result;
        }
    }
}