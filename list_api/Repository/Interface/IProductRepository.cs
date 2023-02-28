using list_api.Models;
using list_api.Models.DTOs;
namespace list_api.Repository.Interface {
	public interface IProductRepository {
		public Product Create(ProductDTO product_dto);
		public Product? Delete(int id);
		public Product? Get(int id);
		public ICollection<ProductViewModel> List();
		public ICollection<ProductViewModel> List(int id_category);
		public Product Update(int id, ProductDTO product_dto);
		public Product Patch(int id, ProductPatchDTO product_patch_dto);
	}
}