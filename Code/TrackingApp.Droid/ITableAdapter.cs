namespace TrackingApp.Droid
{
    public interface ITableAdapter
    {
        ApiResult<object> Add(IEntity entity);
    }
}