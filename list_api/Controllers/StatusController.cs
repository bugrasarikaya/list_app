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
	public class StatusController : ControllerBase {
		private readonly IStatusRepository status_repository;
		public StatusController(IStatusRepository status_repository) { // Constructing.
			this.status_repository = status_repository;
		}
		[HttpPost]
		public IActionResult Create([FromBody] StatusDTO status_dto) { // Responding with a created status after creating.
			if (ModelState.IsValid) {
				Status status_created = status_repository.Create(status_dto);
				return Created(new Uri(Request.GetEncodedUrl() + "/" + status_created.ID), status_created);
			} else return BadRequest(ModelState.Values.SelectMany(mse => mse.Errors).Select(me => me.ErrorMessage));
		}
		[HttpDelete("{param_status}")]
		public IActionResult Delete(string param_status) { // Responding with no content after deleting.
			Validator param_status_validator = new Validator(param_status);
			if (param_status_validator.Validate()) {
				status_repository.Delete(param_status);
				return NoContent();
			} else return BadRequest(param_status_validator.ListMessage);
		}
		[HttpGet("{param_status}")]
		public IActionResult Get(string param_status) { // Responding with a status after getting.
			Validator param_status_validator = new Validator(param_status);
			if (param_status_validator.Validate()) return Ok(status_repository.Get(param_status));
			else return BadRequest(param_status_validator.ListMessage);
		}
		[HttpGet]
		public IActionResult List() { // Responding with status list after getting.
			return Ok(status_repository.List());
		}
		[HttpPut("{param_status}")]
		public IActionResult Update(string param_status, [FromBody] StatusDTO status_dto) { // Responding with an updated status after updating.
			Validator param_status_validator = new Validator(param_status);
			if (param_status_validator.Validate() || ModelState.IsValid) return Ok(status_repository.Update(param_status, status_dto));
			else return BadRequest(param_status_validator.ListMessage.Concat(ModelState.Values.SelectMany(mse => mse.Errors).Select(me => me.ErrorMessage)));
		}
		[HttpPatch("{param_status}")]
		public IActionResult Patch(string param_status, [FromBody] StatusPatchDTO status_patch_dto) { // Responding with a patched status after patching.
			Validator param_status_validator = new Validator(param_status);
			if (param_status_validator.Validate() || ModelState.IsValid) return Ok(status_repository.Patch(param_status, status_patch_dto));
			else return BadRequest(param_status_validator.ListMessage.Concat(ModelState.Values.SelectMany(mse => mse.Errors).Select(me => me.ErrorMessage)));
		}
	}
}