namespace BusinessLayer.Interfaces
{
    public interface ICacheService
    {
        object GetByKey(string cacheObjectName);

        void SetCache(string key, object value);
    }
}
