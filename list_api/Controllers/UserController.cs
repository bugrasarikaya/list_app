using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using list_api.Common;
using list_api.Models;
using list_api.Repository.Interface;
using list_api.Models.DTOs;
namespace list_api.Controllers {
	[ApiController]
	[Authorize(Roles = "Admin")]
	[Route("api/[controller]")]
	public class UserController : ControllerBase {
		private readonly IUserRepository user_repository;
		public UserController(IUserRepository user_repository) { // Constructing.
			this.user_repository = user_repository;
		}
		[HttpPost]
		public IActionResult Create([FromBody] UserDTO user_dto) { // Responding with a created user after creating.
			if (ModelState.IsValid) {
				User user_created = user_repository.Create(user_dto);
				return Created(new Uri(Request.GetEncodedUrl() + "/" + user_created.ID), user_created);
			} else return BadRequest(ModelState.Values.SelectMany(mse => mse.Errors).Select(me => me.ErrorMessage));
		}
		[HttpDelete("{param_user}")]
		public IActionResult Delete(string param_user) { // Responding with no content after deleting.
			Validator param_user_validator = new Validator(param_user);
			if (param_user_validator.Validate()) {
				user_repository.Delete(param_user);
				return NoContent();
			} else return BadRequest(param_user_validator.ListMessage);
		}
		[HttpGet("{param_user}")]
		public IActionResult Get(string param_user) { // Responding with a user after getting.
			Validator param_user_validator = new Validator(param_user);
			if (param_user_validator.Validate()) return Ok(user_repository.Get(param_user));
			else return BadRequest(param_user_validator.ListMessage);
		}
		[HttpGet]
		public IActionResult List() { // Responding with user list after getting.
			return Ok(user_repository.List());
		}
		[HttpPut("{param_user}")]
		public IActionResult Update(string param_user, [FromBody] UserDTO user_dto) { // Responding with a user after updating.
			Validator param_user_validator = new Validator(param_user);
			if (param_user_validator.Validate() || ModelState.IsValid) return Ok(user_repository.Update(param_user, user_dto));
			else return BadRequest(param_user_validator.ListMessage.Concat(ModelState.Values.SelectMany(mse => mse.Errors).Select(me => me.ErrorMessage)));
		}
		[HttpPatch("{param_user}")]
		public IActionResult Patch(string param_user, [FromBody] UserPatchDTO user_patch_dto) { // Responding with a patched user after patching.
			Validator param_user_validator = new Validator(param_user);
			if (param_user_validator.Validate() || ModelState.IsValid) return Ok(user_repository.Patch(param_user, user_patch_dto));
			else return BadRequest(param_user_validator.ListMessage.Concat(ModelState.Values.SelectMany(mse => mse.Errors).Select(me => me.ErrorMessage)));
		}
	}
}