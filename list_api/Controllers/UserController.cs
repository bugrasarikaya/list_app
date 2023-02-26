using list_api.Common;
using list_api.Models;
using list_api.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
		public IActionResult Create([FromBody] User user) { // Responding with a created user after creating.
			if (ModelState.IsValid) {
				User? user_created = user_repository.Create(user);
				return Created("api/User/" + user_created.ID, user_created);
			} else return BadRequest(string.Join(" ", ModelState.Values.SelectMany(mse => mse.Errors).Select(me => me.ErrorMessage)));
		}
		[HttpDelete("{id:int}")]
		public IActionResult Delete(int id) { // Responding with no content after deleting.
			Validator id_validator = new Validator(id);
			if (id_validator.IDValidator()) {
				User? user_deleted = user_repository.Delete(id);
				if (user_deleted != null) return NoContent();
				else return NotFound();
			} else return BadRequest(string.Join(" ", id_validator.ListMessage));
		}
		[HttpGet("{id:int}")]
		public IActionResult Get(int id) { // Responding with a user after getting.
			Validator id_validator = new Validator(id);
			if (id_validator.IDValidator()) {
				User? user = user_repository.Get(id);
				if (user != null) return Ok(user);
				else return NotFound();
			} else return BadRequest(string.Join(" ", id_validator.ListMessage));
		}
		[HttpGet]
		public IActionResult List() { // Responding with user list after getting.
			return Ok(user_repository.List());
		}
		[HttpPut("{id:int}")]
		public IActionResult Update(int id, [FromBody] User user) { // Responding with a user after updating.
			Validator id_validator = new Validator(id);
			if (id_validator.IDValidator() || ModelState.IsValid) {
				User? user_updated = user_repository.Update(id, user);
				if (user_updated != null) return Ok(user_updated);
				else return NotFound();
			} else return BadRequest(string.Join(" ", id_validator.ListMessage) + " " + string.Join(" ", ModelState.Values.SelectMany(mse => mse.Errors).Select(me => me.ErrorMessage)));
		}
	}
}