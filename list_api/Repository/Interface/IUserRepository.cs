using list_api.Models;
using list_api.Models.ViewModels;
namespace list_api.Repository.Interface {
	public interface IUserRepository {
		public User Create(UserViewModel user_view_model);
		public User? Delete(int id);
		public User? Get(int id);
		public ICollection<User> List();
		public User? Update(int id, UserViewModel user_view_model);
	}
}