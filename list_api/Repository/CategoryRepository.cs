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
		public void Delete(string param_category) { // Deleting a category.
			Category category_deleted;
			if (int.TryParse(param_category, out int id_category)) category_deleted = Supply.ByID<Category>(cache, context, id_category);
			else category_deleted = Supply.ByName<Category>(cache, context, param_category);
			Check.ForeignIDForConflict<List, Category>(cache, context, category_deleted.ID);
			Check.ForeignIDForConflict<Product, Category>(cache, context, category_deleted.ID);
			context.Categories.Remove(category_deleted);
			context.SaveChanges();
			RedisCache.Recache<Category>(cache, context);
		}
		public CategoryViewModel Get(string param_category) { // Getting a category.
			Category category;
			if (int.TryParse(param_category, out int id_category)) category = Supply.ByID<Category>(cache, context, id_category);
			else category = Supply.ByName<Category>(cache, context, param_category);
			return Fill.ViewModel<CategoryViewModel, Category>(cache, context, mapper, category);
		}
		public ICollection<CategoryViewModel> List() { // Listing all categories.
			ICollection<CategoryViewModel> list_category_view_model = new List<CategoryViewModel>();
			foreach (int id in Supply.List<Category>(cache, context).OrderBy(b => b.Name).Select(c => c.ID).ToList()) list_category_view_model.Add(Fill.ViewModel<CategoryViewModel, Category>(cache, context, mapper, Supply.ByID<Category>(cache, context, id)));
			return list_category_view_model;
		}
		public CategoryViewModel Update(string param_category, CategoryDTO category_dto) { // Updating a category.
			Category category_updated;
			if (int.TryParse(param_category, out int id_category)) category_updated = Supply.ByID<Category>(cache, context, id_category);
			else category_updated = Supply.ByName<Category>(cache, context, param_category);
			category_updated.Name = Check.NameForConflict<Category>(cache, context, category_dto.Name);
			context.SaveChanges();
			return Fill.ViewModel<CategoryViewModel, Category>(cache, context, mapper, category_updated);
		}
		public CategoryViewModel Patch(string param_category, CategoryPatchDTO category_patch_dto) { // Patching a category.
			Category category_patched;
			if (int.TryParse(param_category, out int id_category)) category_patched = Supply.ByID<Category>(cache, context, id_category);
			else category_patched = Supply.ByName<Category>(cache, context, param_category);
			if (!string.IsNullOrEmpty(category_patch_dto.Name)) category_patched.Name = Check.NameForConflict<List>(cache, context, category_patch_dto.Name);
			context.SaveChanges();
			return Fill.ViewModel<CategoryViewModel, Category>(cache, context, mapper, category_patched);
		}
	}
}