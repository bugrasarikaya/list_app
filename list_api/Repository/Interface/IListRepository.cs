using list_api.Models;
using Microsoft.AspNetCore.Mvc;
namespace list_api.Repository.Interface {
	public interface IListRepository {
		public List Create(List list);
		public List? Delete(int id);
		public ListViewModel Get(int id);
		public ICollection<ListViewModel> List();
		public ICollection<ListViewModel>? ListByCategory(int id_category);
		//public ICollection<ListViewModel>? ListByNameCategory(string name_category);
		public ICollection<ListViewModel>? ListByUser(int id_user);
		//public ICollection<ListViewModel>? ListByNameUser(string name_user);
		public ICollection<ListViewModel>? ListByCategoryAndUser(int id_category, int id_user);
		//public ICollection<ListViewModel>? ListByNameCategoryAndNameUser(string name_category, string name_user);
		public List Update(int id, List list);
		public List SetCompleted(int id_list);
		public ListViewModel Add(ListProduct list_product);
		public ListViewModel? Remove(int id_list, int id_product);
		public List Clear(int id_list);
		public T Supply<T>(int id);
	}
}