using Application.Common.Tags;
using Application.Contracts;
using Application.Dtos.Products;
using Application.Helpers;
using Application.Interfaces;
using Application.Wrappers;
using Domain.Entities.Products;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Application.Common.BehaviourPipes
{
	public class CachedQueryBehaviour<TRequest, TResponse> //: IPipelineBehavior<TRequest, TResponse> 
		where TRequest : ICacheQuery, IRequest<TResponse> 
		where TResponse : class
	{
		////private readonly IDistributedCache _cache; // save data cache
		//private readonly ICacheService _cache;
		//private readonly IHttpContextAccessor _httpContextAccessor; //access to request
		//private readonly ILogger<CachedQueryBehaviour<TRequest, TResponse>> _logger;

		//public CachedQueryBehaviour(ICacheService cache, IHttpContextAccessor httpContextAccessor, ILogger<CachedQueryBehaviour<TRequest, TResponse>> logger)
		//{
		//	_cache = cache ?? throw new ArgumentNullException(nameof(cache));
		//	_httpContextAccessor = httpContextAccessor;
		//	_logger = logger ?? throw new ArgumentNullException(nameof(logger));
		//}
		//public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
		//{
		//	//var key = GenerateKey(request);
		//	//_logger.LogDebug("Checking cache for key: {CacheKey}", key);
		//	//// Try to get from cache
		//	//var cachedResponse = await _cache.GetAsync<TResponse>(key);
		//	//if (cachedResponse != null)
		//	//{
		//	//	_logger.LogDebug("Cache hit for key: {CacheKey}", key);
		//	//	return cachedResponse;
		//	//}

		//	//_logger.LogDebug("Cache miss for key: {CacheKey}. Executing request.", key);

		//	//// Execute the actual request
		//	//var response = await next();

		//	//// Cache the response if it's not null
		//	//if (response != null)
		//	//{
		//	//	var expiration = TimeToLive(request);
		//	//	await CacheResponseAsync(key, response, expiration, request);
		//	//}

		//	//return response;
		//	return ;
		//}
		//private async Task CacheResponseAsync(string key, TResponse response, TimeSpan expiration, TRequest request)
		//{
		//	//try
		//	//{
		//	//	// Special handling for PaginationResponse<ProductDto>
		//	//	if (typeof(TResponse) == typeof(PaginationResponse<ProductDto>))
		//	//	{
		//	//		// اگر سیستم کش شما از Tags پشتیبانی می‌کند، می‌توانید این قسمت را فعال کنید
		//	//		// فعلاً از SetAsync معمولی استفاده می‌کنیم
		//	//		await _cache.SetAsync(key, response, expiration);

		//	//		// اگر نیاز به Tag-based caching دارید، باید interface و implementation را گسترش دهید
		//	//		_logger.LogDebug("Cached response with product tag for key: {CacheKey}, Expiration: {Expiration}", key, expiration);
		//	//	}
		//	//	else
		//	//	{
		//	//		await _cache.SetAsync(key, response, expiration);
		//	//		_logger.LogDebug("Cached response for key: {CacheKey}, Expiration: {Expiration}", key, expiration);
		//	//	}
		//	//}
		//	//catch (Exception ex)
		//	//{
		//	//	_logger.LogError(ex, "Failed to cache response for key: {CacheKey}", key);
		//	//	// Don't throw - caching failure shouldn't break the request
		//	//}
		//}


		//private static TimeSpan TimeToLive(TRequest request)
		//{
		//	return new TimeSpan(request.HoursSaveData, 0, 0, 0);
		//}

		//private string GenerateKey(TRequest request)
		//{
		//	var httpContext = _httpContextAccessor.HttpContext;
		//	if (httpContext?.Request == null)
		//	{
		//		// Fallback key generation if HTTP context is not available
		//		return $"{typeof(TRequest).Name}_{request.GetHashCode()}";
		//	}

		//	try
		//	{
		//		return IdGenerator.GenerateCacheKeyFromRequest(httpContext.Request);
		//	}
		//	catch (Exception ex)
		//	{
		//		_logger.LogWarning(ex, "Failed to generate cache key from request. Using fallback method.");
		//		return $"{typeof(TRequest).Name}_{request.GetHashCode()}_{DateTime.UtcNow.Ticks}";
		//	}
		//}

	}
}
