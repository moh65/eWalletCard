using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading.Tasks;

namespace Mofid.eWallet.Infra.Caches
{
	public class InMemoryCache : ICache
	{

		private IMemoryCache _cache;

		public InMemoryCache(IMemoryCache cache)
		{
			_cache = cache;
		}

		public T Get<T>(string key)
		{
			return _cache.Get<T>(key);
		}

		public T GetOrSet<T>(string key, Func<T> func)
		{
			return _cache.GetOrCreate<T>(key, entry => func());
		}

		public async Task<T> GetOrSetAsync<T>(string key, Func<Task<T>> func)
		{
			return await _cache.GetOrCreateAsync<T>(key, entry => func());
		}

		public void Remove(string key)
		{
			_cache.Remove(key);
		}

		public void Set<T>(string key, T value)
		{
			_cache.Set<T>(key, value);
		}


	}
}
