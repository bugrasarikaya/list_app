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
	public class RoleController : ControllerBase {
		private readonly IRoleRepository role_repository;
		public RoleController(IRoleRepository role_repository) { // Constructing.
			this.role_repository = role_repository;
		}
		[HttpPost]
		public IActionResult Create([FromBody] RoleDTO role_dto) { // Responding with a created role after creating.
			if (ModelState.IsValid) {
				Role role_created = role_repository.Create(role_dto);
				return Created(new Uri(Request.GetEncodedUrl() + "/" + role_created.ID), role_created);
			} else return BadRequest(ModelState.Values.SelectMany(mse => mse.Errors).Select(me => me.ErrorMessage));
		}
		[HttpDelete("{param_role}")]
		public IActionResult Delete(string param_role) { // Responding with no content after deleting.
			Validator param_role_validator = new Validator(param_role);
			if (param_role_validator.Validate()) {
				role_repository.Delete(param_role);
				return NoContent();
			} else return BadRequest(param_role_validator.ListMessage);
		}
		[HttpGet("{param_role}")]
		public IActionResult Get(string param_role) { // Responding with a role after getting.
			Validator param_role_validator = new Validator(param_role);
			if (param_role_validator.Validate()) return Ok(role_repository.Get(param_role));
			else return BadRequest(param_role_validator.ListMessage);
		}
		[HttpGet]
		public IActionResult List() { // Responding with role list after getting.
			return Ok(role_repository.List());
		}
		[HttpPut("{param_role}")]
		public IActionResult Update(string param_role, [FromBody] RoleDTO role_dto) { // Responding with an updated role after updating.
			Validator param_role_validator = new Validator(param_role);
			if (param_role_validator.Validate() || ModelState.IsValid) return Ok(role_repository.Update(param_role, role_dto));
			else return BadRequest(param_role_validator.ListMessage.Concat(ModelState.Values.SelectMany(mse => mse.Errors).Select(me => me.ErrorMessage)));
		}
		[HttpPatch("{param_role}")]
		public IActionResult Patch(string param_role, [FromBody] RolePatchDTO role_patch_dto) { // Responding with a patched role after patching.
			Validator param_role_validator = new Validator(param_role);
			if (param_role_validator.Validate() || ModelState.IsValid) return Ok(role_repository.Patch(param_role, role_patch_dto));
			else return BadRequest(param_role_validator.ListMessage.Concat(ModelState.Values.SelectMany(mse => mse.Errors).Select(me => me.ErrorMessage)));
		}
	}
}