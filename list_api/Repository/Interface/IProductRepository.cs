using list_api.Models;
namespace list_api.Repository.Interface {
	public interface IProductRepository {
		public Product Create(Product product);
		public Product? Delete(int id);
		public Product? Get(int id);
		public ICollection<Product> List();
		public Product? Update(int id, Product product);
	}
}