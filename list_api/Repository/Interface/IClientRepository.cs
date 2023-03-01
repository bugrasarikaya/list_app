using list_api.Models;
using list_api.Models.DTOs;
using list_api.Models.ViewModels;
namespace list_api.Repository.Interface {
	public interface IClientRepository {
		public int IDUser { get; set; }
		public List CreateList(ClientListDTO list_client_dto);
		public void DeleteList(int id_list);
		public ListViewModel GetList(int id_list);
		public ICollection<ListViewModel> ListLists();
		public ICollection<ListViewModel> ListListsByCategory(int id_category);
		public ListViewModel UpdateList(int id_list, ClientListDTO list_client_dto);
		public ListViewModel PatchList(int id_list, ClientListPatchDTO list_client_patch_dto);
		public ListViewModel AddProduct(ListProductDTO list_product_dto);
		public ListViewModel RemoveProduct(int id_list, int id_product);
		public ListViewModel ClearProducts(int id_list);
		public ClientUserViewModel GetUser();
		public ClientUserViewModel UpdateUser(ClientUserDTO user_client_dto);
		public ClientUserViewModel PatchUser(ClientUserPatchDTO user_client_patch_dto);
	}
}