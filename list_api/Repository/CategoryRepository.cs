using AutoMapper;
using Microsoft.Extensions.Caching.Distributed;
using list_api.Data;
using list_api.Models;
using list_api.Models.DTOs;
using list_api.Models.ViewModels;
using list_api.Repository.Common;
using list_api.Repository.Interface;
namespace list_api.Repository {
	public class CategoryRepository : ICategoryRepository {
		private readonly IDistributedCache cache;
		private readonly IListApiDbContext context;
		private readonly IMapper mapper;
		public CategoryRepository(IDistributedCache cache, IListApiDbContext context, IMapper mapper) { // Constructing.
			this.cache = cache;
			this.context = context;
			this.mapper = mapper;
		}
		public Category Create(CategoryDTO category_dto) { // Creating a category.
			Category category_created = new Category() { Name = Check.NameForConflict<Category>(cache, context, category_dto.Name) };
			context.Categories.Add(category_created);
			context.SaveChanges();
			RedisCache.Recache<Category>(cache, context);
			return category_created;
		}
		public void Delete(int id) { // Deleting a category.
			Category category_deleted = Supply.ByID<Category>(cache, context, id);
			Check.ForeignIDForConflict<List, Category>(cache, context, id);
			Check.ForeignIDForConflict<Product, Category>(cache, context, id);
			context.Categories.Remove(category_deleted);
			context.SaveChanges();
			RedisCache.Recache<Category>(cache, context);
		}
		public CategoryViewModel Get(int id) { // Getting a category.
			return mapper.Map<CategoryViewModel>(Supply.ByID<Category>(cache, context, id));
		}
		public ICollection<CategoryViewModel> List() { // Listing all categories.
			ICollection<CategoryViewModel> list_category_view_model = new List<CategoryViewModel>();
			foreach (int id in context.Categories.Select(c => c.ID).ToList()) list_category_view_model.Add(mapper.Map<CategoryViewModel>(Supply.ByID<Category>(cache, context, id)));
			return list_category_view_model;
		}
		public CategoryViewModel Update(int id, CategoryDTO category_dto) { // Updating a category.
			Category category_updated = Supply.ByID<Category>(cache, context, id);
			category_updated.Name = Check.NameForConflict<Category>(cache, context, category_dto.Name);
			context.SaveChanges();
			return mapper.Map<CategoryViewModel>(category_updated);
		}
		public CategoryViewModel Patch(int id, CategoryPatchDTO category_patch_dto) { // Patching a category.
			Category category_patched = Supply.ByID<Category>(cache, context, id);
			if (!string.IsNullOrEmpty(category_patch_dto.Name)) category_patched.Name = Check.NameForConflict<List>(cache, context, category_patch_dto.Name);
			context.SaveChanges();
			return mapper.Map<CategoryViewModel>(category_patched);
		}
	}
}