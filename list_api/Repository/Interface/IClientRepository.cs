using list_api.Models;
using list_api.Models.DTOs;
using list_api.Models.ViewModels;
namespace list_api.Repository.Interface {
	public interface IClientRepository {
		public int IDUser { get; set; }
		public ListViewModel CreateList(ClientListDTO list_client_dto);
		public List? DeleteList(string param_list);
		public ListViewModel GetList(string param_list);
		public ICollection<ListViewModel> ListLists();
		public ICollection<ListViewModel> ListListsByCategory(string param_category);
		public ICollection<ListViewModel> ListByDateTimeCompleting(DateTime date_time_completing);
		public ICollection<ListViewModel> ListByDateTimeCreating(DateTime date_time_creating);
		public ICollection<ListViewModel> ListByDateTimeUpdating(DateTime date_time_updating);
		public ICollection<ListViewModel> ListByCategoryAndDateTimeCompleting(string param_category, DateTime date_time_completing);
		public ICollection<ListViewModel> ListByCategoryAndDateTimeCreating(string param_category, DateTime date_time_creating);
		public ICollection<ListViewModel> ListByCategoryAndDateTimeUpdating(string param_category, DateTime date_time_updating);
		public ListViewModel UpdateList(string param_list, ClientListDTO list_client_dto);
		public ListViewModel PatchList(string param_list, ClientListPatchDTO list_client_patch_dto);
		public ListViewModel AddProduct(ListProductDTO list_product_dto);
		public ListViewModel RemoveProduct(string param_list, int id_product);
		public ClientUserViewModel GetUser();
		public ClientUserViewModel UpdateUser(ClientUserDTO user_client_dto);
		public ClientUserViewModel PatchUser(ClientUserPatchDTO user_client_patch_dto);
	}
}