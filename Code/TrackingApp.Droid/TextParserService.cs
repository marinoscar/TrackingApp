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
using Android.Content.Res;

namespace TrackingApp.Droid
{

    public interface ITextParserService
    {
        ApiResult<TextParserServiceResult> Parse(string text);
    }

    public class TextParserService : ITextParserService
    {
        public ApiResult<TextParserServiceResult> Parse(string text)
        {
            var dic = new Dictionary<string, string>()
            {
                {"url",  Application.Context.GetString(Resource.String.LuisEndPoint)},
                {"id",  Application.Context.GetString(Resource.String.LuisId)},
                {"key",  Application.Context.GetString(Resource.String.LuisKey)},
            };
            var client = new RestClient(dic["url"]);
            var request = GetRequest(dic, text);
            var requestor = new ApiRequestor();
            var result =  requestor.Execute<List<TextParserServiceResult>>(client, request);
            return new ApiResult<TextParserServiceResult>()
            {
                Code = result.Code,
                Exception = result.Exception,
                HasErrors = result.HasErrors,
                Message = result.Message,
                Result = result.Result.FirstOrDefault()
            };
        }

        private IRestRequest GetRequest(Dictionary<string, string> dic, string text)
        {
            var request = new RestRequest(string.Format("{0}/predict", dic["id"]), Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Ocp-Apim-Subscription-Key", dic["key"]);
            request.AddJsonBody(new string[] { text });
            return request;
        }
    }

    public class TextParserServiceResult
    {
        public TextParserServiceResult()
        {
            IntentsResults = new List<IntentResult>();
            EntitiesResults = new List<EntityResult>();
            TokenizedText = new List<string>();
        }

        public List<IntentResult> IntentsResults { get; set; }
        public List<EntityResult> EntitiesResults { get; set; }
        public string UtteranceText { get; set; }

        public List<string> TokenizedText { get; set; }
    }

    public class IntentResult
    {
        public string Name { get; set; }
        public string Label { get; set; }
        public double Score { get; set; }
    }

    public class EntityResult
    {
        public string Name { get; set; }
        public string Word { get; set; }
        public string Color { get; set; }
        public bool IsBuiltInExtractor { get; set; }
    }

    public class Indices
    {
        public int StartToken { get; set; }
        public int EndToken { get; set; }
    }
}