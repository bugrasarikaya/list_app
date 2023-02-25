using list_api.Models;
using list_api.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace list_api.Controllers {
	[ApiController]
	[Authorize(Roles = "Admin")]
	[Route("api[controller]")]
	public class ListProductController : ControllerBase {
		private readonly IListProductRepository list_product_repository;
		public ListProductController(IListProductRepository list_product_repository) { // Constructing.
			this.list_product_repository = list_product_repository;
		}
		[HttpPost]
		public IActionResult Create([FromBody] ListProduct list_product) { // Responding with a created list product after creating.
			if (ModelState.IsValid) {
				ListProduct? list_product_created = list_product_repository.Create(list_product);
				return Created("api/ListProduct/" + list_product_created.ID, list_product_created);
			} else return BadRequest(ModelState);
		}
		[HttpDelete("{id_list:int}/{id_product:int}")]
		public IActionResult Delete(int id_list, int id_product) { // Responding with no content after deleting.
			if (ModelState.IsValid) {
				ListProduct? list_product_deleted = list_product_repository.Delete(id_list, id_product);
				if (list_product_deleted != null) return NoContent();
				else return NotFound();
			} else return BadRequest(ModelState);
		}
		[HttpGet("{id_list:int}/{id_product:int}")]
		public IActionResult Get(int id_list, int id_product) { // Responding with a list product after getting.
			if (ModelState.IsValid) {
				ListProduct? list_product = list_product_repository.Get(id_list, id_product);
				if (list_product != null) return Ok(list_product);
				else return NotFound();
			} else return BadRequest(ModelState);
		}
		[HttpGet]
		public IActionResult List() { // Responding with list product list after getting.
			return Ok(list_product_repository.List());
		}
		[HttpGet("{id_list:int}")]
		public IActionResult List(int id_list) { // Responding with list product list in a specific list after getting.
			return Ok(list_product_repository.List(id_list));
		}
		[HttpGet("{id_category:int}")]
		public IActionResult ListByCategory(int id_category) { // Responding with list product list by a specific category after getting.
			return Ok(list_product_repository.ListByCategory(id_category));
		}
		[HttpGet("{id_list:int}/{id_category:int}")]
		public IActionResult ListByCategory(int id_list, int id_category) { // Responding with list product list by a specific category after getting.
			return Ok(list_product_repository.ListByCategory(id_list, id_category));
		}
		[HttpPut]
		public IActionResult Update([FromBody] ListProduct list_product) { // Responding with an updated list product after updating.
			if (ModelState.IsValid) {
				ListProduct? list_product_updated = list_product_repository.Update(list_product);
				if (list_product_updated != null) return Ok(list_product_updated);
				else return NotFound();
			} else return BadRequest(ModelState);
		}
	}
}