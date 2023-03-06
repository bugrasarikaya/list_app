using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using list_api.Common;
using list_api.Models.DTOs;
using list_api.Repository.Interface;
using list_api.Models.Validators;
using list_api.Models.ViewModels;
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
			ValidationResult dto_validation_result = new ProductDTOValidator().Validate(product_dto);
			if (dto_validation_result.IsValid) {
				ProductViewModel product_view_model_created = product_repository.Create(product_dto);
				return Created(new Uri(Request.GetEncodedUrl() + "/" + product_view_model_created.ID), product_view_model_created);
			} else return BadRequest(dto_validation_result.Errors.Select(e => e.ErrorMessage));
		}
		[HttpDelete("{id:int}")]
		public IActionResult Delete(int id) { // Responding with no content after deleting.
			ParamValidator id_validator = new ParamValidator(id);
			if (id_validator.Validate()) {
				if (product_repository.Delete(id) == null) return NoContent();
				else return NotFound();
			} else return BadRequest(id_validator.ListMessage);
		}
		[Authorize(Roles = "Admin, User")]
		[HttpGet("{id:int}")]
		public IActionResult Get(int id) { // Responding with a product after getting.
			ParamValidator id_validator = new ParamValidator(id);
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
			ParamValidator param_category_validator = new ParamValidator(param_category);
			if (param_category_validator.Validate()) return Ok(product_repository.ListByCategory(param_category));
			else return BadRequest(param_category_validator.ListMessage);
		}
		[HttpPut("{id:int}")]
		public IActionResult Update(int id, [FromBody] ProductDTO product_dto) { // Responding with an updated product after updating.
			ParamValidator id_validator = new ParamValidator(id);
			ValidationResult dto_validation_result = new ProductDTOValidator().Validate(product_dto);
			if (id_validator.Validate() || dto_validation_result.IsValid) return Ok(product_repository.Update(id, product_dto));
			else return BadRequest(id_validator.ListMessage.Concat(dto_validation_result.Errors.Select(e => e.ErrorMessage)));
		}
		[HttpPatch("{id:int}")]
		public IActionResult Patch(int id, [FromBody] ProductPatchDTO product_patch_dto) { // Responding with a patched product after patching.
			ParamValidator id_validator = new ParamValidator(id);
			ValidationResult dto_validation_result = new ProductPatchDTOValidator().Validate(product_patch_dto);
			if (id_validator.Validate() || dto_validation_result.IsValid) return Ok(product_repository.Patch(id, product_patch_dto));
			else return BadRequest(id_validator.ListMessage.Concat(dto_validation_result.Errors.Select(e => e.ErrorMessage)));
		}
	}
}