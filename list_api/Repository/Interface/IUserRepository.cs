using list_api.Models;
using list_api.Models.DTOs;
namespace list_api.Repository.Interface {
	public interface IUserRepository {
		public User Create(UserDTO user_dto);
		public User? Delete(int id);
		public UserViewModel Get(int id);
		public ICollection<UserViewModel> List();
		public User Update(int id, UserDTO user_dto);
		public User Patch(int id, UserPatchDTO user_patch_dto);
	}
}