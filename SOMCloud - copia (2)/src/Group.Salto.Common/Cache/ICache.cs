namespace Group.Salto.Common.Cache
{
    public interface ICache
    {
        object GetData(string category);
        object GetData(string category, string key);
        void SetData(string category, string key, object value);
        bool RemoveData(string category);
        bool RemoveData(string category, string key);
    }
}