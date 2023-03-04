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
		private readonly IConfiguration configuration;
		private readonly IDistributedCache cache;
		private readonly IEncryptor encryptor;
		private readonly IListApiDbContext context;
		public AuthRepository(IConfiguration configuration, IDistributedCache cache, IEncryptor encryptor, IListApiDbContext context) { // Constructing.
			this.cache = cache;
			this.configuration = configuration;
			this.encryptor = encryptor;
			this.context = context;
		}
		public void Register(UserAuthDTO user_auth_dto) { // Creating a user.
			context.Users.Add(new User() { IDRole = Check.ID<Role>(cache, context, (int)Enumerator.Role.User), Name = Check.NameForConflict<User>(cache, context, user_auth_dto.Name), Password = encryptor.Encrpyt(user_auth_dto.Password) });
			context.SaveChanges();
		}
		public Token LogIn(UserAuthDTO user_auth_dto) { // Creating a token for login.
			User user = SecurityCheck.User(cache, context, encryptor, user_auth_dto);
			Token token = new TokenHandler(configuration).CreateAccsessToken(new UserTokenDTO() { ID = user.ID, NameRole = Supply.ByID<Role>(cache, context, user.IDRole).Name });
			user.RefreshToken = token.RefreshToken;
			user.RefreshTokenExpireDate = token.Expiration.AddMinutes(30);
			context.SaveChanges();
			return token;
		}
		public Token Refresh(string refresh_token) { // Refreshing a token.
			User user = SecurityCheck.RefreshToken(cache, context, refresh_token);
			Token? token = new TokenHandler(configuration).CreateAccsessToken(new UserTokenDTO() { ID = user.ID, NameRole = Supply.ByID<Role>(cache, context, user.IDRole).Name });
			user.RefreshToken = token.RefreshToken;
			user.RefreshTokenExpireDate = token.Expiration.AddMinutes(30);
			context.SaveChanges();
			return token;
		}
	}
}