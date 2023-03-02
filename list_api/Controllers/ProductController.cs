using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using list_api.Common;
using list_api.Models;
using list_api.Models.DTOs;
using list_api.Repository.Interface;
namespace list_api.Controllers {
	[ApiController]
	[Authorize(Roles = "Admin")]
	[Route("api/[controller]")]
	public class ProductController : ControllerBase {
		private readonly IProductRepository product_repository;
		public ProductController(IProductRepository product_repository) { // Constructing.
			this.product_repository = product_repository;
		}
		[HttpPost]
		public IActionResult Create([FromBody] ProductDTO product_dto) { // Responding with a created product after creating.
			if (ModelState.IsValid) {
				Product product_created = product_repository.Create(product_dto);
				return Created(new Uri(Request.GetEncodedUrl() + "/" + product_created.ID), product_created);
			} else return BadRequest(ModelState.Values.SelectMany(mse => mse.Errors).Select(me => me.ErrorMessage));
		}
		[HttpDelete("{id:int}")]
		public IActionResult Delete(int id) { // Responding with no content after deleting.
			Validator id_validator = new Validator(id);
			if (id_validator.Validate()) {
				product_repository.Delete(id);
				return NoContent();
			} else return BadRequest(id_validator.ListMessage);
		}
		[Authorize(Roles = "Admin, User")]
		[HttpGet("{id:int}")]
		public IActionResult Get(int id) { // Responding with a product after getting.
			Validator id_validator = new Validator(id);
			if (id_validator.Validate()) return Ok(product_repository.Get(id));
			else return BadRequest(id_validator.ListMessage);
		}
		[Authorize(Roles = "Admin, User")]
		[HttpGet]
		public IActionResult List() { // Responding with product list.
			return Ok(product_repository.List());
		}
		[Authorize(Roles = "Admin, User")]
		[HttpGet("{param_category}")]
		public IActionResult List(string param_category) { // Listing all products which have a specific category.
			Validator param_category_validator = new Validator(param_category);
			if (param_category_validator.Validate()) return Ok(product_repository.List(param_category));
			else return BadRequest(param_category_validator.ListMessage);
		}
		[HttpPut("{id:int}")]
		public IActionResult Update(int id, [FromBody] ProductDTO product_dto) { // Responding with an updated product after updating.
			Validator id_validator = new Validator(id);
			if (id_validator.Validate() || ModelState.IsValid) return Ok(product_repository.Update(id, product_dto));
			else return BadRequest(id_validator.ListMessage.Concat(ModelState.Values.SelectMany(mse => mse.Errors).Select(me => me.ErrorMessage)));
		}
		[HttpPatch("{id:int}")]
		public IActionResult Patch(int id, [FromBody] ProductPatchDTO product_patch_dto) { // Responding with a patched product after patching.
			Validator id_validator = new Validator(id);
			if (id_validator.Validate() || ModelState.IsValid) return Ok(product_repository.Patch(id, product_patch_dto));
			else return BadRequest(id_validator.ListMessage.Concat(ModelState.Values.SelectMany(mse => mse.Errors).Select(me => me.ErrorMessage)));
		}
	}
}