using list_api.Data;
using list_api.Exceptions;
using list_api.Models;
using list_api.Models.DTOs;
namespace list_api.Security.Common {
	public static class SecurityCheck {
		public static User User(IListApiDbContext context, UserAuthDTO user_auth_dto) { // Checking a user record by ID and password.
			User? user = context.Users.SingleOrDefault(u => u.Name == user_auth_dto.Name && u.Password == user_auth_dto.Password);
			if (user != null) return user;
			else throw new NotFoundException("Invalid username or password.");
		}
		public static User RefreshToken(IListApiDbContext context, string refresh_token) { // Checking a user record by ID and password.
			User? user = context.Users.SingleOrDefault(u => u.RefreshToken == refresh_token && u.RefreshTokenExpireDate > DateTime.Now);
			if (user != null) return user;
			else throw new NotFoundException("Invalid refresh token.");
		}
	}
}