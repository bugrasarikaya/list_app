using list_api.Models;
using list_api.Models.ViewModels;
using list_api.Repository.Interface;
using list_api.Security.Models;
using Microsoft.AspNetCore.Mvc;
namespace list_api.Controllers {
	[ApiController]
	[Route("api[controller]")]
	public class TokenController : ControllerBase { // Constructing.
		private readonly ITokenRepository token_repository;
		private Token? token { get; set; }
		public TokenController(ITokenRepository token_repository) {
			this.token_repository = token_repository;
		}
		[HttpPost]
		public IActionResult Create(UserViewModel user_view_model) { // Creating a token.
			if (ModelState.IsValid) {
				token = token_repository.Create(user_view_model);
				if (token != null) return Ok(token);
				else return NoContent();
			}
			return BadRequest(ModelState);
		}
		[HttpGet]
		public IActionResult Refresh(string refresh_token) { // Refreshing a token.
			if (ModelState.IsValid) {
				token = token_repository.Refresh(refresh_token);
				if (token != null) return Ok(token);
				else return NoContent();
			}
			return BadRequest(ModelState);
		}
	}
}