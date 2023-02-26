using list_api.Models;
using list_api.Repository.Interface;
using list_api.Security.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using list_api.Security;
namespace list_api.Controllers {
	[ApiController]
	[AllowAnonymous]
	[Route("api[controller]")]
	public class TokenController : ControllerBase { // Constructing.
		private readonly ITokenRepository token_repository;
		public TokenController(ITokenRepository token_repository) {
			this.token_repository = token_repository;
		}
		[HttpPost]
		public IActionResult Create([FromBody] User user) { // Creating a token for login.
			if (ModelState.IsValid) {
				Token? token = token_repository.Create(user);
				if (token != null) return Ok(token);
				else return NoContent();
			}
			return BadRequest(string.Join(" ", ModelState.Values.SelectMany(mse => mse.Errors).Select(me => me.ErrorMessage)));
		}
		[HttpGet("{refresh_token}")]
		public IActionResult Refresh(string refresh_token) { // Refreshing a token.
			SecurityValidator refresh_token_validator = new SecurityValidator(refresh_token);
			if (refresh_token_validator.RefreshTokenValidator()) {
				Token? token = token_repository.Refresh(refresh_token);
				if (token != null) return Ok(token);
				else return NoContent();
			}
			return BadRequest(string.Join(" ", refresh_token_validator.ListMessage));
		}
	}
}