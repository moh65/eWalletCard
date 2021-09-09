using Microsoft.Extensions.Caching.Memory;
using System;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace Mofid.eWallet.Infra.Caches
{
    public interface ICache
    {
        T Get<T>(string key);
        void Set<T>(string key, T value);
        Task<T> GetOrSetAsync<T>(string key, Func<Task<T>> func);
        T GetOrSet<T>(string key, Func<T> func);
        void Remove(string key);
    }

   
}
