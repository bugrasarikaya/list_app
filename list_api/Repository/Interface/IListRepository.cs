using list_api.Models;
using list_api.Models.DTOs;
using list_api.Models.ViewModels;
namespace list_api.Repository.Interface {
	public interface IListRepository {
		public List Create(ListDTO list_dto);
		public void Delete(int id);
		public ListViewModel Get(int id);
		public ICollection<ListViewModel> List();
		public ICollection<ListViewModel> ListByCategory(int id_category);
		public ICollection<ListViewModel> ListByUser(int id_user);
		public ICollection<ListViewModel> ListByCategoryAndUser(int id_category, int id_user);
		public ListViewModel Update(int id, ListDTO list_dto);
		public ListViewModel Patch(int id_list, ListPatchDTO list_patch_dto);
		public ListViewModel AddProduct(ListProductDTO list_product_dto);
		public ListViewModel RemoveProduct(int id_list, int id_product);
		public ListViewModel ClearProducts(int id_list);
	}
}