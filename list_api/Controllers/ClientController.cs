using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using list_api.Common;
using list_api.Models;
using list_api.Models.DTOs;
using list_api.Repository.Interface;
namespace list_api.Controllers {
	[ApiController]
	[Authorize(Roles = "Admin, User")]
	[Route("api[controller]")]
	public class ClientController : ControllerBase {
		private readonly IClientRepository client_repository;
		public ClientController(IClientRepository client_repository) { // Constructing.
			this.client_repository = client_repository;
			this.client_repository.IDUser = int.Parse(User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.NameIdentifier)!.Value);
		}
		[HttpPost("/list")]
		public IActionResult CreateList([FromBody] ClientListDTO list_client_dto) { // Responding with a created list after creating for accessed user.
			if (ModelState.IsValid) {
				List list_created = client_repository.CreateList(list_client_dto);
				return Created(new Uri(Request.GetEncodedUrl() + "/list/" + list_created.ID), list_created);
			} else return BadRequest(ModelState.Values.SelectMany(mse => mse.Errors).Select(me => me.ErrorMessage));
		}
		[HttpDelete("/list/{id_list:int}")]
		public IActionResult DeleteList(int id_list) { // Responding with no content after deleting for accessed user.
			Validator id_list_validator = new Validator(id_list);
			if (id_list_validator.IDValidator()) {
				client_repository.DeleteList(id_list);
				return NoContent();
			} else return BadRequest(id_list_validator.ListMessage);
		}
		[HttpGet("/list/{id_list:int}")]
		public IActionResult GetList(int id_list) { // Responding with a list after getting for accessed user.
			Validator id_list_validator = new Validator(id_list);
			if (id_list_validator.IDValidator()) return Ok(client_repository.GetList(id_list));
			else return BadRequest(id_list_validator.ListMessage);
		}
		[HttpGet("/list")]
		public IActionResult ListLists() { // Responding with list list for accessed user.
			return Ok(client_repository.ListLists());
		}
		[HttpGet("/list/{id_category:int}")]
		public IActionResult ListListsByCategory(int id_category) { // Responding with list list by category for accessed user.
			Validator id_category_validator = new Validator(id_category);
			if (id_category_validator.IDValidator()) return Ok(client_repository.ListListsByCategory(id_category));
			else return BadRequest(id_category_validator.ListMessage);
		}
		[HttpPut("/list/{id_list:int}")]
		public IActionResult UpdateList(int id_list, [FromBody] ClientListDTO list_client_dto) { // Responding with an updated list for accessed user.
			Validator id_list_validator = new Validator(id_list);
			if (id_list_validator.IDValidator() || ModelState.IsValid) return Ok(client_repository.UpdateList(id_list, list_client_dto));
			else return BadRequest(id_list_validator.ListMessage.Concat(ModelState.Values.SelectMany(mse => mse.Errors).Select(me => me.ErrorMessage)));
		}
		[HttpPatch("/list/{id_list:int}")]
		public IActionResult PatchList(int id_list, [FromBody] ClientListPatchDTO list_patch_dto) { // Responding with a patched list for accessed user.
			Validator id_list_validator = new Validator(id_list);
			if (id_list_validator.IDValidator() || ModelState.IsValid) return Ok(client_repository.PatchList(id_list, list_patch_dto));
			else return BadRequest(id_list_validator.ListMessage.Concat(ModelState.Values.SelectMany(mse => mse.Errors).Select(me => me.ErrorMessage)));
		}
		[HttpPost("/listproduct")]
		public IActionResult AddProduct([FromBody] ListProductDTO list_product_dto) { // Responding with list product after adding a product to a list for accessed user.
			if (ModelState.IsValid) return Ok(client_repository.AddProduct(list_product_dto));
			else return BadRequest(ModelState.Values.SelectMany(mse => mse.Errors).Select(me => me.ErrorMessage));
		}
		[HttpDelete("/listproduct/{id_list:int}/{id_product:int}")]
		public IActionResult RemoveProduct(int id_list, int id_product) { // Responding with list after removing a product from a list for accessed user.
			Validator id_list_validator = new Validator(id_list);
			Validator id_product_validator = new Validator(id_product);
			if (id_list_validator.IDValidator() || id_product_validator.IDValidator()) return Ok(client_repository.RemoveProduct(id_list, id_product));
			else return BadRequest(id_list_validator.ListMessage.Concat(id_product_validator.ListMessage));
		}
		[HttpGet("/listproduct/{id_list:int}")]
		public IActionResult ClearProducts(int id_list) { // Responding with a list after clearing all products.
			Validator id_list_validator = new Validator(id_list);
			if (id_list_validator.IDValidator()) return Ok(client_repository.ClearProducts(id_list));
			else return BadRequest(id_list_validator.ListMessage);
		}
		[HttpGet("/user")]
		public IActionResult GetUser() { // Responding with a user after getting for accessed user.
			return Ok(client_repository.GetUser());
		}
		[HttpPut("/user")]
		public IActionResult UpdateUser([FromBody] ClientUserDTO client_user_dto) { // Responding with a user after updating for accessed user.
			if (ModelState.IsValid) return Ok(client_repository.UpdateUser(client_user_dto));
			else return BadRequest(ModelState.Values.SelectMany(mse => mse.Errors).Select(me => me.ErrorMessage));
		}
		[HttpPatch("/user")]
		public IActionResult PatchUser([FromBody] ClientUserPatchDTO client_user_patch_dto) { // Responding with a user after patching for accessed user.
			if (ModelState.IsValid) return Ok(client_repository.PatchUser(client_user_patch_dto));
			else return BadRequest(ModelState.Values.SelectMany(mse => mse.Errors).Select(me => me.ErrorMessage));
		}
	}
}