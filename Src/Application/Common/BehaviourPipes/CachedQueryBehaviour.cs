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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Application.Common.BehaviourPipes
{
	public class CachedQueryBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : ICacheQuery, IRequest<TResponse>
	{
		//private readonly IDistributedCache _cache; // save data cache
		private readonly ICacheService _cache;
		private readonly IHttpContextAccessor _httpContextAccessor; //access to request

		public CachedQueryBehaviour(ICacheService cache, IHttpContextAccessor httpContextAccessor)
		{
			_cache = cache;
			_httpContextAccessor = httpContextAccessor;
		}
		public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
		{
			TResponse response;
			var key = GenerateKey();
			response = await _cache.GetAsync<TResponse>(key);
			if(response == null)
			{
				response = await next(); // go to get response
				if(typeof(TResponse) == typeof(PaginationResponse<ProductDto>))
				{
					await _cache.SetWithTagsAsync(key, response, new TimeSpan(request.HoursSaveData,0,0) , new string[] { CacheTags.Products });
				}
				else
				{
					await _cache.SetAsync(key, response, new TimeSpan(request.HoursSaveData, 0, 0));
				}
			}
			return response;
		}

		private static TimeSpan TimeToLive(TRequest request)
		{
			return new TimeSpan(request.HoursSaveData, 0, 0, 0);
		}

		private string GenerateKey()
		{
			return IdGenerator.GenerateCacheKeyFromRequest(_httpContextAccessor.HttpContext.Request);
		}

	}
}
