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
	[Route("api[controller]")]
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
		[HttpDelete("{id:int}")]
		public IActionResult Delete(int id) { // Responding with no content after deleting.
			Validator id_validator = new Validator(id);
			if (id_validator.IDValidator()) {
				user_repository.Delete(id);
				return NoContent();
			} else return BadRequest(id_validator.ListMessage);
		}
		[HttpGet("{id:int}")]
		public IActionResult Get(int id) { // Responding with a user after getting.
			Validator id_validator = new Validator(id);
			if (id_validator.IDValidator()) return Ok(user_repository.Get(id));
			else return BadRequest(id_validator.ListMessage);
		}
		[HttpGet]
		public IActionResult List() { // Responding with user list after getting.
			return Ok(user_repository.List());
		}
		[HttpPut("{id:int}")]
		public IActionResult Update(int id, [FromBody] UserDTO user_dto) { // Responding with a user after updating.
			Validator id_validator = new Validator(id);
			if (id_validator.IDValidator() || ModelState.IsValid) return Ok(user_repository.Update(id, user_dto));
			else return BadRequest(id_validator.ListMessage.Concat(ModelState.Values.SelectMany(mse => mse.Errors).Select(me => me.ErrorMessage)));
		}
		[HttpPatch("{id:int}")]
		public IActionResult Patch(int id, [FromBody] UserPatchDTO user_patch_dto) { // Responding with a patched user after patching.
			Validator id_validator = new Validator(id);
			if (id_validator.IDValidator() || ModelState.IsValid) return Ok(user_repository.Patch(id, user_patch_dto));
			else return BadRequest(id_validator.ListMessage.Concat(ModelState.Values.SelectMany(mse => mse.Errors).Select(me => me.ErrorMessage)));
		}
	}
}