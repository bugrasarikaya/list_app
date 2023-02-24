using list_api.Models;
namespace list_api.Repository.Interface {
	public interface ICategoryRepository {
		public Category Create(Category category);
		public Category? Delete(int id);
		public Category? Get(int id);
		public ICollection<Category> List();
		public Category? Update(int id, Category category);
	}
}