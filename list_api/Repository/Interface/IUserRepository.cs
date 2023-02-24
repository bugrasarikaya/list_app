using list_api.Models;
namespace list_api.Repository.Interface {
	public interface IUserRepository {
		public User Create(User user);
		public User? Delete(int id);
		public User? Get(int id);
		public ICollection<User> List();
		public User? Update(int id, User user);
	}
}