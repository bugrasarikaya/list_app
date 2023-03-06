using list_api.Models;
using list_api.Models.DTOs;
using list_api.Models.ViewModels;
namespace list_api.Repository.Interface {
	public interface IProductRepository {
		public ProductViewModel Create(ProductDTO product_dto);
		public Product? Delete(int id);
		public ProductViewModel Get(int id);
		public ICollection<ProductViewModel> List();
		public ICollection<ProductViewModel> ListByCategory(string param_category);
		public ProductViewModel Update(int id, ProductDTO product_dto);
		public ProductViewModel Patch(int id, ProductPatchDTO product_patch_dto);
	}
}