using list_api.Data;
using list_api.Models;
using list_api.Models.DTOs;
using list_api.Repository.Common;
using list_api.Repository.Interface;
using list_api.Security;
using list_api.Security.Models;
namespace list_api.Repository {
	public class AuthRepository : IAuthRepository {
		private readonly IConfiguration configuration;
		private readonly IListApiDbContext context;
		public AuthRepository(IConfiguration configuration, IListApiDbContext context) { // Constructing.
			this.configuration = configuration;
			this.context = context;
		}
		public void Register(UserDTO user_dto) { // Creating a user.
			context.Users.Add(new User() { IDRole = Check.ID<Role>(context, 2), Name = Check.NameForConflict<User>(context, user_dto.Name), Password = user_dto.Password });
			context.SaveChanges();
		}
		public Token? LogIn(UserTokenDTO user_token_dto) { // Creating a token for login.
			User? user_result = context.Users.SingleOrDefault(u => u.Name == user_token_dto.Name && u.Password == user_token_dto.Password);
			if (user_result != null) {
				Token? token = new TokenHandler(configuration).CreateAccsessToken(user_result);
				user_result.RefreshToken = token.RefreshToken;
				user_result.RefreshTokenExpireDate = token.Expiration.AddMinutes(30);
				context.SaveChanges();
				return token;
			} else return null;
		}
		public Token? Refresh(string refresh_token) { // Refreshing a token.
			User? user_result = context.Users.SingleOrDefault(u => u.RefreshToken == refresh_token && u.RefreshTokenExpireDate > DateTime.Now);
			if (user_result != null) {
				Token? token = new TokenHandler(configuration).CreateAccsessToken(user_result);
				user_result.RefreshToken = token.RefreshToken;
				user_result.RefreshTokenExpireDate = token.Expiration.AddMinutes(30);
				context.SaveChanges();
				return token;
			} else return null;
		}
	}
}