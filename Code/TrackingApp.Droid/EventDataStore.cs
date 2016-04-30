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
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Table;

namespace TrackingApp.Droid
{
    public class EventDataStore
    {
        public EventDataStore(ITableStore<EventItem> store)
        {
            TableStore = store;
        }

        public ITableStore<EventItem> TableStore { get; private set; }

        public Task<TableResult> AddAsync(EventItem item)
        {
            return TableStore.AddAndPersistAsync(item);
        }
    }
}