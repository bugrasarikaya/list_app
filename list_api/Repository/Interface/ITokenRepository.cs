using list_api.Models;
using list_api.Security.Models;
namespace list_api.Repository.Interface {
	public interface ITokenRepository {
		public Token? Create(User user);
		public Token? Refresh(string refresh_token);
	}
}