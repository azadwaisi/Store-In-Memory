using Application.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Infrastructure.Services.Chache
{
	public class InMemoryCacheService : IDisposable
	{
		private readonly IMemoryCache _memoryCache;
		private readonly ILogger<InMemoryCacheService> _logger;
		private readonly CacheOptions _options;
		private readonly ConcurrentDictionary<string, DateTime> _keyTracker;
		private long _hitCount;
		private long _missCount;
		private DateTime _lastAccessed;

		public InMemoryCacheService(
			IMemoryCache memoryCache,
			ILogger<InMemoryCacheService> logger,
			IOptions<CacheOptions> options)
		{
			_memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
			_options = options.Value ?? throw new ArgumentNullException(nameof(options));
			_keyTracker = new ConcurrentDictionary<string, DateTime>();
			_lastAccessed = DateTime.UtcNow;
		}

		#region Synchronous Methods

		public T? Get<T>(string key) where T : class
		{
			if (string.IsNullOrWhiteSpace(key))
				throw new ArgumentException("Key cannot be null or empty", nameof(key));

			try
			{
				var fullKey = GenerateKey<T>(key);
				var result = _memoryCache.Get<T>(fullKey);

				UpdateStatistics(result != null);

				if (result != null)
				{
					_logger.LogDebug("Cache hit for key: {Key}", key);
					return result;
				}

				_logger.LogDebug("Cache miss for key: {Key}", key);
				return null;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error getting item from cache with key: {Key}", key);
				return null;
			}
		}

		public bool Set<T>(string key, T value, TimeSpan? expiration = null) where T : class
		{
			if (string.IsNullOrWhiteSpace(key))
				throw new ArgumentException("Key cannot be null or empty", nameof(key));

			if (value == null)
				throw new ArgumentNullException(nameof(value));

			try
			{
				var fullKey = GenerateKey<T>(key);
				var options = CreateCacheEntryOptions(expiration);

				_memoryCache.Set(fullKey, value, options);
				_keyTracker.TryAdd(fullKey, DateTime.UtcNow);

				_logger.LogDebug("Item cached with key: {Key}, Expiration: {Expiration}",
					key, expiration?.ToString() ?? "Default");

				return true;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error setting item in cache with key: {Key}", key);
				return false;
			}
		}

		public bool Remove(string key)
		{
			if (string.IsNullOrWhiteSpace(key))
				throw new ArgumentException("Key cannot be null or empty", nameof(key));

			try
			{
				var fullKey = GenerateKey<object>(key);
				_memoryCache.Remove(fullKey);
				_keyTracker.TryRemove(fullKey, out _);

				_logger.LogDebug("Item removed from cache with key: {Key}", key);
				return true;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error removing item from cache with key: {Key}", key);
				return false;
			}
		}

		public bool RemoveByPattern(string pattern)
		{
			if (string.IsNullOrWhiteSpace(pattern))
				throw new ArgumentException("Pattern cannot be null or empty", nameof(pattern));

			try
			{
				var regex = new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
				var keysToRemove = _keyTracker.Keys
					.Where(key => regex.IsMatch(key))
					.ToList();

				foreach (var key in keysToRemove)
				{
					_memoryCache.Remove(key);
					_keyTracker.TryRemove(key, out _);
				}

				_logger.LogDebug("Removed {Count} items matching pattern: {Pattern}",
					keysToRemove.Count, pattern);

				return keysToRemove.Count > 0;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error removing items by pattern: {Pattern}", pattern);
				return false;
			}
		}

		public void Clear()
		{
			try
			{
				// Note: IMemoryCache doesn't have a built-in Clear method
				// We need to dispose and recreate, but since we're using DI,
				// we'll just remove tracked keys
				var keys = _keyTracker.Keys.ToList();
				foreach (var key in keys)
				{
					_memoryCache.Remove(key);
				}

				_keyTracker.Clear();
				_logger.LogInformation("Cache cleared. Removed {Count} items", keys.Count);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error clearing cache");
			}
		}

		public bool Exists(string key)
		{
			if (string.IsNullOrWhiteSpace(key))
				return false;

			var fullKey = GenerateKey<object>(key);
			return _memoryCache.TryGetValue(fullKey, out _);
		}

		public T GetOrSet<T>(string key, Func<T> getItem, TimeSpan? expiration = null) where T : class
		{
			var cached = Get<T>(key);
			if (cached != null)
				return cached;

			var item = getItem();
			if (item != null)
			{
				Set(key, item, expiration);
			}

			return item;
		}

		#endregion

		#region Asynchronous Methods

		public Task<T?> GetAsync<T>(string key) where T : class
		{
			return Task.FromResult(Get<T>(key));
		}

		public Task<bool> SetAsync<T>(string key, T value, TimeSpan? expiration = null) where T : class
		{
			return Task.FromResult(Set(key, value, expiration));
		}

		public Task<bool> RemoveAsync(string key)
		{
			return Task.FromResult(Remove(key));
		}

		public Task<bool> RemoveByPatternAsync(string pattern)
		{
			return Task.FromResult(RemoveByPattern(pattern));
		}

		public Task ClearAsync()
		{
			Clear();
			return Task.CompletedTask;
		}

		public Task<bool> ExistsAsync(string key)
		{
			return Task.FromResult(Exists(key));
		}

		public async Task<T> GetOrSetAsync<T>(string key, Func<Task<T>> getItem, TimeSpan? expiration = null) where T : class
		{
			var cached = await GetAsync<T>(key);
			if (cached != null)
				return cached;

			var item = await getItem();
			if (item != null)
			{
				await SetAsync(key, item, expiration);
			}

			return item;
		}

		public async Task<Dictionary<string, T?>> GetMultipleAsync<T>(IEnumerable<string> keys) where T : class
		{
			var result = new Dictionary<string, T?>();

			foreach (var key in keys)
			{
				result[key] = await GetAsync<T>(key);
			}

			return result;
		}

		public async Task<bool> SetMultipleAsync<T>(Dictionary<string, T> items, TimeSpan? expiration = null) where T : class
		{
			var tasks = items.Select(kvp => SetAsync(kvp.Key, kvp.Value, expiration));
			var results = await Task.WhenAll(tasks);

			return results.All(r => r);
		}

		#endregion

		#region Statistics

		//public CacheStatistics GetStatistics()
		//{
		//	return new CacheStatistics
		//	{
		//		TotalItems = _keyTracker.Count,
		//		HitCount = Interlocked.Read(ref _hitCount),
		//		MissCount = Interlocked.Read(ref _missCount),
		//		LastAccessed = _lastAccessed,
		//		MemoryUsageBytes = GC.GetTotalMemory(false) // Approximate
		//	};
		//}

		#endregion

		#region Private Methods

		private string GenerateKey<T>(string key)
		{
			var typeName = typeof(T).Name;
			return $"{_options.KeyPrefix}:{typeName}:{key}";
		}

		private MemoryCacheEntryOptions CreateCacheEntryOptions(TimeSpan? expiration)
		{
			var options = new MemoryCacheEntryOptions
			{
				Priority = CacheItemPriority.Normal
			};

			var expirationTime = expiration ?? _options.DefaultExpiration;

			options.SetAbsoluteExpiration(expirationTime);
			options.SetSlidingExpiration(TimeSpan.FromMinutes(_options.SlidingExpirationMinutes));

			// Register cleanup callback
			options.RegisterPostEvictionCallback((key, value, reason, state) =>
			{
				if (key is string keyStr)
				{
					_keyTracker.TryRemove(keyStr, out _);
					_logger.LogDebug("Cache item evicted: {Key}, Reason: {Reason}", keyStr, reason);
				}
			});

			return options;
		}

		private void UpdateStatistics(bool isHit)
		{
			if (isHit)
				Interlocked.Increment(ref _hitCount);
			else
				Interlocked.Increment(ref _missCount);

			_lastAccessed = DateTime.UtcNow;
		}

		#endregion

		public void Dispose()
		{
			_keyTracker.Clear();
			_memoryCache?.Dispose();
		}
	}
	// Configuration Options
	public class CacheOptions
	{
		public const string SectionName = "Cache";

		public string KeyPrefix { get; set; } = "MyApp";
		public TimeSpan DefaultExpiration { get; set; } = TimeSpan.FromMinutes(30);
		public int SlidingExpirationMinutes { get; set; } = 10;
		public long MaxMemorySize { get; set; } = 100 * 1024 * 1024; // 100MB
	}
}
