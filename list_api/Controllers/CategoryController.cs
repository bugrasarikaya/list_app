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
	public class CategoryController : ControllerBase {
		private readonly ICategoryRepository category_repository;
		public CategoryController(ICategoryRepository category_repository) { // Constructing.
			this.category_repository = category_repository;
		}
		[HttpPost]
		public IActionResult Create([FromBody] CategoryDTO category_dto) { // Responding with a created category after creating.
			if (ModelState.IsValid) {
				Category category_created = category_repository.Create(category_dto);
				return Created(new Uri(Request.GetEncodedUrl() + "/" + category_created.ID), category_created);
			} else return BadRequest(ModelState.Values.SelectMany(mse => mse.Errors).Select(me => me.ErrorMessage));
		}
		[HttpDelete("{param_category}")]
		public IActionResult Delete(string param_category) { // Responding with no content after deleting.
			Validator param_category_validator = new Validator(param_category);
			if (param_category_validator.Validate()) {
				category_repository.Delete(param_category);
				return NoContent();
			} else return BadRequest(param_category_validator.ListMessage);
		}
		[HttpGet("{param_category}")]
		public IActionResult Get(string param_category) { // Responding with a category after getting.
			Validator param_category_validator = new Validator(param_category);
			if (param_category_validator.Validate()) return Ok(category_repository.Get(param_category));
			else return BadRequest(param_category_validator.ListMessage);
		}
		[HttpGet]
		public IActionResult List() { // Responding with category list after getting.
			return Ok(category_repository.List());
		}
		[HttpPut("{param_category}")]
		public IActionResult Update(string param_category, [FromBody] CategoryDTO category_dto) { // Responding with an updated category after updating.
			Validator param_category_validator = new Validator(param_category);
			if (param_category_validator.Validate() || ModelState.IsValid) return Ok(category_repository.Update(param_category, category_dto));
			else return BadRequest(param_category_validator.ListMessage.Concat(ModelState.Values.SelectMany(mse => mse.Errors).Select(me => me.ErrorMessage)));
		}
		[HttpPatch("{param_category}")]
		public IActionResult Patch(string param_category, [FromBody] CategoryPatchDTO category_patch_dto) { // Responding with a patched category after patching.
			Validator param_category_validator = new Validator(param_category);
			if (param_category_validator.Validate() || ModelState.IsValid) return Ok(category_repository.Patch(param_category, category_patch_dto));
			else return BadRequest(param_category_validator.ListMessage.Concat(ModelState.Values.SelectMany(mse => mse.Errors).Select(me => me.ErrorMessage)));
		}
	}
}