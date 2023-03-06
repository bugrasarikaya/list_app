using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using list_api.Common;
using list_api.Models.DTOs;
using list_api.Repository.Interface;
using list_api.Models.Validators;
using list_api.Models.ViewModels;
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
			ValidationResult dto_validation_result = new ClientListDTOValidator().Validate(list_client_dto);
			if (dto_validation_result.IsValid) {
				ListViewModel list_view_model_created = client_repository.CreateList(list_client_dto);
				return Created(new Uri(Request.GetEncodedUrl() + "/list/" + list_view_model_created.ID), list_view_model_created);
			} else return BadRequest(dto_validation_result.Errors.Select(e => e.ErrorMessage));
		}
		[HttpDelete("list/{param_list}")]
		public IActionResult DeleteList(string param_list) { // Responding with no content after deleting for accessed user.
			ParamValidator param_list_validator = new ParamValidator(param_list);
			if (param_list_validator.Validate()) {
				if (client_repository.DeleteList(param_list) == null) return NoContent();
				else return NotFound();
			} else return BadRequest(param_list_validator.ListMessage);
		}
		[HttpGet("list/{param_list}")]
		public IActionResult GetList(string param_list) { // Responding with a list after getting for accessed user.
			ParamValidator param_list_validator = new ParamValidator(param_list);
			if (param_list_validator.Validate()) return Ok(client_repository.GetList(param_list));
			else return BadRequest(param_list_validator.ListMessage);
		}
		[HttpGet("lists")]
		public IActionResult ListLists() { // Responding with list list for accessed user.
			return Ok(client_repository.ListLists());
		}
		[HttpGet("lists/{param_category}")]
		public IActionResult ListListsByCategory(string param_category) { // Responding with list list by category for accessed user.
			ParamValidator param_category_validator = new ParamValidator(param_category);
			if (param_category_validator.Validate()) return Ok(client_repository.ListListsByCategory(param_category));
			else return BadRequest(param_category_validator.ListMessage);
		}
		[HttpGet("lists/datetimecompleting/{date_time_completing:DateTime}")]
		public IActionResult ListByDateTimeCompleting(DateTime date_time_completing) { // Responding with all list which have a specific completing date time for accessed user.
			ParamValidator date_time_completing_validator = new ParamValidator(date_time_completing);
			if (date_time_completing_validator.Validate()) return Ok(client_repository.ListByDateTimeCompleting(date_time_completing));
			else return BadRequest(date_time_completing_validator.ListMessage);
		}
		[HttpGet("lists/datetimecreating/{date_time_creating:DateTime}")]
		public IActionResult ListByDateTimeCreating(DateTime date_time_creating) { // Responding with all list which have a specific creating date time for accessed user.
			ParamValidator date_time_creating_validator = new ParamValidator(date_time_creating);
			if (date_time_creating_validator.Validate()) return Ok(client_repository.ListByDateTimeCreating(date_time_creating));
			else return BadRequest(date_time_creating_validator.ListMessage);
		}
		[HttpGet("lists/datetimeupdating/{date_time_updating:DateTime}")]
		public IActionResult ListByDateTimeUpdating(DateTime date_time_updating) { // Responding with all list which have a specific updating date time for accessed user.
			ParamValidator date_time_updating_validator = new ParamValidator(date_time_updating);
			if (date_time_updating_validator.Validate()) return Ok(client_repository.ListByDateTimeUpdating(date_time_updating));
			else return BadRequest(date_time_updating_validator.ListMessage);
		}
		[HttpGet("lists/categoryanddatetimecompleting/{param_category}/{date_time_completing:DateTime}")]
		public IActionResult ListByCategoryAndDateTimeCompleting(string param_category, DateTime date_time_completing) { // Responding with all list which have a specific category and completing date time for accessed user.
			ParamValidator param_category_validator = new ParamValidator(param_category);
			ParamValidator date_time_completing_validator = new ParamValidator(date_time_completing);
			if (param_category_validator.Validate() || date_time_completing_validator.Validate()) return Ok(client_repository.ListByCategoryAndDateTimeCompleting(param_category, date_time_completing));
			else return BadRequest(param_category_validator.ListMessage.Concat(date_time_completing_validator.ListMessage));
		}
		[HttpGet("lists/categoryanddatetimecreating/{param_category}/{date_time_creating:DateTime}")]
		public IActionResult ListByCategoryAndDateTimeCreating(string param_category, DateTime date_time_creating) { // Responding with all list which have a specific category and creating date time for accessed user.
			ParamValidator param_category_validator = new ParamValidator(param_category);
			ParamValidator date_time_creating_validator = new ParamValidator(date_time_creating);
			if (param_category_validator.Validate() || date_time_creating_validator.Validate()) return Ok(client_repository.ListByCategoryAndDateTimeCreating(param_category, date_time_creating));
			else return BadRequest(param_category_validator.ListMessage.Concat(date_time_creating_validator.ListMessage));
		}
		[HttpGet("lists/categoryanddatetimeupdating/{param_category}/{date_time_updating:DateTime}")]
		public IActionResult ListByCategoryAndDateTimeUpdating(string param_category, DateTime date_time_updating) { // Responding with all list which have a specific category and updating date time for accessed user.
			ParamValidator param_category_validator = new ParamValidator(param_category);
			ParamValidator date_time_updating_validator = new ParamValidator(date_time_updating);
			if (param_category_validator.Validate() || date_time_updating_validator.Validate()) return Ok(client_repository.ListByCategoryAndDateTimeUpdating(param_category, date_time_updating));
			else return BadRequest(param_category_validator.ListMessage.Concat(date_time_updating_validator.ListMessage));
		}
		[HttpPut("list/{param_list}")]
		public IActionResult UpdateList(string param_list, [FromBody] ClientListDTO list_client_dto) { // Responding with an updated list for accessed user.
			ParamValidator param_list_validator = new ParamValidator(param_list);
			ValidationResult dto_validation_result = new ClientListDTOValidator().Validate(list_client_dto);
			if (param_list_validator.Validate() || dto_validation_result.IsValid) return Ok(client_repository.UpdateList(param_list, list_client_dto));
			else return BadRequest(param_list_validator.ListMessage.Concat(dto_validation_result.Errors.Select(e => e.ErrorMessage)));
		}
		[HttpPatch("list/{param_list}")]
		public IActionResult PatchList(string param_list, [FromBody] ClientListPatchDTO list_patch_dto) { // Responding with a patched list for accessed user.
			ParamValidator param_list_validator = new ParamValidator(param_list);
			ValidationResult dto_validation_result = new ClientListPatchDTOValidator().Validate(list_patch_dto);
			if (param_list_validator.Validate() || dto_validation_result.IsValid) return Ok(client_repository.PatchList(param_list, list_patch_dto));
			else return BadRequest(param_list_validator.ListMessage.Concat(dto_validation_result.Errors.Select(e => e.ErrorMessage)));
		}
		[HttpPost("listproduct")]
		public IActionResult AddProduct([FromBody] ListProductDTO list_product_dto) { // Responding with list product after adding a product to a list for accessed user.
			ValidationResult dto_validation_result = new ListProductDTOValidator().Validate(list_product_dto);
			if (dto_validation_result.IsValid) return Ok(client_repository.AddProduct(list_product_dto));
			else return BadRequest(dto_validation_result.Errors.Select(e => e.ErrorMessage));
		}
		[HttpDelete("listproduct/{param_list}/{param_product}")]
		public IActionResult RemoveProduct(string param_list, int id_product) { // Responding with list after removing a product from a list for accessed user.
			ParamValidator param_list_validator = new ParamValidator(param_list);
			ParamValidator id_product_validator = new ParamValidator(id_product);
			if (param_list_validator.Validate() || id_product_validator.Validate()) return Ok(client_repository.RemoveProduct(param_list, id_product));
			else return BadRequest(param_list_validator.ListMessage.Concat(id_product_validator.ListMessage));
		}
		[HttpGet("user")]
		public IActionResult GetUser() { // Responding with a user after getting for accessed user.
			return Ok(client_repository.GetUser());
		}
		[HttpPut("user")]
		public IActionResult UpdateUser([FromBody] ClientUserDTO client_user_dto) { // Responding with a user after updating for accessed user.
			ValidationResult dto_validation_result = new ClientUserDTOValidator().Validate(client_user_dto);
			if (dto_validation_result.IsValid) return Ok(client_repository.UpdateUser(client_user_dto));
			else return BadRequest(dto_validation_result.Errors.Select(e => e.ErrorMessage));
		}
		[HttpPatch("user")]
		public IActionResult PatchUser([FromBody] ClientUserPatchDTO client_user_patch_dto) { // Responding with a user after patching for accessed user.
			ValidationResult dto_validation_result = new ClientUserPatchDTOValidator().Validate(client_user_patch_dto);
			if (dto_validation_result.IsValid) return Ok(client_repository.PatchUser(client_user_patch_dto));
			else return BadRequest(dto_validation_result.Errors.Select(e => e.ErrorMessage));
		}
	}
}