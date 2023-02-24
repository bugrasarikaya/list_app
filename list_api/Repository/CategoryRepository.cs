using list_api.Models;
using list_api.Repository.Interface;
using list_api.Data;
namespace list_api.Repository {
	public class CategoryRepository : ICategoryRepository {
		private readonly IListApiDbContext context;
		private Category? category { get; set; }
		public CategoryRepository(IListApiDbContext context) { // Constructing.
			this.context = context;
		}
		public Category Create(Category category) { // Creating a category.
			category = new Category() { Name = category.Name };
			if (context.Categories.Any(c => c.Name == category.Name)) throw new InvalidOperationException("Category already exists.");
			context.Categories.Add(category);
			context.SaveChanges();
			return category;
		}
		public Category? Delete(int id) { // Deleting a category.
			category = context.Categories.FirstOrDefault(c => c.ID == id);
			if (category != null) {
				if (context.Lists.Any(l => l.CategoryID == id)) throw new InvalidOperationException("Category exists in a list.");
				if (context.Products.Any(p => p.CategoryID == id)) throw new InvalidOperationException("Category exists in a product.");
				context.Categories.Remove(category);
				context.SaveChanges();
			}
			return category;
		}
		public Category? Get(int id) { // Getting a category.
			return context.Categories.FirstOrDefault(c => c.ID == id);
		}
		public ICollection<Category> List() { // Listing all categorys.
			return context.Categories.ToList();
		}
		public Category? Update(int id, Category category) { // Updating a category.
			this.category = context.Categories.FirstOrDefault(c => c.ID == id);
			if (this.category != null) {
				this.category.Name = category.Name;
				context.SaveChanges();
			}
			return category;
		}
	}
}