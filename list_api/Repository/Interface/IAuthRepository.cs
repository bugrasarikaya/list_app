using list_api.Models.DTOs;
using list_api.Security.Models;
namespace list_api.Repository.Interface {
	public interface IAuthRepository {
		public void Register(UserDTO user_dto);
		public Token? LogIn(UserTokenDTO user_token_dto);
		public Token? Refresh(string refresh_token);
	}
}