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
	public class ListController : ControllerBase {
		private readonly IListRepository list_repository;
		public ListController(IListRepository list_repository) { // Constructing.
			this.list_repository = list_repository;
		}
		[HttpPost]
		public IActionResult Create([FromBody] ListDTO list_dto) { // Responding with a created list after creating.
			if (ModelState.IsValid) {
				List list_created = list_repository.Create(list_dto);
				return Created(new Uri(Request.GetEncodedUrl() + "/" + list_created.ID), list_created);
			} else return BadRequest(ModelState.Values.SelectMany(mse => mse.Errors).Select(me => me.ErrorMessage));
		}
		[HttpDelete("{id_list:int}")]
		public IActionResult Delete(int id_list) { // Responding with no content after deleting.
			Validator id_list_validator = new Validator(id_list);
			if (id_list_validator.IDValidator()) {
				list_repository.Delete(id_list);
				return NoContent();
			} else return BadRequest(id_list_validator.ListMessage);
		}
		[HttpGet("{id_list:int}")]
		public IActionResult Get(int id_list) { // Responding with a list after getting.
			Validator id_list_validator = new Validator(id_list);
			if (id_list_validator.IDValidator()) return Ok(list_repository.Get(id_list));
			else return BadRequest(id_list_validator.ListMessage);
		}
		[HttpGet]
		public IActionResult List() { // Responding with list list.
			return Ok(list_repository.List());
		}
		[HttpGet("{id_category:int}")]
		public IActionResult ListByCategory(int id_category) { // Listing all products which have a specific category.
			Validator id_category_validator = new Validator(id_category);
			if (id_category_validator.IDValidator()) return Ok(list_repository.ListByCategory(id_category));
			else return BadRequest(id_category_validator.ListMessage);
		}
		[HttpGet("{id_user:int}")]
		public IActionResult ListByUser(int id_user) { // Listing all products which have a specific user.
			Validator id_user_validator = new Validator(id_user);
			if (id_user_validator.IDValidator()) return Ok(list_repository.ListByUser(id_user));
			else return BadRequest(id_user_validator.ListMessage);
		}
		[HttpGet("{id_category:int}/{id_user:int}")]
		public IActionResult ListByCategoryAndUser(int id_category, int id_user) { // Listing all products which have a specific category and user.
			Validator id_category_validator = new Validator(id_category);
			Validator id_user_validator = new Validator(id_user);
			if (id_category_validator.IDValidator() || id_user_validator.IDValidator()) return Ok(list_repository.ListByCategoryAndUser(id_category, id_user));
			else return BadRequest(id_category_validator.ListMessage.Concat(id_user_validator.ListMessage));
		}
		[HttpPut("{id_list:int}")]
		public IActionResult Update(int id_list, [FromBody] ListDTO list_dto) { // Responding with an updated list after updating.
			Validator id_list_validator = new Validator(id_list);
			if (id_list_validator.IDValidator() || ModelState.IsValid) return Ok(list_repository.Update(id_list, list_dto));
			else return BadRequest(id_list_validator.ListMessage.Concat(ModelState.Values.SelectMany(mse => mse.Errors).Select(me => me.ErrorMessage)));
		}
		[HttpPatch("{id_list:int}")]
		public IActionResult Patch(int id_list, [FromBody] ListPatchDTO list_patch_dto) { // Responding with a patched list after updating.
			Validator id_list_validator = new Validator(id_list);
			if (id_list_validator.IDValidator() || ModelState.IsValid) return Ok(list_repository.Patch(id_list, list_patch_dto));
			else return BadRequest(id_list_validator.ListMessage.Concat(ModelState.Values.SelectMany(mse => mse.Errors).Select(me => me.ErrorMessage)));
		}
		[HttpPost("listproduct")]
		public IActionResult AddProduct([FromBody] ListProductDTO list_productdto) { // Responding with a list after adding a product.
			if (ModelState.IsValid) return Ok(list_repository.AddProduct(list_productdto));
			else return BadRequest(ModelState.Values.SelectMany(mse => mse.Errors).Select(me => me.ErrorMessage));
		}
		[HttpDelete("listproduct/{id_list:int}/{id_product:int}")]
		public IActionResult RemoveProduct(int id_list, int id_product) { // Responding with a list after removing a product.
			Validator id_list_validator = new Validator(id_list);
			Validator id_product_validator = new Validator(id_product);
			if (id_list_validator.IDValidator() || id_product_validator.IDValidator()) return Ok(list_repository.RemoveProduct(id_list, id_product));
			else return BadRequest(id_list_validator.ListMessage.Concat(id_product_validator.ListMessage));
		}
		[HttpGet("listproduct/{id_list:int}")]
		public IActionResult ClearProducts(int id_list) { // Responding with a list after clearing all products.
			Validator id_list_validator = new Validator(id_list);
			if (id_list_validator.IDValidator()) return Ok(list_repository.ClearProducts(id_list));
			else return BadRequest(id_list_validator.ListMessage);
		}
	}
}