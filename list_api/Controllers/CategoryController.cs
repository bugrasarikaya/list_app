using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using list_api.Common;
using list_api.Models.DTOs;
using list_api.Repository.Interface;
using list_api.Models.ViewModels;
using list_api.Models.Validators;
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
			ValidationResult dto_validation_result = new CategoryDTOValidator().Validate(category_dto);
			if (dto_validation_result.IsValid) {
				CategoryViewModel category_view_model_created = category_repository.Create(category_dto);
				return Created(new Uri(Request.GetEncodedUrl() + "/" + category_view_model_created.ID), category_view_model_created);
			} else return BadRequest(dto_validation_result.Errors.Select(e => e.ErrorMessage));
		}
		[HttpDelete("{param_category}")]
		public IActionResult Delete(string param_category) { // Responding with no content after deleting.
			ParamValidator param_category_validator = new ParamValidator(param_category);
			if (param_category_validator.Validate()) {
				if (category_repository.Delete(param_category) == null) return NoContent();
				else return NotFound();
			} else return BadRequest(param_category_validator.ListMessage);
		}
		[HttpGet("{param_category}")]
		public IActionResult Get(string param_category) { // Responding with a category after getting.
			ParamValidator param_category_validator = new ParamValidator(param_category);
			if (param_category_validator.Validate()) return Ok(category_repository.Get(param_category));
			else return BadRequest(param_category_validator.ListMessage);
		}
		[HttpGet]
		public IActionResult List() { // Responding with category list after getting.
			return Ok(category_repository.List());
		}
		[HttpPut("{param_category}")]
		public IActionResult Update(string param_category, [FromBody] CategoryDTO category_dto) { // Responding with an updated category after updating.
			ParamValidator param_category_validator = new ParamValidator(param_category);
			ValidationResult dto_validation_result = new CategoryDTOValidator().Validate(category_dto);
			if (param_category_validator.Validate() || dto_validation_result.IsValid) return Ok(category_repository.Update(param_category, category_dto));
			else return BadRequest(param_category_validator.ListMessage.Concat(dto_validation_result.Errors.Select(me => me.ErrorMessage)));
		}
		[HttpPatch("{param_category}")]
		public IActionResult Patch(string param_category, [FromBody] CategoryPatchDTO category_patch_dto) { // Responding with a patched category after patching.
			ParamValidator param_category_validator = new ParamValidator(param_category);
			ValidationResult dto_validation_result = new CategoryPatchDTOValidator().Validate(category_patch_dto);
			if (param_category_validator.Validate() || dto_validation_result.IsValid) return Ok(category_repository.Patch(param_category, category_patch_dto));
			else return BadRequest(param_category_validator.ListMessage.Concat(dto_validation_result.Errors.Select(me => me.ErrorMessage)));
		}
	}
}