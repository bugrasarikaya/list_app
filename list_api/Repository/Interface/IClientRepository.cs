using list_api.Models;
using list_api.Models.DTOs;
namespace list_api.Repository.Interface {
	public interface IClientRepository {
		public int IDUser { get; set; }
		public List CreateList(ListDTO list_dto);
		public List? DeleteList(int id_list);
		public ListViewModel? GetList(int id_list);
		public ICollection<ListViewModel> ListLists();
		public ICollection<ListViewModel> ListListsByCategory(int id_category);
		public List UpdateList(int id_list, ListDTO list_dto);
		public List PatchList(int id_list, ListClientPatchDTO list_client_patch_dto);
		public ListViewModel AddProduct(ListProductDTO list_product_dto);
		public ListViewModel? RemoveProduct(int id_list, int id_product);
		public List ClearList(int id_list);
		public UserViewModel GetUser();
		public UserViewModel UpdateUser(UserClientDTO user_client_dto);
	}
}