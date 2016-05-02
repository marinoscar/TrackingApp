using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TrackingApp.Droid.Library.Models;

namespace TrackingApp.Droid.Library.Models
{
    public abstract class EntityBase : IEntity
    {
        public EntityBase()
        {
            RowKey = Guid.NewGuid().ToString();
        }

        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public DateTimeOffset Timestamp { get; set; }

        public abstract string ToOData();
    }
}