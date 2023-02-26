using list_api.Models;
using list_api.Repository.Interface;
using list_api.Data;
using System.Net;
using System.Web.Http;
namespace list_api.Repository {
	public class CategoryRepository : ICategoryRepository {
		private readonly IListApiDbContext context;
		public CategoryRepository(IListApiDbContext context) { // Constructing.
			this.context = context;
		}
		public Category Create(Category category) { // Creating a category.
			Category? category_created = new Category() { Name = category.Name };
			if (context.Categories.Any(c => c.Name == category.Name)) throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Conflict) { ReasonPhrase = "Category already exists." });
			context.Categories.Add(category_created);
			context.SaveChanges();
			return category_created;
		}
		public Category? Delete(int id) { // Deleting a category.
			Category? category_deleted = context.Categories.FirstOrDefault(c => c.ID == id);
			if (category_deleted != null) {
				if (context.Lists.Any(l => l.IDCategory == id)) throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Conflict) { ReasonPhrase = "Category exists in a list." });
				if (context.Products.Any(p => p.IDCategory == id)) throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Conflict) { ReasonPhrase = "Category exists in a product." });
				context.Categories.Remove(category_deleted);
				context.SaveChanges();
			}
			return category_deleted;
		}
		public Category? Get(int id) { // Getting a category.
			return context.Categories.FirstOrDefault(c => c.ID == id);
		}
		public ICollection<Category> List() { // Listing all categories.
			return context.Categories.ToList();
		}
		public Category? Update(int id, Category category) { // Updating a category.
			Category? category_updated = context.Categories.FirstOrDefault(c => c.ID == id);
			if (category_updated != null) {
				if (context.Categories.Any(c => c.Name == category.Name)) throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Conflict) { ReasonPhrase = "Category already exists." });
				category_updated.Name = category.Name;
				context.SaveChanges();
			}
			return category_updated;
		}
	}
}