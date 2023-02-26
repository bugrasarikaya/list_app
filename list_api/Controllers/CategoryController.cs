using list_api.Models;
using list_api.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using list_api.Common;
namespace list_api.Controllers {
	[ApiController]
	[Authorize(Roles = "Admin")]
	[Route("api[controller]")]
	public class CategoryController : ControllerBase {
		private readonly ICategoryRepository category_repository;
		public CategoryController(ICategoryRepository category_repository) { // Constructing.
			this.category_repository = category_repository;
		}
		[HttpPost]
		public IActionResult Create([FromBody] Category category) { // Responding with a created category after creating.
			if (ModelState.IsValid) {
				Category? category_created = category_repository.Create(category);
				return Created("api/Category/" + category_created.ID, category_created);
			} else {
				return BadRequest(string.Join(" ", ModelState.Values.SelectMany(mse => mse.Errors).Select(me => me.ErrorMessage)));
			}
		}
		[HttpDelete("{id:int}")]
		public IActionResult Delete(int id) { // Responding with no content after deleting.
			Validator id_validator = new Validator(id);
			if (id_validator.IDValidator()) {
				Category? category_deleted = category_repository.Delete(id);
				if (category_deleted != null) return NoContent();
				else return NotFound();
			} else return BadRequest(string.Join(" ", id_validator.ListMessage));
		}
		[HttpGet("{id:int}")]
		public IActionResult Get(int id) { // Responding with a category after getting.
			Validator id_validator = new Validator(id);
			if (id_validator.IDValidator()) {
				Category? category = category_repository.Get(id);
				if (category != null) return Ok(category);
				else return NotFound();
			} else return BadRequest(string.Join(" ", id_validator.ListMessage));
		}
		[HttpGet]
		public IActionResult List() { // Responding with category list after getting.
			return Ok(category_repository.List());
		}
		[HttpPut("{id:int}")]
		public IActionResult Update(int id, [FromBody] Category category) { // Responding with an updated category after updating.
			Validator id_validator = new Validator(id);
			if (id_validator.IDValidator() || ModelState.IsValid) {
				Category? category_updated = category_repository.Update(id, category);
				if (category_updated != null) return Ok(category_updated);
				else return NotFound();
			} else return BadRequest(string.Join(" ", id_validator.ListMessage) + " " + string.Join(" ", ModelState.Values.SelectMany(mse => mse.Errors).Select(me => me.ErrorMessage)));
		}
	}
}