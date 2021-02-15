using Ondato_WebApi.Models;
using System.Collections.Generic;

namespace Ondato_WebApi.Helpers
{
    public static class DataStore
    {
        private static readonly Dictionary<string, CherryItem> _cherries = new Dictionary<string, CherryItem>();
            
        public static string CreateUpdate(string key, CherryItem cherry)
        {
            CherryItem existingItem;
            if (!_cherries.TryGetValue(key, out existingItem))
            {
                _cherries.Add(key, cherry);
            }
            else
            {
                _cherries[key] = cherry;
            }
            return key;
        }

        public static void Delete(string key)
        {
            CherryItem existingItem;
            if (!_cherries.TryGetValue(key, out existingItem))
            {
                throw new KeyNotFoundException();
            }
            _cherries.Remove(key);
        }

        public static CherryItem Get(string key)
        {
            CherryItem existingItem;
            if (!_cherries.TryGetValue(key, out existingItem))
            {
                throw new KeyNotFoundException();
            }

            return existingItem;
        }
    }
}
