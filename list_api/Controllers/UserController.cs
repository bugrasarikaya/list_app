using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using list_api.Common;
using list_api.Repository.Interface;
using list_api.Models.DTOs;
using list_api.Models.ViewModels;
using list_api.Models.Validators;
namespace list_api.Controllers {
	[ApiController]
	[Authorize(Roles = "Admin")]
	[Route("api/[controller]")]
	public class UserController : ControllerBase {
		private readonly IUserRepository user_repository;
		public UserController(IUserRepository user_repository) { // Constructing.
			this.user_repository = user_repository;
		}
		[HttpPost]
		public IActionResult Create([FromBody] UserDTO user_dto) { // Responding with a created user after creating.
			ValidationResult dto_validation_result = new UserDTOValidator().Validate(user_dto);
			if (dto_validation_result.IsValid) {
				UserViewModel user_view_model_created = user_repository.Create(user_dto);
				return Created(new Uri(Request.GetEncodedUrl() + "/" + user_view_model_created.ID), user_view_model_created);
			} else return BadRequest(dto_validation_result.Errors.Select(e => e.ErrorMessage));
		}
		[HttpDelete("{param_user}")]
		public IActionResult Delete(string param_user) { // Responding with no content after deleting.
			ParamValidator param_user_validator = new ParamValidator(param_user);
			if (param_user_validator.Validate()) {
				if (user_repository.Delete(param_user) == null) return NoContent();
				return NotFound();
			} else return BadRequest(param_user_validator.ListMessage);
		}
		[HttpGet("{param_user}")]
		public IActionResult Get(string param_user) { // Responding with a user after getting.
			ParamValidator param_user_validator = new ParamValidator(param_user);
			if (param_user_validator.Validate()) return Ok(user_repository.Get(param_user));
			else return BadRequest(param_user_validator.ListMessage);
		}
		[HttpGet]
		public IActionResult List() { // Responding with user list after getting.
			return Ok(user_repository.List());
		}
		[HttpPut("{param_user}")]
		public IActionResult Update(string param_user, [FromBody] UserDTO user_dto) { // Responding with a user after updating.
			ParamValidator param_user_validator = new ParamValidator(param_user);
			ValidationResult dto_validation_result = new UserDTOValidator().Validate(user_dto);
			if (param_user_validator.Validate() || dto_validation_result.IsValid) return Ok(user_repository.Update(param_user, user_dto));
			else return BadRequest(param_user_validator.ListMessage.Concat(dto_validation_result.Errors.Select(e => e.ErrorMessage)));
		}
		[HttpPatch("{param_user}")]
		public IActionResult Patch(string param_user, [FromBody] UserPatchDTO user_patch_dto) { // Responding with a patched user after patching.
			ParamValidator param_user_validator = new ParamValidator(param_user);
			ValidationResult dto_validation_result = new UserPatchDTOValidator().Validate(user_patch_dto);
			if (param_user_validator.Validate() || dto_validation_result.IsValid) return Ok(user_repository.Patch(param_user, user_patch_dto));
			else return BadRequest(param_user_validator.ListMessage.Concat(dto_validation_result.Errors.Select(e => e.ErrorMessage)));
		}
	}
}