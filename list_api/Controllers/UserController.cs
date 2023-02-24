using list_api.Models;
using list_api.Models.ViewModels;
using list_api.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
namespace list_api.Controllers {
	[ApiController]
	[Route("api[controller]")]
	public class UserController : ControllerBase {
		private readonly IUserRepository user_repository;
		private User? user { get; set; }
		public UserController(IUserRepository user_repository) { // Constructing.
			this.user_repository = user_repository;
		}
		[HttpGet]
		public IActionResult Create([FromBody] UserViewModel user_view_model) { // Responding with a created user after creating.
			if (ModelState.IsValid) {
				user = user_repository.Create(user_view_model);
				return Created("api/User/" + user.ID, user);
			} else return BadRequest(ModelState);
		}
		[HttpDelete("id:int")]
		public IActionResult Delete(int id) { // Responding with no content after deleting.
			if (ModelState.IsValid) {
				user = user_repository.Delete(id);
				if (user != null) return NoContent();
				else return NotFound();
			} else return BadRequest(ModelState);
		}
		[HttpGet("id:int")]
		public IActionResult Get(int id) { // Responding with a user after getting.
			if (ModelState.IsValid) {
				user = user_repository.Get(id);
				if (user != null) return Ok(user);
				else return NotFound();
			} else return BadRequest(ModelState);
		}
		[HttpGet]
		public IActionResult List() { // Responding with user list after getting.
			return Ok(user_repository.List());
		}
		[HttpPut]
		public IActionResult Update(int id, [FromBody] UserViewModel user_view_model) { // Responding with an updated user after updating.
			if (ModelState.IsValid) {
				user = user_repository.Update(id, user_view_model);
				if (user != null) return Ok(user);
				else return NotFound();
			} else return BadRequest(ModelState);
		}
	}
}