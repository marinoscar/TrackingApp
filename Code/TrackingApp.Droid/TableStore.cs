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
using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using System.Threading.Tasks;

namespace TrackingApp.Droid
{
    public class TableStore<T> where T : TableEntity
    {

        private enum Action { None, Add, Edit, Delete }
        private List<Tuple<T, Action>> _items;
        private TableBatchOperation _operations;

        public TableStore(string tableName)
        {
            TableName = tableName;
            var account = new CloudStorageAccount(new StorageCredentials("", ""), true);
            var client = account.CreateCloudTableClient();
            Table = client.GetTableReference(TableName);
            _operations = new TableBatchOperation();
            _items = new List<Tuple<T, Action>>();
        }

        public string TableName { get; private set; }
        public CloudTable Table { get; private set; }

        public void Add(T item)
        {
            _items.Add(new Tuple<T, Action>(item, Action.Add));
        }

        public void Edit(T item)
        {
            _items.Add(new Tuple<T, Action>(item, Action.Edit));
        }

        public void Delete(T item)
        {
            _items.Add(new Tuple<T, Action>(item, Action.Delete));
        }

        public Task<IList<TableResult>> PersistAsync()
        {
            _items.Where(i => i.Item2 == Action.Add).ToList().ForEach(i => _operations.Insert(i.Item1));
            _items.Where(i => i.Item2 == Action.Edit).ToList().ForEach(i => _operations.InsertOrReplace(i.Item1));
            _items.Where(i => i.Item2 == Action.Delete).ToList().ForEach(i => _operations.Delete(i.Item1));
            var result = Table.ExecuteBatchAsync(_operations);
            result.GetAwaiter().OnCompleted(Clear);
            return result;
        }

        public IList<TableResult> Persist()
        {
            var result =  PersistAsync();
            result.Wait();
            return result.Result;
        }

        private void Clear()
        {
            _operations.Clear();
            _items.Clear();
        }
    }
}