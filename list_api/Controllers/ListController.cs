using list_api.Models;
using list_api.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace list_api.Controllers {
	[ApiController]
	[Authorize(Roles = "Admin")]
	[Route("api[controller]")]
	public class ListController : ControllerBase {
		private readonly IListRepository list_repository;
		public ListController(IListRepository list_repository) { // Constructing.
			this.list_repository = list_repository;
		}
		[HttpPost]
		public IActionResult Create([FromBody] List list) { // Responding with a created list after creating.
			if (ModelState.IsValid) {
				List? list_created = list_repository.Create(list);
				return Created("api/List/" + list_created.ID, list_created);
			} else return BadRequest(ModelState);
		}
		[HttpDelete("{id:int}")]
		public IActionResult Delete(int id) { // Responding with no content after deleting.
			if (ModelState.IsValid) {
				List? list_deleted = list_repository.Delete(id);
				if (list_deleted != null) return NoContent();
				else return NotFound();
			} else return BadRequest(ModelState);
		}
		[HttpGet("{id:int}")]
		public IActionResult Get(int id) { // Responding with a list after getting.
			if (ModelState.IsValid) {
				List? list = list_repository.Get(id);
				if (list != null) return Ok(list);
				else return NotFound();
			} else return BadRequest(ModelState);
		}
		[HttpGet]
		public IActionResult List() { // Responding with list list after getting.
			return Ok(list_repository.List());
		}
		[HttpPut("{id:int}")]
		public IActionResult Update(int id, [FromBody] List list) { // Responding with an updated list after updating.
			if (ModelState.IsValid) {
				List? list_updated = list_repository.Update(id, list);
				if (list_updated != null) return Ok(list_updated);
				else return NotFound();
			} else return BadRequest(ModelState);
		}
	}
}