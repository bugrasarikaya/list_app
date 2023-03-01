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
	[Route("api[controller]")]
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
		[HttpDelete("{id:int}")]
		public IActionResult Delete(int id) { // Responding with no content after deleting.
			Validator id_validator = new Validator(id);
			if (id_validator.IDValidator()) {
				category_repository.Delete(id);
				return NoContent();
			} else return BadRequest(id_validator.ListMessage);
		}
		[HttpGet("{id:int}")]
		public IActionResult Get(int id) { // Responding with a category after getting.
			Validator id_validator = new Validator(id);
			if (id_validator.IDValidator()) return Ok(category_repository.Get(id));
			else return BadRequest(id_validator.ListMessage);
		}
		[HttpGet]
		public IActionResult List() { // Responding with category list after getting.
			return Ok(category_repository.List());
		}
		[HttpPut("{id:int}")]
		public IActionResult Update(int id, [FromBody] CategoryDTO category_dto) { // Responding with an updated category after updating.
			Validator id_validator = new Validator(id);
			if (id_validator.IDValidator() || ModelState.IsValid) return Ok(category_repository.Update(id, category_dto));
			else return BadRequest(id_validator.ListMessage.Concat(ModelState.Values.SelectMany(mse => mse.Errors).Select(me => me.ErrorMessage)));
		}
		[HttpPatch("{id:int}")]
		public IActionResult Patch(int id, [FromBody] CategoryPatchDTO category_patch_dto) { // Responding with a patched category after patching.
			Validator id_validator = new Validator(id);
			if (id_validator.IDValidator() || ModelState.IsValid) return Ok(category_repository.Patch(id, category_patch_dto));
			else return BadRequest(id_validator.ListMessage.Concat(ModelState.Values.SelectMany(mse => mse.Errors).Select(me => me.ErrorMessage)));
		}
	}
}