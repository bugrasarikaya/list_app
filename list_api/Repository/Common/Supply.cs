using Microsoft.Extensions.Caching.Distributed;
using list_api.Data;
using list_api.Exceptions;
using list_api.Models;
namespace list_api.Repository.Common {
	public static class Supply {
		public static T ByID<T>(IDistributedCache cache, IListApiDbContext context, int id_1, int id_2 = 0) { // Supplying a record by ID after checking.
			if (typeof(T) == typeof(Brand)) {
				Brand? brand = List<Brand>(cache, context).SingleOrDefault(b => b.ID == id_1);
				if (brand != null) return (T)Convert.ChangeType(brand, typeof(T));
				else throw new NotFoundException("Brand could not be found.");
			} else if (typeof(T) == typeof(Category)) {
				Category? category = List<Category>(cache, context).SingleOrDefault(c => c.ID == id_1);
				if (category != null) return (T)Convert.ChangeType(category, typeof(T));
				else throw new NotFoundException("Category could not be found.");
			} else if (typeof(T) == typeof(List)) {
				List? list = List<List>(cache, context).SingleOrDefault(l => l.ID == id_1);
				if (list != null) return (T)Convert.ChangeType(list, typeof(T));
				else throw new NotFoundException("List could not be found.");
			} else if (typeof(T) == typeof(ListProduct)) {
				ListProduct? list_product = List<ListProduct>(cache, context).SingleOrDefault(l => l.IDList == id_1 && l.IDProduct == id_2);
				if (list_product != null) return (T)Convert.ChangeType(list_product, typeof(T));
				else throw new NotFoundException("List product could not be found.");
			} else if (typeof(T) == typeof(Product)) {
				Product? product = List<Product>(cache, context).SingleOrDefault(p => p.ID == id_1);
				if (product != null) return (T)Convert.ChangeType(product, typeof(T));
				else throw new NotFoundException("Product could not be found.");
			} else if (typeof(T) == typeof(Role)) {
				Role? role = List<Role>(cache, context).SingleOrDefault(r => r.ID == id_1);
				if (role != null) return (T)Convert.ChangeType(role, typeof(T));
				else throw new NotFoundException("Role could not be found.");
			} else if (typeof(T) == typeof(Status)) {
				Status? status = List<Status>(cache, context).SingleOrDefault(s => s.ID == id_1);
				if (status != null) return (T)Convert.ChangeType(status, typeof(T));
				else throw new NotFoundException("Status could not be found.");
			} else {
				User? user = List<User>(cache, context).SingleOrDefault(u => u.ID == id_1);
				if (user != null) return (T)Convert.ChangeType(user, typeof(T));
				else throw new NotFoundException("User could not be found.");
			}
		}
		public static T ByName<T>(IDistributedCache cache, IListApiDbContext context, string name, int id_user = 0) { // Supplying a record by Name after checking.
			if (typeof(T) == typeof(Brand)) {
				Brand? brand = List<Brand>(cache, context).SingleOrDefault(b => b.Name == name);
				if (brand != null) return (T)Convert.ChangeType(brand, typeof(T));
				else throw new NotFoundException("Brand could not be found.");
			} else if (typeof(T) == typeof(Category)) {
				Category? category = List<Category>(cache, context).SingleOrDefault(c => c.Name == name);
				if (category != null) return (T)Convert.ChangeType(category, typeof(T));
				else throw new NotFoundException("Category could not be found.");
			} else if (typeof(T) == typeof(List)) {
				List? list = List<List>(cache, context).Where(l => l.IDUser == id_user).SingleOrDefault(l => l.Name == name);
				if (list != null) return (T)Convert.ChangeType(list, typeof(T));
				else throw new NotFoundException("List could not be found.");
			} else if (typeof(T) == typeof(Role)) {
				Role? role = List<Role>(cache, context).SingleOrDefault(r => r.Name == name);
				if (role != null) return (T)Convert.ChangeType(role, typeof(T));
				else throw new NotFoundException("Role could not be found.");
			} else if (typeof(T) == typeof(Status)) {
				Status? status = List<Status>(cache, context).SingleOrDefault(s => s.Name == name);
				if (status != null) return (T)Convert.ChangeType(status, typeof(T));
				else throw new NotFoundException("Status could not be found.");
			} else {
				User? user = List<User>(cache, context).SingleOrDefault(u => u.Name == name);
				if (user != null) return (T)Convert.ChangeType(user, typeof(T));
				else throw new NotFoundException("User could not be found.");
			}
		}
		public static List<T> List<T>(IDistributedCache cache, IListApiDbContext context) { // Supplying a record list.
			if (typeof(T) == typeof(Brand)) return (List<T>)Convert.ChangeType(context.Brands.ToList(), typeof(List<T>));
			else if (typeof(T) == typeof(Category)) return (List<T>)Convert.ChangeType(RedisCache.Supply<List<Category>>(cache, context), typeof(List<T>));
			else if (typeof(T) == typeof(List)) return (List<T>)Convert.ChangeType(context.Lists.ToList(), typeof(List<T>));
			else if (typeof(T) == typeof(ListProduct)) return (List<T>)Convert.ChangeType(context.ListProducts.ToList(), typeof(List<T>));
			else if (typeof(T) == typeof(Product)) return (List<T>)Convert.ChangeType(context.Products.ToList(), typeof(List<T>));
			else if (typeof(T) == typeof(Role)) return (List<T>)Convert.ChangeType(RedisCache.Supply<List<Role>>(cache, context), typeof(List<T>));
			else if (typeof(T) == typeof(Status)) return (List<T>)Convert.ChangeType(RedisCache.Supply<List<Status>>(cache, context), typeof(List<T>));
			else return (List<T>)Convert.ChangeType(context.Users.ToList(), typeof(List<T>));
		}
	}
}