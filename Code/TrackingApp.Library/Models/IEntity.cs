using System;

namespace TrackingApp.Droid.Library.Models
{
    public interface IEntity
    {
        string PartitionKey { get; set; }
        string RowKey { get; set; }
        DateTimeOffset Timestamp { get; set; }
        string ToOData();
    }
}