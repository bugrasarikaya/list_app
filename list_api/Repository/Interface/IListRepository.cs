using list_api.Models;
using list_api.Models.DTOs;
using list_api.Models.ViewModels;
namespace list_api.Repository.Interface {
	public interface IListRepository {
		public ListViewModel Create(ListDTO list_dto);
		public List? Delete(int id);
		public ListViewModel Get(int id);
		public ICollection<ListViewModel> List();
		public ICollection<ListViewModel> ListByCategory(string param_category);
		public ICollection<ListViewModel> ListByDateTimeCompleting(DateTime date_time_completing);
		public ICollection<ListViewModel> ListByDateTimeCreating(DateTime date_time_creating);
		public ICollection<ListViewModel> ListByDateTimeUpdating(DateTime date_time_updating);
		public ICollection<ListViewModel> ListByUser(string param_user);
		public ICollection<ListViewModel> ListByCategoryAndDateTimeCompleting(string param_category, DateTime date_time_completing);
		public ICollection<ListViewModel> ListByCategoryAndDateTimeCreating(string param_category, DateTime date_time_creating);
		public ICollection<ListViewModel> ListByCategoryAndDateTimeUpdating(string param_category, DateTime date_time_updating);
		public ICollection<ListViewModel> ListByCategoryAndUser(string param_category, string param_user);
		public ICollection<ListViewModel> ListByDateTimeCompletingAndUser(DateTime date_time_completing, string param_user);
		public ICollection<ListViewModel> ListByDateTimeCreatingAndUser(DateTime date_time_creating, string param_user);
		public ICollection<ListViewModel> ListByDateTimeUpdatingAndUser(DateTime date_time_updating, string param_user);
		public ICollection<ListViewModel> ListByCategoryAndDateTimeCompletingAndUser(string param_category, DateTime date_time_completing, string param_user);
		public ICollection<ListViewModel> ListByCategoryAndDateTimeCreatingAndUser(string param_category, DateTime date_time_creating, string param_user);
		public ICollection<ListViewModel> ListByCategoryAndDateTimeUpdatingAndUser(string param_category, DateTime date_time_updating, string param_user);
		public ListViewModel Update(int id, ListDTO list_dto);
		public ListViewModel Patch(int id_list, ListPatchDTO list_patch_dto);
		public ListViewModel AddProduct(ListProductDTO list_product_dto);
		public ListViewModel RemoveProduct(int id_list, int id_product);
	}
}