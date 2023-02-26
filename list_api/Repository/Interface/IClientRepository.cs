using list_api.Models;
namespace list_api.Repository.Interface {
	public interface IClientRepository {
		public int IDUser { get; set; }
		public List CreateList(List list);
		public List? Deletelist(int id_list);
		public ListViewModel? GetList(int id_list);
		public ICollection<ListViewModel> ListLists();
		public ICollection<ListViewModel> ListListsByCategory(int id_category);
		public List UpdateList(int id_list, List list);
		public List SetCompleted(int id_list);
		//public ICollection<Product>? ListListProducts(int id_list);
		public ListViewModel AddProduct(ListProduct list_product);
		public ListViewModel? RemoveProduct(int id_list, int id_product);
		public List Clear(int id_list);
		public UserViewModel GetUser();
		public UserViewModel UpdateUser(User user);
		public T Supply<T>(int id);

	}
}