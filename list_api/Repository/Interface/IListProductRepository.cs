using list_api.Models;
namespace list_api.Repository.Interface {
	public interface IListProductRepository {
		public ListProduct Create(ListProduct list);
		public ListProduct? Delete(int id_list, int id_product);
		public ListProduct? Get(int id_list, int id_product);
		public ICollection<ListProduct> List();
		public ICollection<ListProduct> List(int id_list);
		public ListProduct? Update(int id, ListProduct list_product);
		public T Supply<T>(int id);
	}
}