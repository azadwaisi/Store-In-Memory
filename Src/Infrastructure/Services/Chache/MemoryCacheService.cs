using Application.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.Chache
{
	public class MemoryCacheService 
	{
		private readonly IMemoryCache _memoryCache;
		private readonly ILogger<MemoryCacheService> _logger;
		public MemoryCacheService(IMemoryCache memoryCache, ILogger<MemoryCacheService> logger)
		{
			_memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}
		public async Task<T> GetOrSetAsync<T>(string cacheKey, Func<Task<T>> factory, TimeSpan expiration)
		{
			if (string.IsNullOrWhiteSpace(cacheKey))
				throw new ArgumentException("Cache key cannot be null or empty", nameof(cacheKey));

			if (factory == null)
				throw new ArgumentNullException(nameof(factory));

			// Try to get the value from cache
			if (_memoryCache.TryGetValue(cacheKey, out T cachedValue))
			{
				_logger.LogDebug("Cache hit for key: {CacheKey}", cacheKey);
				return cachedValue;
			}

			_logger.LogDebug("Cache miss for key: {CacheKey}", cacheKey);

			// Value not in cache, execute the factory function
			var value = await factory();

			// Set the value in cache with expiration
			var cacheOptions = new MemoryCacheEntryOptions
			{
				AbsoluteExpirationRelativeToNow = expiration,
				SlidingExpiration = null, // Optional: you can add sliding expiration if needed
				Priority = CacheItemPriority.Normal
			};

			_memoryCache.Set(cacheKey, value, cacheOptions);
			_logger.LogDebug("Value cached for key: {CacheKey} with expiration: {Expiration}", cacheKey, expiration);

			return value;
		}

		public void Remove(string cacheKey)
		{
			if (string.IsNullOrWhiteSpace(cacheKey))
				throw new ArgumentException("Cache key cannot be null or empty", nameof(cacheKey));

			_memoryCache.Remove(cacheKey);
			_logger.LogDebug("Cache entry removed for key: {CacheKey}", cacheKey);
		}
	}
}
