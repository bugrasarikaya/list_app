using list_api.Models;
using list_api.Models.DTOs;
using list_api.Models.ViewModels;
namespace list_api.Repository.Interface {
	public interface ICategoryRepository {
		public Category Create(CategoryDTO category_dto);
		public void Delete(int id);
		public CategoryViewModel Get(int id);
		public ICollection<CategoryViewModel> List();
		public CategoryViewModel Update(int id, CategoryDTO category_dto);
		public CategoryViewModel Patch(int id, CategoryPatchDTO category_patch_dto);
	}
}