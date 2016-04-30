using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Table;

namespace TrackingApp.Droid
{
    public interface ITableStore<T> where T : TableEntity
    {
        CloudTable Table { get; }
        string TableName { get; }

        void Add(T item);
        TableResult AddAndPersist(T item);
        Task<TableResult> AddAndPersistAsync(T item);
        void Delete(T item);
        TableResult DeleteAndPersist(T item);
        Task<TableResult> DeleteAndPersistAsync(T item);
        void Edit(T item);
        TableResult EditAndPersist(T item);
        Task<TableResult> EditAndPersistAsync(T item);
        IList<TableResult> Persist();
        Task<IList<TableResult>> PersistAsync();
    }
}