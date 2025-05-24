using Application.Interfaces;
using Domain.Entities.Base;
using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
	public class RedisCacheService : ICacheService 
	{
		private readonly IDatabase _database;
		private readonly IConnectionMultiplexer _redis; // برای استفاده از Tag-Based Caching

		public RedisCacheService(IConnectionMultiplexer redis)
		{
			_redis = redis;
			_database = _redis.GetDatabase();

		}

		public async Task<T> GetAsync<T>(string key)
		{
			var jsonData = await _database.StringGetAsync(key);
			if (jsonData.IsNullOrEmpty)
				return default(T);

			return JsonSerializer.Deserialize<T>(jsonData);
		}


		public async Task SetAsync<T>(string key, T value, TimeSpan expiration)
		{
			if (value == null)
				throw new ArgumentNullException(nameof(value));

			var jsonData = JsonSerializer.Serialize(value);
			await _database.StringSetAsync(key, jsonData, expiration);
		}



		// متد برای اضافه کردن با تگ
		public async Task SetWithTagsAsync<T>(string key, T value, TimeSpan expiration, params string[] tags)
		{
			// ابتدا خود مقدار را کش می کنیم
			await SetAsync(key, value, expiration);
		
			foreach (var tag in tags)
			{
				// از یک Redis Set برای نگاشت تگ به کلید استفاده می کنیم.
				// "tag:{tag}" -> {key1, key2, ...}
				await _database.SetAddAsync($"tag:{tag}", key);
			}

		}

		// متد برای حذف بر اساس تگ
		public async Task RemoveByTagAsync(string tag)
		{
			string tagKey = $"tag:{tag}";
			// تمام کلیدهایی که با این تگ هستند را پیدا می کنیم
			var keys = await _database.SetMembersAsync(tagKey);
			if (keys.Length == 0)
				return;
			// کلیدهای پیدا شده و خود تگ را حذف می کنیم
			foreach (var key in keys)
			{
				await _database.KeyDeleteAsync(key.ToString()); // حذف کلید اصلی
			}
			await _database.KeyDeleteAsync(tagKey);  // حذف کلید تگ
		}


		public async Task RemoveAsync(string key)
		{
			await _database.KeyDeleteAsync(key);
		}

		public enum Tags
		{
			Products = 0,

		}
	}
}
