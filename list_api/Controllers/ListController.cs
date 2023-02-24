using list_api.Models;
using list_api.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
namespace list_api.Controllers {
	[ApiController]
	[Route("api[controller]")]
	public class ListController : ControllerBase {
		private readonly IListRepository list_repository;
		private List? list { get; set; }
		public ListController(IListRepository list_repository) { // Constructing.
			this.list_repository = list_repository;
		}
		[HttpGet]
		public IActionResult Create([FromBody] List list) { // Responding with a created list after creating.
			if (ModelState.IsValid) {
				this.list = list_repository.Create(list);
				return Created("api/List/" + list.ID, list);
			} else return BadRequest(ModelState);
		}
		[HttpDelete("id:int")]
		public IActionResult Delete(int id) { // Responding with no content after deleting.
			if (ModelState.IsValid) {
				list = list_repository.Delete(id);
				if (list != null) return NoContent();
				else return NotFound();
			} else return BadRequest(ModelState);
		}
		[HttpGet("id:int")]
		public IActionResult Get(int id) { // Responding with a list after getting.
			if (ModelState.IsValid) {
				list = list_repository.Get(id);
				if (list != null) return Ok(list);
				else return NotFound();
			} else return BadRequest(ModelState);
		}
		[HttpGet]
		public IActionResult List() { // Responding with list list after getting.
			return Ok(list_repository.List());
		}
		[HttpPut]
		public IActionResult Update(int id, [FromBody] List list) { // Responding with an updated list after updating.
			if (ModelState.IsValid) {
				this.list = list_repository.Update(id, list);
				if (this.list != null) return Ok(list);
				else return NotFound();
			} else return BadRequest(ModelState);
		}
	}
}