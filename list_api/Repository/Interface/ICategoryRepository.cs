using list_api.Models;
using list_api.Models.DTOs;
namespace list_api.Repository.Interface {
	public interface ICategoryRepository {
		public Category Create(CategoryDTO category_dto);
		public Category? Delete(int id);
		public CategoryViewModel Get(int id);
		public ICollection<CategoryViewModel> List();
		public Category Update(int id, CategoryDTO category_dto);
		public Category Patch(int id, CategoryPatchDTO category_patch_dto);
	}
}