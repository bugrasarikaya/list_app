using list_api.Models;
namespace list_api.Repository.Interface {
	public interface IListRepository {
		public List Create(List list);
		public List? Delete(int id);
		public List? Get(int id);
		public ICollection<List> List();
		public List? Update(int id, List list);
	}
}