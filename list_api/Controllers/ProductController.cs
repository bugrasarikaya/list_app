using list_api.Models;
using list_api.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
namespace list_api.Controllers {
	[ApiController]
	[Route("api[controller]")]
	public class ProductController : ControllerBase {
		private readonly IProductRepository product_repository;
		private Product? product { get; set; }
		public ProductController(IProductRepository product_repository) { // Constructing.
			this.product_repository = product_repository;
		}
		[HttpGet]
		public IActionResult Create([FromBody] Product product) { // Responding with a created product after creating.
			if (ModelState.IsValid) {
				this.product = product_repository.Create(product);
				return Created("api/Product/" + product.ID, product);
			} else return BadRequest(ModelState);
		}
		[HttpDelete("id:int")]
		public IActionResult Delete(int id) { // Responding with no content after deleting.
			if (ModelState.IsValid) {
				product = product_repository.Delete(id);
				if (product != null) return NoContent();
				else return NotFound();
			} else return BadRequest(ModelState);
		}
		[HttpGet("id:int")]
		public IActionResult Get(int id) { // Responding with a product after getting.
			if (ModelState.IsValid) {
				product = product_repository.Get(id);
				if (product != null) return Ok(product);
				else return NotFound();
			} else return BadRequest(ModelState);
		}
		[HttpGet]
		public IActionResult List() { // Responding with product list after getting.
			return Ok(product_repository.List());
		}
		[HttpPut]
		public IActionResult Update(int id, [FromBody] Product product) { // Responding with an updated product after updating.
			if (ModelState.IsValid) {
				this.product = product_repository.Update(id, product);
				if (this.product != null) return Ok(product);
				else return NotFound();
			} else return BadRequest(ModelState);
		}
	}
}