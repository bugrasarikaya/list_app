using Microsoft.Extensions.Caching.Distributed;
using list_api.Data;
using list_api.Models;
using list_api.Models.DTOs;
using list_api.Repository.Common;
using list_api.Repository.Interface;
using list_api.Security;
using list_api.Security.Common;
namespace list_api.Repository {
	public class AuthRepository : IAuthRepository {
		private readonly IDistributedCache cache;
		private readonly IConfiguration configuration;
		private readonly IListApiDbContext context;
		public AuthRepository(IDistributedCache cache, IConfiguration configuration, IListApiDbContext context) { // Constructing.
			this.cache = cache;
			this.configuration = configuration;
			this.context = context;
		}
		public void Register(UserAuthDTO user_auth_dto) { // Creating a user.
			context.Users.Add(new User() { IDRole = Check.ID<Role>(cache, context, 2), Name = Check.NameForConflict<User>(cache, context, user_auth_dto.Name), Password = user_auth_dto.Password });
			context.SaveChanges();
		}
		public Token LogIn(UserAuthDTO user_auth_dto) { // Creating a token for login.
			User user = SecurityCheck.User(context, user_auth_dto);
			Token token = new TokenHandler(configuration).CreateAccsessToken(new UserTokenDTO() { ID = user.ID, NameRole = Supply.ByID<Role>(cache, context, user.IDRole).Name });
			user.RefreshToken = token.RefreshToken;
			user.RefreshTokenExpireDate = token.Expiration.AddMinutes(30);
			context.SaveChanges();
			return token;
		}
		public Token Refresh(string refresh_token) { // Refreshing a token.
			User user = SecurityCheck.RefreshToken(context, refresh_token);
			Token? token = new TokenHandler(configuration).CreateAccsessToken(new UserTokenDTO() { ID = user.ID, NameRole = Supply.ByID<Role>(cache, context, user.IDRole).Name });
			user.RefreshToken = token.RefreshToken;
			user.RefreshTokenExpireDate = token.Expiration.AddMinutes(30);
			context.SaveChanges();
			return token;
		}
	}
}