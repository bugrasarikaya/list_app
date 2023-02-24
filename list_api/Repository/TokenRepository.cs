using list_api.Repository.Interface;
using list_api.Models;
using list_api.Data;
using list_api.Security;
using list_api.Security.Models;
namespace list_api.Repository {
	public class TokenRepository : ITokenRepository {
		private readonly IConfiguration configuration;
		private readonly IListApiDbContext context;
		public TokenRepository(IConfiguration configuration, IListApiDbContext context) { // Constructing.
			this.configuration = configuration;
			this.context = context;
		}
		public Token? Create(User user) { // Creating a token for login.
			User? user_result = context.Users.SingleOrDefault(u => u.Name == user.Name && u.Password == user.Password);
			if (user_result != null) {
				Token? token = new TokenHandler(configuration).CreateAccsessToken(user_result);
				user_result.RefreshToken = token.RefreshToken;
				user_result.RefreshTokenExpireDate = token.Expiration.AddMinutes(15);
				context.SaveChanges();
				return token;
			} else return null;
		}
		public Token? Refresh(string refresh_token) { // Refreshing a token.
			User? user_result = context.Users.SingleOrDefault(u => u.RefreshToken == refresh_token && u.RefreshTokenExpireDate > DateTime.Now);
			if (user_result != null) {
				Token? token = new TokenHandler(configuration).CreateAccsessToken(user_result);
				user_result.RefreshToken = token.RefreshToken;
				user_result.RefreshTokenExpireDate = token.Expiration.AddMinutes(15);
				context.SaveChanges();
				return token;
			} else return null;
		}
	}
}