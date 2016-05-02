using TrackingApp.Droid.Library.Models;

namespace TrackingApp.Droid.Library.DataAccess
{
    public interface ITableAdapter
    {
        ApiResult<object> Add(IEntity entity);
    }
}