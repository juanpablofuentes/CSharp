using System.Collections.Generic;

namespace Group.Salto.Common.Cache
{
    public class MemoryCache : ICache
    {
        protected Dictionary<string, Dictionary<string, object>> data;

        public MemoryCache()
        {
            data = new Dictionary<string, Dictionary<string, object>>();
        }

        public object GetData(string category, string key)
        {
            if (data.ContainsKey(category)
                && data[category].ContainsKey(key))
                return data[category][key];
            return null;
        }

        public object GetData(string category)
        {
            if (data.ContainsKey(category))
                return data[category];
            return null;
        }

        public void SetData(string category, string key, object value)
        {
            if (!data.ContainsKey(category)) data.TryAdd(category,
                new Dictionary<string, object> { { key, value } });
            else if (!data[category].ContainsKey(key))
                data[category].Add(key, value);
            else data[category][key] = value;
        }

        public bool RemoveData(string category)
        {
            if (data.ContainsKey(category))
            { 
                return data.Remove(category);
            }

            return false;
        }

        public bool RemoveData(string category, string key)
        {
            if (data.ContainsKey(category) && data[category].ContainsKey(key))
            {
                return data[category].Remove(key);
            }

            return false;
        }
    }
}
