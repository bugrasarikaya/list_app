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
	public class BrandController : ControllerBase {
		private readonly IBrandRepository brand_repository;
		public BrandController(IBrandRepository brand_repository) { // Constructing.
			this.brand_repository = brand_repository;
		}
		[HttpPost]
		public IActionResult Create([FromBody] BrandDTO brand_dto) { // Responding with a created brand after creating.
			if (ModelState.IsValid) {
				Brand brand_created = brand_repository.Create(brand_dto);
				return Created(new Uri(Request.GetEncodedUrl() + "/" + brand_created.ID), brand_created);
			} else return BadRequest(ModelState.Values.SelectMany(mse => mse.Errors).Select(me => me.ErrorMessage));
		}
		[HttpDelete("{param_brand}")]
		public IActionResult Delete(string param_brand) { // Responding with no content after deleting.
			Validator validator = new Validator(param_brand);
			if (validator.Validate()) {
				brand_repository.Delete(param_brand);
				return NoContent();
			} else return BadRequest(validator.ListMessage);
		}
		[HttpGet("{param_brand}")]
		public IActionResult Get(string param_brand) { // Responding with a brand after getting.
			Validator validator = new Validator(param_brand);
			if (validator.Validate()) return Ok(brand_repository.Get(param_brand));
			else return BadRequest(validator.ListMessage);
		}
		[HttpGet]
		public IActionResult List() { // Responding with brand list after getting.
			return Ok(brand_repository.List());
		}
		[HttpPut("{param_brand}")]
		public IActionResult Update(string param_brand, [FromBody] BrandDTO brand_dto) { // Responding with an updated brand after updating.
			Validator validator = new Validator(param_brand);
			if (validator.Validate() || ModelState.IsValid) return Ok(brand_repository.Update(param_brand, brand_dto));
			else return BadRequest(validator.ListMessage.Concat(ModelState.Values.SelectMany(mse => mse.Errors).Select(me => me.ErrorMessage)));
		}
		[HttpPatch("{param_brand}")]
		public IActionResult Patch(string param_brand, [FromBody] BrandPatchDTO brand_patch_dto) { // Responding with a patched brand after patching.
			Validator validator = new Validator(param_brand);
			if (validator.Validate() || ModelState.IsValid) return Ok(brand_repository.Patch(param_brand, brand_patch_dto));
			else return BadRequest(validator.ListMessage.Concat(ModelState.Values.SelectMany(mse => mse.Errors).Select(me => me.ErrorMessage)));
		}
	}
}