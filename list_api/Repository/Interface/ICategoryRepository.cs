using list_api.Models;
using list_api.Models.DTOs;
using list_api.Models.ViewModels;
namespace list_api.Repository.Interface {
	public interface ICategoryRepository {
		public Category Create(CategoryDTO category_dto);
		public void Delete(string param_category);
		public CategoryViewModel Get(string param_category);
		public ICollection<CategoryViewModel> List();
		public CategoryViewModel Update(string param_category, CategoryDTO category_dto);
		public CategoryViewModel Patch(string param_category, CategoryPatchDTO category_patch_dto);
	}
}