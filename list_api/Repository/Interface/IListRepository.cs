using list_api.Models;
namespace list_api.Repository.Interface {
	public interface IListRepository {
		public List Create();
		public List Delete();
		public List Get();
		public ICollection<List> List();
		public List Update();
	}
}