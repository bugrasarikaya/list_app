using list_api.Models;
using list_api.Models.DTOs;
using list_api.Models.ViewModels;
namespace list_api.Repository.Interface {
	public interface IBrandRepository {
		public Brand Create(BrandDTO category_dto);
		public void Delete(string param_brand);
		public BrandViewModel Get(string param_brand);
		public ICollection<BrandViewModel> List();
		public BrandViewModel Update(string param_brand, BrandDTO category_dto);
		public BrandViewModel Patch(string param_brand, BrandPatchDTO category_patch_dto);
	}
}