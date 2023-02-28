using list_api.Models;
using list_api.Models.DTOs;
namespace list_api.Repository.Interface {
	public interface IListRepository {
		public List Create(ListDTO list_dto);
		public List? Delete(int id);
		public ListViewModel Get(int id);
		public ICollection<ListViewModel> List();
		public ICollection<ListViewModel> ListByCategory(int id_category);
		public ICollection<ListViewModel> ListByUser(int id_user);
		public ICollection<ListViewModel> ListByCategoryAndUser(int id_category, int id_user);
		public List Update(int id, ListDTO list_dto);
		public List Patch(int id_list, ListPatchDTO list_patch_dto);
		public ListViewModel Add(ListProductDTO list_product_dto);
		public ListViewModel? Remove(int id_list, int id_product);
		public List Clear(int id_list);
	}
}