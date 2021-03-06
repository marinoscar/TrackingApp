using System;
using RestSharp;
using System.Net;
using TrackingApp.Droid.Library.Models;
using TrackingApp.Droid.Library.Configuration;

namespace TrackingApp.Droid.Library.DataAccess
{
    public class TableAdapter : ITableAdapter
    {
        private AuthorizationHelper _auth;

        public TableAdapter(string tableName, IStringSettings settings)
        {
            TableName = tableName;
            Settings = settings;
            _auth = new AuthorizationHelper(Settings["TableStore"], Settings["TableStoreKey"]);
        }

        public string TableName { get; private set; }
        public IStringSettings Settings { get; private set; }


        public ApiResult<object> Add(IEntity entity)
        {
            var apiCaller = new ApiRequestor();
            return apiCaller.Execute<object>(GetClient(), GetRequest(entity), HttpStatusCode.Created, null);
        }


        private IRestClient GetClient()
        {
            return new RestClient(string.Format("https://{0}.table.core.windows.net", Settings["TableStore"]));
        }

        private IRestRequest GetRequest(IEntity entity)
        {
            var body = entity.ToOData();
            var ts = DateTime.UtcNow;
            var auth = _auth.Get(Method.POST, ts, TableName);
            var request = new RestRequest(TableName, Method.POST);
            request.AddHeader("Authorization", auth);
            request.AddHeader("x-ms-date", ts.ToHeader());
            request.AddHeader("x-ms-version", "2015-04-05");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Content-Length", body.Length.ToString());
            request.AddHeader("Accept", "application/json;odata=nometadata");
            request.AddHeader("DataServiceVersion", "3.0;NetFx");
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            return request;
        }
    }
}