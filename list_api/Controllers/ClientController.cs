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
	[Route("api/[controller]")]
	public class ClientController : ControllerBase {
		private readonly IClientRepository client_repository;
		public ClientController(IClientRepository client_repository) { // Constructing.
			this.client_repository = client_repository;
			this.client_repository.IDUser = int.Parse(User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.NameIdentifier)!.Value);
		}
		[HttpPost("list")]
		public IActionResult CreateList([FromBody] ClientListDTO list_client_dto) { // Responding with a created list after updating for accessed user.
			if (ModelState.IsValid) {
				List list_created = client_repository.CreateList(list_client_dto);
				return Created(new Uri(Request.GetEncodedUrl() + "/list/" + list_created.ID), list_created);
			} else return BadRequest(ModelState.Values.SelectMany(mse => mse.Errors).Select(me => me.ErrorMessage));
		}
		[HttpDelete("list/{param_list}")]
		public IActionResult DeleteList(string param_list) { // Responding with no content after deleting for accessed user.
			Validator param_list_validator = new Validator(param_list);
			if (param_list_validator.Validate()) {
				client_repository.DeleteList(param_list);
				return NoContent();
			} else return BadRequest(param_list_validator.ListMessage);
		}
		[HttpGet("list/{param_list}")]
		public IActionResult GetList(string param_list) { // Responding with a list after getting for accessed user.
			Validator param_list_validator = new Validator(param_list);
			if (param_list_validator.Validate()) return Ok(client_repository.GetList(param_list));
			else return BadRequest(param_list_validator.ListMessage);
		}
		[HttpGet("lists")]
		public IActionResult ListLists() { // Responding with list list for accessed user.
			return Ok(client_repository.ListLists());
		}
		[HttpGet("lists/{param_category}")]
		public IActionResult ListListsByCategory(string param_category) { // Responding with list list by category for accessed user.
			Validator param_category_validator = new Validator(param_category);
			if (param_category_validator.Validate()) return Ok(client_repository.ListListsByCategory(param_category));
			else return BadRequest(param_category_validator.ListMessage);
		}
		[HttpGet("lists/datetimecompleting/{date_time_completing:DateTime}")]
		public IActionResult ListByDateTimeCompleting(DateTime date_time_completing) { // Responding with all list which have a specific completing date time for accessed user.
			Validator date_time_completing_validator = new Validator(date_time_completing);
			if (date_time_completing_validator.Validate()) return Ok(client_repository.ListByDateTimeCompleting(date_time_completing));
			else return BadRequest(date_time_completing_validator.ListMessage);
		}
		[HttpGet("lists/datetimecreating/{date_time_creating:DateTime}")]
		public IActionResult ListByDateTimeCreating(DateTime date_time_creating) { // Responding with all list which have a specific creating date time for accessed user.
			Validator date_time_creating_validator = new Validator(date_time_creating);
			if (date_time_creating_validator.Validate()) return Ok(client_repository.ListByDateTimeCreating(date_time_creating));
			else return BadRequest(date_time_creating_validator.ListMessage);
		}
		[HttpGet("lists/datetimeupdating/{date_time_updating:DateTime}")]
		public IActionResult ListByDateTimeUpdating(DateTime date_time_updating) { // Responding with all list which have a specific updating date time for accessed user.
			Validator date_time_updating_validator = new Validator(date_time_updating);
			if (date_time_updating_validator.Validate()) return Ok(client_repository.ListByDateTimeUpdating(date_time_updating));
			else return BadRequest(date_time_updating_validator.ListMessage);
		}
		[HttpGet("lists/categoryanddatetimecompleting/{param_category}/{date_time_completing:DateTime}")]
		public IActionResult ListByCategoryAndDateTimeCompleting(string param_category, DateTime date_time_completing) { // Responding with all list which have a specific category and completing date time for accessed user.
			Validator param_category_validator = new Validator(param_category);
			Validator date_time_completing_validator = new Validator(date_time_completing);
			if (param_category_validator.Validate() || date_time_completing_validator.Validate()) return Ok(client_repository.ListByCategoryAndDateTimeCompleting(param_category, date_time_completing));
			else return BadRequest(param_category_validator.ListMessage.Concat(date_time_completing_validator.ListMessage));
		}
		[HttpGet("lists/categoryanddatetimecreating/{param_category}/{date_time_creating:DateTime}")]
		public IActionResult ListByCategoryAndDateTimeCreating(string param_category, DateTime date_time_creating) { // Responding with all list which have a specific category and creating date time for accessed user.
			Validator param_category_validator = new Validator(param_category);
			Validator date_time_creating_validator = new Validator(date_time_creating);
			if (param_category_validator.Validate() || date_time_creating_validator.Validate()) return Ok(client_repository.ListByCategoryAndDateTimeCreating(param_category, date_time_creating));
			else return BadRequest(param_category_validator.ListMessage.Concat(date_time_creating_validator.ListMessage));
		}
		[HttpGet("lists/categoryanddatetimeupdating/{param_category}/{date_time_updating:DateTime}")]
		public IActionResult ListByCategoryAndDateTimeUpdating(string param_category, DateTime date_time_updating) { // Responding with all list which have a specific category and updating date time for accessed user.
			Validator param_category_validator = new Validator(param_category);
			Validator date_time_updating_validator = new Validator(date_time_updating);
			if (param_category_validator.Validate() || date_time_updating_validator.Validate()) return Ok(client_repository.ListByCategoryAndDateTimeUpdating(param_category, date_time_updating));
			else return BadRequest(param_category_validator.ListMessage.Concat(date_time_updating_validator.ListMessage));
		}
		[HttpPut("list/{param_list}")]
		public IActionResult UpdateList(string param_list, [FromBody] ClientListDTO list_client_dto) { // Responding with an updated list for accessed user.
			Validator param_list_validator = new Validator(param_list);
			if (param_list_validator.Validate() || ModelState.IsValid) return Ok(client_repository.UpdateList(param_list, list_client_dto));
			else return BadRequest(param_list_validator.ListMessage.Concat(ModelState.Values.SelectMany(mse => mse.Errors).Select(me => me.ErrorMessage)));
		}
		[HttpPatch("list/{param_list}")]
		public IActionResult PatchList(string param_list, [FromBody] ClientListPatchDTO list_patch_dto) { // Responding with a patched list for accessed user.
			Validator param_list_validator = new Validator(param_list);
			if (param_list_validator.Validate() || ModelState.IsValid) return Ok(client_repository.PatchList(param_list, list_patch_dto));
			else return BadRequest(param_list_validator.ListMessage.Concat(ModelState.Values.SelectMany(mse => mse.Errors).Select(me => me.ErrorMessage)));
		}
		[HttpPost("listproduct")]
		public IActionResult AddProduct([FromBody] ListProductDTO list_product_dto) { // Responding with list product after adding a product to a list for accessed user.
			if (ModelState.IsValid) return Ok(client_repository.AddProduct(list_product_dto));
			else return BadRequest(ModelState.Values.SelectMany(mse => mse.Errors).Select(me => me.ErrorMessage));
		}
		[HttpDelete("listproduct/{param_list}/{param_product}")]
		public IActionResult RemoveProduct(string param_list, int id_product) { // Responding with list after removing a product from a list for accessed user.
			Validator param_list_validator = new Validator(param_list);
			Validator id_product_validator = new Validator(id_product);
			if (param_list_validator.Validate() || id_product_validator.Validate()) return Ok(client_repository.RemoveProduct(param_list, id_product));
			else return BadRequest(param_list_validator.ListMessage.Concat(id_product_validator.ListMessage));
		}
		[HttpGet("user")]
		public IActionResult GetUser() { // Responding with a user after getting for accessed user.
			return Ok(client_repository.GetUser());
		}
		[HttpPut("user")]
		public IActionResult UpdateUser([FromBody] ClientUserDTO client_user_dto) { // Responding with a user after updating for accessed user.
			if (ModelState.IsValid) return Ok(client_repository.UpdateUser(client_user_dto));
			else return BadRequest(ModelState.Values.SelectMany(mse => mse.Errors).Select(me => me.ErrorMessage));
		}
		[HttpPatch("user")]
		public IActionResult PatchUser([FromBody] ClientUserPatchDTO client_user_patch_dto) { // Responding with a user after patching for accessed user.
			if (ModelState.IsValid) return Ok(client_repository.PatchUser(client_user_patch_dto));
			else return BadRequest(ModelState.Values.SelectMany(mse => mse.Errors).Select(me => me.ErrorMessage));
		}
	}
}