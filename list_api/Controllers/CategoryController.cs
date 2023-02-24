using list_api.Models;
using list_api.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
namespace list_api.Controllers {
	[ApiController]
	[Route("api[controller]")]
	public class CategoryController : ControllerBase {
		private readonly ICategoryRepository category_repository;
		public CategoryController(ICategoryRepository category_repository) { // Constructing.
			this.category_repository = category_repository;
		}
		[HttpGet]
		public IActionResult Create([FromBody] Category category) { // Responding with a created category after creating.
			if (ModelState.IsValid) {
				Category? category_created = category_repository.Create(category);
				return Created("api/Category/" + category_created.ID, category_created);
			} else return BadRequest(ModelState);
		}
		[HttpDelete("id:int")]
		public IActionResult Delete(int id) { // Responding with no content after deleting.
			if (ModelState.IsValid) {
				Category? category_deleted = category_repository.Delete(id);
				if (category_deleted != null) return NoContent();
				else return NotFound();
			} else return BadRequest(ModelState);
		}
		[HttpGet("id:int")]
		public IActionResult Get(int id) { // Responding with a category after getting.
			if (ModelState.IsValid) {
				Category? category = category_repository.Get(id);
				if (category != null) return Ok(category);
				else return NotFound();
			} else return BadRequest(ModelState);
		}
		[HttpGet]
		public IActionResult List() { // Responding with category list after getting.
			return Ok(category_repository.List());
		}
		[HttpPut]
		public IActionResult Update([FromQuery] int id, [FromBody] Category category) { // Responding with an updated category after updating.
			if (ModelState.IsValid) {
				Category? category_updated = category_repository.Update(id, category);
				if (category_updated != null) return Ok(category_updated);
				else return NotFound();
			} else return BadRequest(ModelState);
		}
	}
}