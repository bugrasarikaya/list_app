using AutoMapper;
using list_api.Data;
using list_api.Models;
using list_api.Models.DTOs;
using list_api.Repository.Common;
using list_api.Repository.Interface;
namespace list_api.Repository {
	public class CategoryRepository : ICategoryRepository {
		private readonly IListApiDbContext context;
		private readonly IMapper mapper;
		public CategoryRepository(IListApiDbContext context, IMapper mapper) { // Constructing.
			this.context = context;
			this.mapper = mapper;
		}
		public Category Create(CategoryDTO category_dto) { // Creating a category.
			Category category_created = new Category() { Name = Check.NameForConflict<Category>(context, category_dto.Name) };
			context.Categories.Add(category_created);
			context.SaveChanges();
			return category_created;
		}
		public Category? Delete(int id) { // Deleting a category.
			Category category_deleted = Supply.ByID<Category>(context, id);
			Check.ForeignIDForConflict<List, Category>(context, id);
			Check.ForeignIDForConflict<Product, Category>(context, id);
			context.Categories.Remove(category_deleted);
			context.SaveChanges();
			return category_deleted;
		}
		public CategoryViewModel Get(int id) { // Getting a category.
			return mapper.Map<CategoryViewModel>(Supply.ByID<Category>(context, id));
		}
		public ICollection<CategoryViewModel> List() { // Listing all categories.
			ICollection<CategoryViewModel> list_category_view_model = new List<CategoryViewModel>();
			foreach (int id in context.Categories.Select(c => c.ID).ToList()) list_category_view_model.Add(mapper.Map<CategoryViewModel>(Supply.ByID<Category>(context, id)));
			return list_category_view_model;
		}
		public Category Update(int id, CategoryDTO category_dto) { // Updating a category.
			Category category_updated = Supply.ByID<Category>(context, id);
			category_updated.Name = Check.NameForConflict<Category>(context, category_dto.Name);
			context.SaveChanges();
			return category_updated;
		}
		public Category Patch(int id, CategoryPatchDTO category_patch_dto) { // Patching a category.
			Category category_patched = Supply.ByID<Category>(context, id);
			if (!string.IsNullOrEmpty(category_patch_dto.Name)) category_patched.Name = Check.NameForConflict<List>(context, category_patch_dto.Name);
			context.SaveChanges();
			return category_patched;
		}
	}
}