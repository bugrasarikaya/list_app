using list_api.Models;
using list_api.Repository.Interface;
using list_api.Data;
namespace list_api.Repository {
	public class ProductRepository : IProductRepository {
		private readonly IListApiDbContext context;
		public ProductRepository(IListApiDbContext context) { // Constructing.
			this.context = context;
		}
		public Product Create(Product product) { // Creating a product.
			Product? product_created = new Product() { Name = product.Name, Description = product.Description, IDCategory = product.IDCategory, Price = product.Price };
			if (context.Products.Any(p => p.Name == product.Name)) throw new InvalidOperationException("Product already exists.");
			context.Products.Add(product_created);
			context.SaveChanges();
			return product_created;
		}
		public Product? Delete(int id) { // Deleting a product.
			Product? product_deleted = context.Products.FirstOrDefault(p => p.ID == id);
			if (product_deleted != null) {
				context.Products.Remove(product_deleted);
				context.SaveChanges();
			}
			return product_deleted;
		}
		public Product? Get(int id) { // Getting a product.
			return context.Products.FirstOrDefault(p => p.ID == id);
		}
		public ICollection<Product> List() { // Listing all products.
			return context.Products.ToList();
		}
		public Product? Update(int id, Product product) { // Updating a product.
			Product? product_updated = context.Products.FirstOrDefault(p => p.ID == id);
			if (product_updated != null) {
				if (context.Products.Any(p => p.Name == product.Name)) throw new InvalidOperationException("Product already exists.");
				product_updated.Name = product.Name;
				product_updated.Description = product.Description;
				product_updated.IDCategory = product.IDCategory;
				product_updated.Price = product.Price;
				context.SaveChanges();
			}
			return product_updated;
		}
	}
}