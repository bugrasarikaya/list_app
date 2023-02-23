using list_api.Models.ViewModels;
using list_api.Security.Models;
using Microsoft.AspNetCore.Mvc;
namespace list_api.Repository.Interface {
	public interface ITokenRepository {
		public ActionResult<Token>? Create(UserViewModel user_view_model);
		public ActionResult<Token>? Refresh(string refresh_token);
		//public Token Refresh();
	}
}