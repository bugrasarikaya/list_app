using list_api.Models;
using list_api.Repository.Interface;
using list_api.Data;
namespace list_api.Repository {
	public class ClientRepository : IClientRepository {
		private readonly IListApiDbContext context;
		public int IDUser { get; set; }
		public ClientRepository(IListApiDbContext context) { // Constructing.
			this.context = context;
		}
		public List CreateList(List list) { // Creating a list.
			List? list_created = new List() { Name = list.Name, Description = list.Description, IDCategory = list.IDCategory, IDUser = IDUser };
			if (context.Lists.Any(l => l.Name == list.Name && l.IDUser == IDUser)) throw new InvalidOperationException("List already exists.");
			context.Lists.Add(list_created);
			context.SaveChanges();
			return list_created;
		}
		public List? Deletelist(int id_list) { // Deleting a list.
			List? list_deleted = context.Lists.FirstOrDefault(l => l.ID == id_list || l.IDUser == IDUser);
			if (list_deleted != null) {
				context.Lists.Remove(list_deleted);
				context.SaveChanges();
			}
			return list_deleted;
		}
		public List? GetList(int id_list) { // Getting a list.
			return context.Lists.FirstOrDefault(l => l.ID == id_list || l.IDUser == IDUser);
		}
		public ICollection<List> ListLists() { // Listing all lists.
			return context.Lists.Where(l => l.IDUser == IDUser).ToList();
		}
		public List? UpdateList(int id_list, List list) { // Updating a list.
			List? list_updated = context.Lists.FirstOrDefault(l => l.ID == id_list);
			if (list_updated != null) {
				list_updated.IDCategory = list.IDCategory;
				list_updated.IDUser = IDUser;
				if (context.Lists.Any(l => l.Name == list.Name && l.IDUser == IDUser)) throw new InvalidOperationException("List already exists.");
				list_updated.Name = list.Name;
				list_updated.Description = list.Description;
				context.SaveChanges();
			}
			return list_updated;
		}
		public ICollection<Product>? ListListProducts(int id_list) { // Listing all products in a list.
			List list = Supply<List>(id_list);
			List<int>? list_product_ids = context.ListProducts.Where(lp => lp.ID == list.ID).Select(lp => lp.IDProduct).ToList();
			if (list_product_ids != null) return context.Products.Where(p => list_product_ids.Contains(p.ID)).ToList();
			else return null;
		}
		public ListProduct AddProduct(ListProduct list_product) { // Adding a product to a list.
			List list = Supply<List>(list_product.IDList);
			Product? product = Supply<Product>(list_product.IDProduct);
			ListProduct list_product_created = new ListProduct() { IDList = list_product.IDList, IDProduct = list_product.IDProduct, Quantity = list_product.Quantity };
			if (context.ListProducts.Any(lp => lp.IDList == list_product_created.IDList && lp.IDProduct == list_product_created.IDProduct)) throw new InvalidOperationException("Product already exists in list.");
			context.ListProducts.Add(list_product_created);
			list.Cost += product.Price * list_product.Quantity;
			list.DateTime = DateTime.Now;
			context.SaveChanges();
			return list_product_created;
		}
		public ListProduct? RemoveProduct(int id_list, int id_product) { // Removing a product from a list.
			List list = Supply<List>(id_list);
			Product? product = Supply<Product>(id_product);
			ListProduct? list_product_deleted = context.ListProducts.FirstOrDefault(lp => lp.IDList == id_list && lp.IDProduct == id_product);
			if (list_product_deleted != null) {
				list.Cost -= product.Price * list_product_deleted.Quantity;
				list.DateTime = DateTime.Now;
				context.ListProducts.Remove(list_product_deleted);
				context.SaveChanges();
			}
			return list_product_deleted;
		}
		public User? GetUser() { // Getting a user.
			return context.Users.FirstOrDefault(u => u.ID == IDUser);
		}
		public User? UpdateUser(User user) { // Updating a user.
			User? user_updated = context.Users.FirstOrDefault(u => u.ID == IDUser);
			if (user_updated != null) {
				if (context.Users.Any(u => u.Name == user.Name)) throw new InvalidOperationException("User already exists.");
				user_updated.Name = user.Name;
				user_updated.Password = user.Password;
				context.SaveChanges();
			}
			return user_updated;
		}
		public T Supply<T>(int id) { // Checking existence of a record and supplying.
			if (typeof(T) == typeof(List)) {
				List? list = context.Lists.FirstOrDefault(l => l.ID == id || l.IDUser == IDUser);
				if (list != null) return (T)Convert.ChangeType(list, typeof(T));
				else throw new InvalidOperationException("List could not be found.");
			} else {
				Product? product = context.Products.FirstOrDefault(p => p.ID == id);
				if (product != null) return (T)Convert.ChangeType(product, typeof(T));
				else throw new InvalidOperationException("Product could not be found.");
			}
		}
	}
}