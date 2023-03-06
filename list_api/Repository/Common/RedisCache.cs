using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Text;
using list_api.Data;
using list_api.Models;
namespace list_api.Repository.Common {
	public static class RedisCache {
		public static T Add<T>(IDistributedCache cache, IListApiDbContext context) { // Returning a value with key to cache.
			string cache_key;
			T value;
			if (typeof(T) == typeof(List<Category>)) {
				value = (T)Convert.ChangeType(context.Categories.ToList(), typeof(T));
				cache_key = "list_category";
			} else if (typeof(T) == typeof(List<Role>)) {
				value = (T)Convert.ChangeType(context.Roles.ToList(), typeof(T));
				cache_key = "list_role";
			} else {
				value = (T)Convert.ChangeType(context.Statuses.ToList(), typeof(T));
				cache_key = "list_status";
			}
			cache.Set(cache_key, Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(value, Formatting.None, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore })), new DistributedCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromDays(1)).SetAbsoluteExpiration(DateTime.Now.AddMonths(1)));
			return value;
		}
		public static void Remove<T>(IDistributedCache cache) { // Removing a key with value from cache.
			cache.Remove(typeof(T).Name);
		}
		public static T Supply<T>(IDistributedCache cache, IListApiDbContext context) { // Supplying a key with value from cache.
			string cache_key;
			if (typeof(T) == typeof(List<Category>)) cache_key = cache_key = "list_category";
			else if (typeof(T) == typeof(List<Role>)) cache_key = cache_key = "list_role";
			else cache_key = "list_status";
			byte[]? statuses_cache = cache.Get(cache_key);
			if (statuses_cache != null) return JsonConvert.DeserializeObject<T>(Encoding.UTF8.GetString(statuses_cache))!;
			else return Add<T>(cache, context);
		}
		public static void Recache<T>(IDistributedCache cache, IListApiDbContext context) { // Recaching a key with key.
			Remove<T>(cache);
			cache.Set(typeof(T).Name, Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(Add<T>(cache, context))), new DistributedCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromDays(1)).SetAbsoluteExpiration(DateTime.Now.AddMonths(1)));
		}
	}
}