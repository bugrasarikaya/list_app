using Microsoft.Extensions.Caching.Distributed;
using list_api.Data;
using list_api.Exceptions;
using list_api.Models;
namespace list_api.Repository.Common {
	public static class Check {
		public static int ID<T>(IDistributedCache cache, IListApiDbContext context, int id) { // Checking a record by ID.
			if (typeof(T) == typeof(Category)) {
				if (RedisCache.Supply<List<Category>>(cache, context).Any(c => c.ID == id)) return id;
				else throw new NotFoundException("Category could not be found.");
			} else if (typeof(T) == typeof(List)) {
				if (context.Lists.Any(l => l.ID == id)) return id;
				else throw new NotFoundException("List could not be found.");
			} else if (typeof(T) == typeof(Product)) {
				if (context.Products.Any(p => p.ID == id)) return id;
				else throw new NotFoundException("Product could not be found.");
			} else if (typeof(T) == typeof(Role)) {
				if (RedisCache.Supply<List<Role>>(cache, context).Any(r => r.ID == id)) return id;
				else throw new NotFoundException("Role could not be found.");
			} else if (typeof(T) == typeof(Status)) {
				if (RedisCache.Supply<List<Status>>(cache, context).Any(s => s.ID == id)) return id;
				else throw new NotFoundException("Status could not be found.");
			} else {
				if (context.Users.Any(u => u.ID == id)) return id;
				else throw new NotFoundException("User could not be found.");
			}
		}
		public static string Name<T>(IDistributedCache cache, IListApiDbContext context, string name) { // Checking a record by name.
			if (typeof(T) == typeof(Category)) {
				if (RedisCache.Supply<List<Category>>(cache, context).Any(c => c.Name == name)) return name;
				else throw new NotFoundException("Category could not be found.");
			} else if (typeof(T) == typeof(List)) {
				if (context.Lists.Any(l => l.Name == name)) return name;
				else throw new NotFoundException("List could not be found.");
			} else if (typeof(T) == typeof(Product)) {
				if (context.Products.Any(p => p.Name == name)) return name;
				else throw new NotFoundException("Product could not be found.");
			} else if (typeof(T) == typeof(Role)) {
				if (RedisCache.Supply<List<Role>>(cache, context).Any(r => r.Name == name)) return name;
				else throw new NotFoundException("Role could not be found.");
			} else if (typeof(T) == typeof(Status)) {
				if (RedisCache.Supply<List<Status>>(cache, context).Any(s => s.Name == name)) return name;
				else throw new NotFoundException("Status could not be found.");
			} else {
				if (context.Users.Any(u => u.Name == name)) return name;
				else throw new NotFoundException("User could not be found.");
			}
		}
		public static string NameForConflict<T>(IDistributedCache cache, IListApiDbContext context, string name, int id_user = 0) { // Checking a record name for conflict.
			if (typeof(T) == typeof(Category)) {
				if (!RedisCache.Supply<List<Category>>(cache, context).Any(c => c.Name == name)) return name;
				else throw new ConflictException("Category already exists.");
			} else if (typeof(T) == typeof(List)) {
				if (!context.Lists.Where(l => l.IDUser == id_user).Any(l => l.Name == name)) return name;
				else throw new ConflictException("List already exists.");
			} else if (typeof(T) == typeof(Product)) {
				if (!context.Products.Any(p => p.Name == name)) return name;
				else throw new ConflictException("Product already exists.");
			} else if (typeof(T) == typeof(Role)) {
				if (!RedisCache.Supply<List<Role>>(cache, context).Any(r => r.Name == name)) return name;
				else throw new ConflictException("Role already exists.");
			} else if (typeof(T) == typeof(Status)) {
				if (!RedisCache.Supply<List<Status>>(cache, context).Any(s => s.Name == name)) return name;
				else throw new ConflictException("Status already exists.");
			} else {
				if (!context.Users.Any(u => u.Name == name)) return name;
				else throw new ConflictException("User already exists.");
			}
		}
		public static void ForeignIDForConflict<T1, T2>(IDistributedCache cache, IListApiDbContext context, int id_1, int id_2 = 0) { // Checking a foreign ID in another record for conflict.
			if (typeof(T1) == typeof(List) && typeof(T2) == typeof(Category)) {
				if (context.Lists.Any(l => l.IDCategory == id_1)) throw new ConflictException("Category exists in a list.");
			} else if (typeof(T1) == typeof(List) && typeof(T2) == typeof(Status)) {
				if (context.Lists.Any(l => l.IDStatus == id_1)) throw new ConflictException("Status exists in a list.");
			} else if (typeof(T1) == typeof(List) && typeof(T2) == typeof(User)) {
				if (context.Lists.Any(l => l.IDUser == id_1)) throw new ConflictException("User exists in a list.");
			} else if (typeof(T1) == typeof(ListProduct) && typeof(T2) == typeof(ListProduct)) {
				if (context.ListProducts.Any(lp => lp.IDList == id_1 && lp.IDProduct == id_2)) throw new ConflictException("Product already exists in a list.");
			} else if (typeof(T1) == typeof(ListProduct) && typeof(T2) == typeof(Product)) {
				if (context.ListProducts.Any(lp => lp.IDProduct == id_1)) throw new ConflictException("Product exists in a list.");
			} else if (typeof(T1) == typeof(Product) && typeof(T2) == typeof(Category)) {
				if (context.Products.Any(p => p.IDCategory == id_1)) throw new ConflictException("Category exists in a product.");
			} else if (typeof(T1) == typeof(User) && typeof(T2) == typeof(Role)) {
				if (context.Users.Any(u => u.IDRole == id_1)) throw new ConflictException("Role exists in a user.");
			}
		}
	}
}