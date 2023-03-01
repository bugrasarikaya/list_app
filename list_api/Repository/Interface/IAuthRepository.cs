using list_api.Models.DTOs;
using list_api.Security;
namespace list_api.Repository.Interface {
	public interface IAuthRepository {
		public void Register(UserAuthDTO user_auth_dto);
		public Token LogIn(UserAuthDTO user_auth_dto);
		public Token Refresh(string refresh_token);
	}
}