using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace WorkWearApi.Services
{
    public interface IRepository
    {
        string Get(string key);

        void Add(string key, string value);

        void Update(string key, string value);

        bool Exists(string key);
    }

    public class MemoryRepository : IRepository
    {
        private IDictionary<string, string> _items;

        public MemoryRepository()
        {
            // This repository will be a singleton for access by multiple threads (api requests)
            // So that means potentially multiple threads updating the same key at same time.
            // So we need to handle concurrency
            _items = new ConcurrentDictionary<string, string>();
        }

        public string Get(string key)
        {
            string value;
            var result = _items.TryGetValue(key, out value);
            if (result == false)
            {
                throw new KeyNotFoundException($"Key not found: {key}");
            }
            return value;
        }

        public void Add(string key, string value)
        {
            var result = _items.TryAdd(key, value);
            if (!result) 
            {
                throw new ArgumentException("Key already exists");
            }
        }

        public void Update(string key, string value)
        {            
            if (_items.ContainsKey(key)) _items[key] = value;
            
        }

        public bool Exists(string key)
        {
            return _items.ContainsKey(key);
        }
    }
}
