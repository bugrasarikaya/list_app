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
	public class RoleController : ControllerBase {
		private readonly IRoleRepository role_repository;
		public RoleController(IRoleRepository role_repository) { // Constructing.
			this.role_repository = role_repository;
		}
		[HttpPost]
		public IActionResult Create([FromBody] RoleDTO role_dto) { // Responding with a created role after creating.
			ValidationResult dto_validation_result = new RoleDTOValidator().Validate(role_dto);
			if (dto_validation_result.IsValid) {
				RoleViewModel role_view_model_created = role_repository.Create(role_dto);
				return Created(new Uri(Request.GetEncodedUrl() + "/" + role_view_model_created.ID), role_view_model_created);
			} else return BadRequest(dto_validation_result.Errors.Select(e => e.ErrorMessage));
		}
		[HttpDelete("{param_role}")]
		public IActionResult Delete(string param_role) { // Responding with no content after deleting.
			ParamValidator param_role_validator = new ParamValidator(param_role);
			if (param_role_validator.Validate()) {
				if (role_repository.Delete(param_role) == null) return NoContent();
				else return NotFound();
			} else return BadRequest(param_role_validator.ListMessage);
		}
		[HttpGet("{param_role}")]
		public IActionResult Get(string param_role) { // Responding with a role after getting.
			ParamValidator param_role_validator = new ParamValidator(param_role);
			if (param_role_validator.Validate()) return Ok(role_repository.Get(param_role));
			else return BadRequest(param_role_validator.ListMessage);
		}
		[HttpGet]
		public IActionResult List() { // Responding with role list after getting.
			return Ok(role_repository.List());
		}
		[HttpPut("{param_role}")]
		public IActionResult Update(string param_role, [FromBody] RoleDTO role_dto) { // Responding with an updated role after updating.
			ParamValidator param_role_validator = new ParamValidator(param_role);
			ValidationResult dto_validation_result = new RoleDTOValidator().Validate(role_dto);
			if (param_role_validator.Validate() || dto_validation_result.IsValid) return Ok(role_repository.Update(param_role, role_dto));
			else return BadRequest(param_role_validator.ListMessage.Concat(dto_validation_result.Errors.Select(e => e.ErrorMessage)));
		}
		[HttpPatch("{param_role}")]
		public IActionResult Patch(string param_role, [FromBody] RolePatchDTO role_patch_dto) { // Responding with a patched role after patching.
			ParamValidator param_role_validator = new ParamValidator(param_role);
			ValidationResult dto_validation_result = new RolePatchDTOValidator().Validate(role_patch_dto);
			if (param_role_validator.Validate() || dto_validation_result.IsValid) return Ok(role_repository.Patch(param_role, role_patch_dto));
			else return BadRequest(param_role_validator.ListMessage.Concat(dto_validation_result.Errors.Select(e => e.ErrorMessage)));
		}
	}
}