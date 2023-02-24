using list_api.Repository.Interface;
using list_api.Models;
using list_api.Data;
using Microsoft.AspNetCore.Mvc;
using list_api.Models.ViewModels;
using list_api.Security;
using list_api.Security.Models;
namespace list_api.Repository {
	public class TokenRepository : ITokenRepository {
		private readonly IConfiguration configuration;
		private readonly IListApiDbContext context;
		private Token? token { get; set; }
		private User? user { get; set; }
		public TokenRepository(IConfiguration configuration, IListApiDbContext context) { // Constructing.
			this.configuration = configuration;
			this.context = context;
		}
		public Token? Create(UserViewModel user_view_model) { // Creating a token for login.
			user = context.Users.SingleOrDefault(u => u.Name == user_view_model.Name && u.Password == user_view_model.Password);
			if (user != null) {
				token = new TokenHandler(configuration).CreateAccsessToken(user);
				user.RefreshToken = token.RefreshToken;
				user.RefreshTokenExpireDate = token.Expiration.AddMinutes(15);
				context.SaveChanges();
				return token;
			} else return null;
		}
		public Token? Refresh(string refresh_token) { // Refreshing a token.
			user = context.Users.SingleOrDefault(u => u.RefreshToken == refresh_token && u.RefreshTokenExpireDate > DateTime.Now);
			if (user != null) {
				token = new TokenHandler(configuration).CreateAccsessToken(user);
				user.RefreshToken = token.RefreshToken;
				user.RefreshTokenExpireDate = token.Expiration.AddMinutes(15);
				context.SaveChanges();
				return token;
			} else return null;
		}
	}
}