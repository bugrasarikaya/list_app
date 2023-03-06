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
	public class StatusController : ControllerBase {
		private readonly IStatusRepository status_repository;
		public StatusController(IStatusRepository status_repository) { // Constructing.
			this.status_repository = status_repository;
		}
		[HttpPost]
		public IActionResult Create([FromBody] StatusDTO status_dto) { // Responding with a created status after creating.
			ValidationResult dto_validation_result = new StatusDTOValidator().Validate(status_dto);
			if (dto_validation_result.IsValid) {
				StatusViewModel status_view_model_created = status_repository.Create(status_dto);
				return Created(new Uri(Request.GetEncodedUrl() + "/" + status_view_model_created.ID), status_view_model_created);
			} else return BadRequest(dto_validation_result.Errors.Select(e => e.ErrorMessage));
		}
		[HttpDelete("{param_status}")]
		public IActionResult Delete(string param_status) { // Responding with no content after deleting.
			ParamValidator param_status_validator = new ParamValidator(param_status);
			if (param_status_validator.Validate()) {
				if (status_repository.Delete(param_status) == null) return NoContent();
				return NotFound();
			} else return BadRequest(param_status_validator.ListMessage);
		}
		[HttpGet("{param_status}")]
		public IActionResult Get(string param_status) { // Responding with a status after getting.
			ParamValidator param_status_validator = new ParamValidator(param_status);
			if (param_status_validator.Validate()) return Ok(status_repository.Get(param_status));
			else return BadRequest(param_status_validator.ListMessage);
		}
		[HttpGet]
		public IActionResult List() { // Responding with status list after getting.
			return Ok(status_repository.List());
		}
		[HttpPut("{param_status}")]
		public IActionResult Update(string param_status, [FromBody] StatusDTO status_dto) { // Responding with an updated status after updating.
			ParamValidator param_status_validator = new ParamValidator(param_status);
			ValidationResult dto_validation_result = new StatusDTOValidator().Validate(status_dto);
			if (param_status_validator.Validate() || dto_validation_result.IsValid) return Ok(status_repository.Update(param_status, status_dto));
			else return BadRequest(param_status_validator.ListMessage.Concat(dto_validation_result.Errors.Select(e => e.ErrorMessage)));
		}
		[HttpPatch("{param_status}")]
		public IActionResult Patch(string param_status, [FromBody] StatusPatchDTO status_patch_dto) { // Responding with a patched status after patching.
			ParamValidator param_status_validator = new ParamValidator(param_status);
			ValidationResult dto_validation_result = new StatusPatchDTOValidator().Validate(status_patch_dto);
			if (param_status_validator.Validate() || dto_validation_result.IsValid) return Ok(status_repository.Patch(param_status, status_patch_dto));
			else return BadRequest(param_status_validator.ListMessage.Concat(dto_validation_result.Errors.Select(e => e.ErrorMessage)));
		}
	}
}