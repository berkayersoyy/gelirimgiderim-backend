

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Core.Utilities.IoC;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;

namespace Core.CrossCuttingConcerns.Caching.Microsoft
{
    public class MemoryCacheManager:ICacheManager
    {
        private IMemoryCache _memoryCache;

        /// <summary>
        /// Memory caching for caching using with Microsoft Memory Cache.
        /// </summary>
        /// <param name="memoryCache"></param>
        public MemoryCacheManager(IMemoryCache memoryCache)
        {
            _memoryCache = ServiceTool.ServiceProvider.GetService<IMemoryCache>();
        }
        /// <summary>
        /// Gets the entity from cache by key.
        /// </summary>
        /// <typeparam name="T">Entity</typeparam>
        /// <param name="key">Key</param>
        /// <returns><typeparam name="T"></typeparam></returns>
        public T Get<T>(string key)
        {
            return _memoryCache.Get<T>(key);
        }
        /// <summary>
        /// Gets the entity from cache by key.
        /// </summary>
        /// <param name="key">Key</param>
        /// <returns><typeparam name="object"></typeparam></returns>
        public object Get(string key)
        {
            return _memoryCache.Get(key);
        }
        /// <summary>
        /// Adds to cache the data.
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="data">Data</param>
        /// <param name="duration">Duration</param>
        public void Add(string key, object data, int duration)
        {
            _memoryCache.Set(key, data, TimeSpan.FromMinutes(duration));
        }
        /// <summary>
        /// Checks whether add or not.
        /// </summary>
        /// <param name="key">Key</param>
        /// <returns><typeparam name="bool"></typeparam></returns>
        public bool IsAdd(string key)
        {
            return _memoryCache.TryGetValue(key, out _);
        }
        /// <summary>
        /// Removes the data from cache by key.
        /// </summary>
        /// <param name="key">Key</param>
        public void Remove(string key)
        {
            _memoryCache.Remove(key);
        }
        /// <summary>
        /// Removes the data from cache by pattern, if the method name contains the pattern removes that's cache.
        /// </summary>
        /// <param name="pattern">Pattern</param>
        public void RemoveByPattern(string pattern)
        {
            var cacheEntriesCollectionDefinition = typeof(MemoryCache).GetProperty("EntriesCollection",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var cacheEntriesCollection = cacheEntriesCollectionDefinition.GetValue(_memoryCache) as dynamic;
            List<ICacheEntry> cacheCollectionValues = new List<ICacheEntry>();
            foreach (var cacheItem in cacheEntriesCollection)
            {
                ICacheEntry cacheItemValue = cacheItem.GetType().GetProperty("Value").GetValue(cacheItem, null);
                cacheCollectionValues.Add(cacheItemValue);
            }

            var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
            var keysToRemove = cacheCollectionValues.Where(d => regex.IsMatch(d.Key.ToString())).Select(d => d.Key)
                .ToList();
            foreach (var key in keysToRemove)
            {
                _memoryCache.Remove(key);
            }
        }
    }
}