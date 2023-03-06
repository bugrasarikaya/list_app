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
	public class BrandController : ControllerBase {
		private readonly IBrandRepository brand_repository;
		public BrandController(IBrandRepository brand_repository) { // Constructing.
			this.brand_repository = brand_repository;
		}
		[HttpPost]
		public IActionResult Create([FromBody] BrandDTO brand_dto) { // Responding with a created brand after creating.
			ValidationResult dto_validation_result = new BrandDTOValidator().Validate(brand_dto);
			if (dto_validation_result.IsValid) {
				BrandViewModel brand_view_model_created = brand_repository.Create(brand_dto);
				return Created(new Uri(Request.GetEncodedUrl() + "/" + brand_view_model_created.ID), brand_view_model_created);
			} else return BadRequest(dto_validation_result.Errors.Select(e => e.ErrorMessage));
		}
		[HttpDelete("{param_brand}")]
		public IActionResult Delete(string param_brand) { // Responding with no content after deleting.
			ParamValidator param_brand_validator = new ParamValidator(param_brand);
			if (param_brand_validator.Validate()) {
				if (brand_repository.Delete(param_brand) == null) return NoContent();
				else return NotFound();
			} else return BadRequest(param_brand_validator.ListMessage);
		}
		[HttpGet("{param_brand}")]
		public IActionResult Get(string param_brand) { // Responding with a brand after getting.
			ParamValidator param_brand_validator = new ParamValidator(param_brand);
			if (param_brand_validator.Validate()) return Ok(brand_repository.Get(param_brand));
			else return BadRequest(param_brand_validator.ListMessage);
		}
		[HttpGet]
		public IActionResult List() { // Responding with brand list after getting.
			return Ok(brand_repository.List());
		}
		[HttpPut("{param_brand}")]
		public IActionResult Update(string param_brand, [FromBody] BrandDTO brand_dto) { // Responding with an updated brand after updating.
			ParamValidator param_brand_validator = new ParamValidator(param_brand);
			ValidationResult dto_validation_result = new BrandDTOValidator().Validate(brand_dto);
			if (param_brand_validator.Validate() || ModelState.IsValid) return Ok(brand_repository.Update(param_brand, brand_dto));
			else return BadRequest(param_brand_validator.ListMessage.Concat(dto_validation_result.Errors.Select(me => me.ErrorMessage)));
		}
		[HttpPatch("{param_brand}")]
		public IActionResult Patch(string param_brand, [FromBody] BrandPatchDTO brand_patch_dto) { // Responding with a patched brand after patching.
			ParamValidator param_brand_validator = new ParamValidator(param_brand);
			ValidationResult dto_validation_result = new BrandPatchDTOValidator().Validate(brand_patch_dto);
			if (param_brand_validator.Validate() || dto_validation_result.IsValid) return Ok(brand_repository.Patch(param_brand, brand_patch_dto));
			else return BadRequest(param_brand_validator.ListMessage.Concat(dto_validation_result.Errors.Select(me => me.ErrorMessage)));
		}
	}
}