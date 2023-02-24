using list_api.Models;
using list_api.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
namespace list_api.Controllers {
	[ApiController]
	[Route("api[controller]")]
	public class ListProductController : ControllerBase {
		private readonly IListProductRepository list_product_repository;
		public ListProductController(IListProductRepository list_product_repository) { // Constructing.
			this.list_product_repository = list_product_repository;
		}
		[HttpGet]
		public IActionResult Create([FromBody] ListProduct list_product) { // Responding with a created product list after creating.
			if (ModelState.IsValid) {
				ListProduct? list_product_created = list_product_repository.Create(list_product);
				return Created("api/ListProduct/" + list_product_created.ID, list_product_created);
			} else return BadRequest(ModelState);
		}
		[HttpDelete]
		public IActionResult Delete([FromQuery] int id_list, [FromQuery] int id_product) { // Responding with no content after deleting.
			if (ModelState.IsValid) {
				ListProduct? list_product_deleted = list_product_repository.Delete(id_list, id_product);
				if (list_product_deleted != null) return NoContent();
				else return NotFound();
			} else return BadRequest(ModelState);
		}
		[HttpGet]
		public IActionResult Get([FromQuery] int id_list, [FromQuery] int id_product) { // Responding with a list_product after getting.
			if (ModelState.IsValid) {
				ListProduct? list_product = list_product_repository.Get(id_list, id_product);
				if (list_product != null) return Ok(list_product);
				else return NotFound();
			} else return BadRequest(ModelState);
		}
		[HttpGet]
		public IActionResult List() { // Responding with list_product list after getting.
			return Ok(list_product_repository.List());
		}
		[HttpPut]
		public IActionResult Update([FromQuery] int id, [FromBody] ListProduct list_product) { // Responding with an updated list_product after updating.
			if (ModelState.IsValid) {
				ListProduct? list_product_updated = list_product_repository.Update(id, list_product);
				if (list_product_updated != null) return Ok(list_product_updated);
				else return NotFound();
			} else return BadRequest(ModelState);
		}
	}
}