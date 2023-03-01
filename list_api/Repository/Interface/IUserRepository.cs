using list_api.Models;
using list_api.Models.DTOs;
using list_api.Models.ViewModels;
namespace list_api.Repository.Interface {
	public interface IUserRepository {
		public User Create(UserDTO user_dto);
		public void Delete(int id);
		public UserViewModel Get(int id);
		public ICollection<UserViewModel> List();
		public UserViewModel Update(int id, UserDTO user_dto);
		public UserViewModel Patch(int id, UserPatchDTO user_patch_dto);
	}
}