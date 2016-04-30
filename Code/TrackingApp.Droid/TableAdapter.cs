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
using Newtonsoft.Json.Linq;

namespace TrackingApp.Droid
{
    public class TableAdapter : ITableAdapter
    {
        private AuthorizationHelper _auth;

        public TableAdapter(string tableName, IDictionary<string, string> settings)
        {
            TableName = tableName;
            Settings = settings;
            _auth = new AuthorizationHelper(Settings["TableStore"], Settings["TableStoreKey"]);
        }

        public string TableName { get; private set; }
        public IDictionary<string, string> Settings { get; private set; }


        public ApiResult<object> Add(IEntity entity)
        {
            var apiCaller = new ApiRequestor();
            return apiCaller.Execute<object>(GetClient(), GetRequest(entity));
        }


        private IRestClient GetClient()
        {
            return new RestClient(string.Format("https://{0}/.table.core.windows.net", Settings["TableStore"]));
        }

        private IRestRequest GetRequest(IEntity entity)
        {
            var body = entity.ToOData();
            var ts = DateTime.UtcNow;
            var auth = _auth.Get(Method.POST, ts, TableName);
            var request = new RestRequest(TableName, Method.POST);
            request.AddHeader("Authorization", auth);
            request.AddHeader("x-ms-date", ts.ToString("R"));
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Content-Length", body.Length.ToString());
            request.AddHeader("Accept", "application/json;odata=nometadata");
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            return request;
        }
    }
}