using list_api.Models;
namespace list_api.Repository.Interface {
	public interface IListProductRepository {
		public ListProduct Create(ListProduct list);
		public ListProduct? Delete(int id_list, int id_product);
		public ListProduct? Get(int id_list, int id_product);
		public ICollection<ListProduct> List();
		public ICollection<ListProduct> List(int id_list);
		public ICollection<ListProduct> ListByCategory(int id_category);
		public ICollection<ListProduct> ListByCategory(int id_list, int id_category);
		public ListProduct? Update(ListProduct list_product);
		public T Supply<T>(int id);
	}
}