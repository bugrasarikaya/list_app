using list_api.Models;
using list_api.Repository.Interface;
using list_api.Data;
namespace list_api.Repository {
	public class ProductRepository : IProductRepository {
		private readonly IListApiDbContext context;
		private Product? product { get; set; }
		public ProductRepository(IListApiDbContext context) { // Constructing.
			this.context = context;
		}
		public Product Create(Product product) { // Creating a product.
			product = new Product() { Name = product.Name, Description = product.Description, CategoryID = product.CategoryID, Price = product.Price };
			if (context.Products.Any(p => p.Name == product.Name)) throw new InvalidOperationException("Product already exists.");
			context.Products.Add(product);
			context.SaveChanges();
			return product;
		}
		public Product? Delete(int id) { // Deleting a product.
			product = context.Products.FirstOrDefault(p => p.ID == id);
			if (product != null) {
				context.Products.Remove(product);
				context.SaveChanges();
			}
			return product;
		}
		public Product? Get(int id) { // Getting a product.
			return context.Products.FirstOrDefault(p => p.ID == id);
		}
		public ICollection<Product> List() { // Listing all products.
			return context.Products.ToList();
		}
		public Product? Update(int id, Product product) { // Updating a product.
			this.product = context.Products.FirstOrDefault(p => p.ID == id);
			if (this.product != null) {
				this.product.Name = product.Name;
				this.product.Description = product.Description;
				this.product.CategoryID = product.CategoryID;
				this.product.Price = product.Price;
				context.SaveChanges();
			}
			return product;
		}
	}
}