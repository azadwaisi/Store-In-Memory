using Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
	public interface ICacheService
	{
		Task<T> GetAsync<T>(string key);
		Task SetAsync<T>(string key, T value, TimeSpan expiration);
		Task SetWithTagsAsync<T>(string key, T value, TimeSpan expiration, params string[] tags);
		Task RemoveByTagAsync(string tag);
		Task RemoveAsync(string key);
	}
}
