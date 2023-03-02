using list_api.Models;
using list_api.Models.DTOs;
using list_api.Models.ViewModels;
namespace list_api.Repository.Interface {
	public interface IUserRepository {
		public User Create(UserDTO user_dto);
		public void Delete(string param_user);
		public UserViewModel Get(string param_user);
		public ICollection<UserViewModel> List();
		public UserViewModel Update(string param_user, UserDTO user_dto);
		public UserViewModel Patch(string param_user, UserPatchDTO user_patch_dto);
	}
}