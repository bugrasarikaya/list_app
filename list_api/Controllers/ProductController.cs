using list_api.Models;
using list_api.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace list_api.Controllers {
	[ApiController]
	[Authorize(Roles = "Admin")]
	[Route("api[controller]")]
	public class ProductController : ControllerBase {
		private readonly IProductRepository product_repository;
		public ProductController(IProductRepository product_repository) { // Constructing.
			this.product_repository = product_repository;
		}
		[HttpPost]
		public IActionResult Create([FromBody] Product product) { // Responding with a created product after creating.
			if (ModelState.IsValid) {
				Product? product_created = product_repository.Create(product);
				return Created("api/Product/" + product_created.ID, product_created);
			} else return BadRequest(ModelState);
		}
		[HttpDelete("{id:int}")]
		public IActionResult Delete(int id) { // Responding with no content after deleting.
			if (ModelState.IsValid) {
				Product? product_deleted = product_repository.Delete(id);
				if (product_deleted != null) return NoContent();
				else return NotFound();
			} else return BadRequest(ModelState);
		}
		[Authorize(Roles = "Admin, User")]
		[HttpGet("{id:int}")]
		public IActionResult Get(int id) { // Responding with a product after getting.
			if (ModelState.IsValid) {
				Product? product = product_repository.Get(id);
				if (product != null) return Ok(product);
				else return NotFound();
			} else return BadRequest(ModelState);
		}
		[Authorize(Roles = "Admin, User")]
		[HttpGet]
		public IActionResult List() { // Responding with product list after getting.
			return Ok(product_repository.List());
		}
		[Authorize(Roles = "Admin, User")]
		[HttpGet]
		public IActionResult List(int id_category) { // Listing all products which have a specific category.
			return Ok(product_repository.List(id_category));
		}
		[HttpPut("{id:int}")]
		public IActionResult Update(int id, [FromBody] Product product) { // Responding with an updated product after updating.
			if (ModelState.IsValid) {
				Product? product_updated = product_repository.Update(id, product);
				if (product_updated != null) return Ok(product_updated);
				else return NotFound();
			} else return BadRequest(ModelState);
		}
	}
}