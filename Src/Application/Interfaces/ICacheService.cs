using Domain.Entities.Base;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
	//public interface ICacheService //for distributed redis 
	//{
	//	Task<T> GetAsync<T>(string key);
	//	Task SetAsync<T>(string key, T value, TimeSpan expiration);
	//	Task SetWithTagsAsync<T>(string key, T value, TimeSpan expiration, params string[] tags);
	//	Task RemoveByTagAsync(string tag);
	//	Task RemoveAsync(string key);
	//}
	//For In-Memory use behavior pipline
	////public interface ICacheService
	////{
	////	// Synchronous Methods
	////	T? Get<T>(string key) where T : class;
	////	bool Set<T>(string key, T value, TimeSpan? expiration = null) where T : class;
	////	bool Remove(string key);
	////	bool RemoveByPattern(string pattern);
	////	void Clear();
	////	bool Exists(string key);

	////	// Asynchronous Methods
	////	Task<T?> GetAsync<T>(string key) where T : class;
	////	Task<bool> SetAsync<T>(string key, T value, TimeSpan? expiration = null) where T : class;
	////	Task<bool> RemoveAsync(string key);
	////	Task<bool> RemoveByPatternAsync(string pattern);
	////	Task ClearAsync();
	////	Task<bool> ExistsAsync(string key);

	////	// Advanced Methods
	////	Task<T> GetOrSetAsync<T>(string key, Func<Task<T>> getItem, TimeSpan? expiration = null) where T : class;
	////	T GetOrSet<T>(string key, Func<T> getItem, TimeSpan? expiration = null) where T : class;

	////	// Cache Statistics
	////	CacheStatistics GetStatistics();

	////	// Multiple Keys Operations
	////	Task<Dictionary<string, T?>> GetMultipleAsync<T>(IEnumerable<string> keys) where T : class;
	////	Task<bool> SetMultipleAsync<T>(Dictionary<string, T> items, TimeSpan? expiration = null) where T : class;
	////}
	////public record CacheStatistics
	////{
	////	public int TotalItems { get; init; }
	////	public long HitCount { get; init; }
	////	public long MissCount { get; init; }
	////	public double HitRatio => TotalRequests > 0 ? (double)HitCount / TotalRequests : 0;
	////	public long TotalRequests => HitCount + MissCount;
	////	public DateTime LastAccessed { get; init; }
	////	public long MemoryUsageBytes { get; init; }
	////}
	//For In-Memory Hybrid Cash No Use Behavior Pipline
	public interface ICacheService
	{
		Task<T> GetOrSetAsync<T>(string cacheKey, Func<Task<T>> factory, TimeSpan expiration);
		void Remove(string cacheKey);
	}
}
