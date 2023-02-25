using list_api.Models;
namespace list_api.Repository.Interface {
	public interface IClientRepository {
		public int IDUser { get; set; }
		public List CreateList(List list);
		public List? Deletelist(int id_list);
		public List? GetList(int id_list);
		public ICollection<List> ListLists();
		public List? UpdateList(int id_list, List list);
		public ICollection<Product>? ListListProducts(int id_list);
		public ListProduct AddProduct(ListProduct list_product);
		public ListProduct? RemoveProduct(int id_list, int id_product);
		public User? GetUser();
		public User? UpdateUser(User user);
		public T Supply<T>(int id);

	}
}