using list_api.Models;
using list_api.Data;
using list_api.Repository.Interface;
namespace list_api.Repository {
	public class ListProductRepository : IListProductRepository {
		private readonly IListApiDbContext context;
		public ListProductRepository(IListApiDbContext context) { // Constructing.
			this.context = context;
		}
		public ListProduct Create(ListProduct list_product) { // Adding a product to a list.
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
		public ListProduct? Delete(int id_list, int id_product) { // Removing a product from a list.
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
		public ListProduct? Get(int id_list, int id_product) { // Getting a product from a list.
			return context.ListProducts.FirstOrDefault(lp => lp.IDList == id_list && lp.IDProduct == id_product);
		}
		public ICollection<ListProduct> List() { // Listing all list products in all lists.
			return context.ListProducts.ToList();
		}
		public ICollection<ListProduct> List(int id_list) { // Listing all list products in a list.
			return context.ListProducts.Where(lp => lp.IDList == id_list).ToList();
		}
		public ICollection<ListProduct> ListByCategory(int id_category) { // Listing all list products by category in all lists.
			return context.ListProducts.Where(lp=> context.Products.Any(p => p.IDCategory == lp.IDProduct && p.IDCategory == id_category)).ToList();
		}
		public ICollection<ListProduct> ListByCategory(int id_list, int id_category) { // Listing all list products by category in a list.
			return context.ListProducts.Where(lp => lp.IDList == id_list && context.Products.Any(p => p.IDCategory == lp.IDProduct && p.IDCategory == id_category)).ToList();
		}
		public ListProduct? Update(ListProduct list_product) { // Updating a product in a list.
			List list = Supply<List>(list_product.IDList);
			Product? product = Supply<Product>(list_product.IDProduct);
			ListProduct? list_product_updated = context.ListProducts.FirstOrDefault(lp => lp.IDList == list_product.IDList && lp.IDProduct == list_product.IDProduct);
			if (list_product_updated != null) {
				list_product_updated.IDList = list_product.IDList;
				list_product_updated.IDProduct = list_product.IDProduct;
				list.Cost -= product.Price * list_product_updated.Quantity;
				list_product_updated.Quantity = list_product.Quantity;
				list.Cost += product.Price * list_product_updated.Quantity;
				list.DateTime = DateTime.Now;
				context.SaveChanges();
			}
			return list_product_updated;
		}
		public T Supply<T>(int id) { // Checking existence of a record and supplying.
			if (typeof(T) == typeof(List)) {
				List? list = context.Lists.FirstOrDefault(l => l.ID == id);
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