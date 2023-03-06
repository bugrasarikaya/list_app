using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using list_api.Common;
using list_api.Models.DTOs;
using list_api.Repository.Interface;
using list_api.Models.Validators;
using list_api.Models.ViewModels;
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
			ValidationResult dto_validation_result = new ListDTOValidator().Validate(list_dto);
			if (dto_validation_result.IsValid) {
				ListViewModel list_created = list_repository.Create(list_dto);
				return Created(new Uri(Request.GetEncodedUrl() + "/" + list_created.ID), list_created);
			} else return BadRequest(dto_validation_result.Errors.Select(e => e.ErrorMessage));
		}
		[HttpDelete("{id_list:int}")]
		public IActionResult Delete(int id_list) { // Responding with no content after deleting.
			ParamValidator id_list_validator = new ParamValidator(id_list);
			if (id_list_validator.Validate()) {
				if (list_repository.Delete(id_list) == null) return NoContent();
				else return NotFound();
			} else return BadRequest(id_list_validator.ListMessage);
		}
		[HttpGet("{id_list:int}")]
		public IActionResult Get(int id_list) { // Responding with a list after getting.
			ParamValidator id_list_validator = new ParamValidator(id_list);
			if (id_list_validator.Validate()) return Ok(list_repository.Get(id_list));
			else return BadRequest(id_list_validator.ListMessage);
		}
		[HttpGet]
		public IActionResult List() { // Responding with all list.
			return Ok(list_repository.List());
		}
		[HttpGet("/category/{param_category}")]
		public IActionResult ListByCategory(string param_category) { // Responding with all list which have a specific category.
			ParamValidator param_category_validator = new ParamValidator(param_category);
			if (param_category_validator.Validate()) return Ok(list_repository.ListByCategory(param_category));
			else return BadRequest(param_category_validator.ListMessage);
		}
		[HttpGet("/datetimecompleting/{date_time_completing:DateTime}")]
		public IActionResult ListByDateTimeCompleting(DateTime date_time_completing) { // Responding with all list which have a specific completing date time.
			ParamValidator date_time_completing_validator = new ParamValidator(date_time_completing);
			if (date_time_completing_validator.Validate()) return Ok(list_repository.ListByDateTimeCompleting(date_time_completing));
			else return BadRequest(date_time_completing_validator.ListMessage);
		}
		[HttpGet("/datetimecreating/{date_time_creating:DateTime}")]
		public IActionResult ListByDateTimeCreating(DateTime date_time_creating) { // Responding with all list have a specific creating date time.
			ParamValidator date_time_creating_validator = new ParamValidator(date_time_creating);
			if (date_time_creating_validator.Validate()) return Ok(list_repository.ListByDateTimeCreating(date_time_creating));
			else return BadRequest(date_time_creating_validator.ListMessage);
		}
		[HttpGet("/datetimeupdating/{date_time_updating:DateTime}")]
		public IActionResult ListByDateTimeUpdating(DateTime date_time_updating) { // Responding with all list which have a specific updating date time.
			ParamValidator date_time_updating_validator = new ParamValidator(date_time_updating);
			if (date_time_updating_validator.Validate()) return Ok(list_repository.ListByDateTimeUpdating(date_time_updating));
			else return BadRequest(date_time_updating_validator.ListMessage);
		}
		[HttpGet("/user/{param_user}")]
		public IActionResult ListByUser(string param_user) { // Responding with all list which have a specific user.
			ParamValidator param_user_validator = new ParamValidator(param_user);
			if (param_user_validator.Validate()) return Ok(list_repository.ListByUser(param_user));
			else return BadRequest(param_user_validator.ListMessage);
		}
		[HttpGet("/categoryanddatetimecompleting/{param_category}/{date_time_completing:DateTime}")]
		public IActionResult ListByCategoryAndDateTimeCompleting(string param_category, DateTime date_time_completing) { // Responding with all list which have a specific category and completing date time.
			ParamValidator param_category_validator = new ParamValidator(param_category);
			ParamValidator date_time_completing_validator = new ParamValidator(date_time_completing);
			if (param_category_validator.Validate() || date_time_completing_validator.Validate()) return Ok(list_repository.ListByCategoryAndDateTimeCompleting(param_category, date_time_completing));
			else return BadRequest(param_category_validator.ListMessage.Concat(date_time_completing_validator.ListMessage));
		}
		[HttpGet("/categoryanddatetimecreating/{param_category}/{date_time_creating:DateTime}")]
		public IActionResult ListByCategoryAndDateTimeCreating(string param_category, DateTime date_time_creating) { // Responding with all list which have a specific category and creating date time.
			ParamValidator param_category_validator = new ParamValidator(param_category);
			ParamValidator date_time_creating_validator = new ParamValidator(date_time_creating);
			if (param_category_validator.Validate() || date_time_creating_validator.Validate()) return Ok(list_repository.ListByCategoryAndDateTimeCreating(param_category, date_time_creating));
			else return BadRequest(param_category_validator.ListMessage.Concat(date_time_creating_validator.ListMessage));
		}
		[HttpGet("/categoryanddatetimeupdating/{param_category}/{date_time_updating:DateTime}")]
		public IActionResult ListByCategoryAndDateTimeUpdating(string param_category, DateTime date_time_updating) { // Responding with all list which have a specific category and updating date time.
			ParamValidator param_category_validator = new ParamValidator(param_category);
			ParamValidator date_time_updating_validator = new ParamValidator(date_time_updating);
			if (param_category_validator.Validate() || date_time_updating_validator.Validate()) return Ok(list_repository.ListByCategoryAndDateTimeUpdating(param_category, date_time_updating));
			else return BadRequest(param_category_validator.ListMessage.Concat(date_time_updating_validator.ListMessage));
		}
		[HttpGet("/categoryanduser/{param_category}/{param_user}")]
		public IActionResult ListByCategoryAndUser(string param_category, string param_user) { // Responding with all list which have a specific category and user.
			ParamValidator param_category_validator = new ParamValidator(param_category);
			ParamValidator param_user_validator = new ParamValidator(param_user);
			if (param_category_validator.Validate() || param_user_validator.Validate()) return Ok(list_repository.ListByCategoryAndUser(param_category, param_user));
			else return BadRequest(param_category_validator.ListMessage.Concat(param_user_validator.ListMessage));
		}
		[HttpGet("/datetimecompletinganduser/{date_time_completing:DateTime}/{param_user}")]
		public IActionResult ListByDateTimeCompletingAndUser(DateTime date_time_completing, string param_user) { // Responding with all list which have a specific completing date time and user.
			ParamValidator date_time_completing_validator = new ParamValidator(date_time_completing);
			ParamValidator param_user_validator = new ParamValidator(param_user);
			if (param_user_validator.Validate() || date_time_completing_validator.Validate()) return Ok(list_repository.ListByDateTimeCompletingAndUser(date_time_completing, param_user));
			else return BadRequest(param_user_validator.ListMessage.Concat(date_time_completing_validator.ListMessage));
		}
		[HttpGet("/datetimecreatinganduser/{date_time_creating:DateTime}/{param_user}")]
		public IActionResult ListByDateTimeCreatingAndUser(DateTime date_time_creating, string param_user) { // Responding with all list which have a specific creating date time and user.
			ParamValidator date_time_creating_validator = new ParamValidator(date_time_creating);
			ParamValidator param_user_validator = new ParamValidator(param_user);
			if (param_user_validator.Validate() || date_time_creating_validator.Validate()) return Ok(list_repository.ListByDateTimeCreatingAndUser(date_time_creating, param_user));
			else return BadRequest(param_user_validator.ListMessage.Concat(date_time_creating_validator.ListMessage));
		}
		[HttpGet("/datetimeupdatinganduser/{date_time_updating:DateTime}/{param_user}")]
		public IActionResult ListByDateTimeUpdatingAndUser(DateTime date_time_updating, string param_user) { // Responding with all list which have a specific updating date time and user.
			ParamValidator date_time_updating_validator = new ParamValidator(date_time_updating);
			ParamValidator param_user_validator = new ParamValidator(param_user);
			if (param_user_validator.Validate() || date_time_updating_validator.Validate()) return Ok(list_repository.ListByDateTimeUpdatingAndUser(date_time_updating, param_user));
			else return BadRequest(param_user_validator.ListMessage.Concat(date_time_updating_validator.ListMessage));
		}
		[HttpGet("/categoryanddatetimecompletinganduser/{param_category}/{date_time_completing:DateTime}/{param_user}")]
		public IActionResult ListByCategoryAndDateTimeCompletingAndUser(string param_category, DateTime date_time_completing, string param_user) { // Responding with all list which have a specific category, completing date time and user.
			ParamValidator param_category_validator = new ParamValidator(param_category);
			ParamValidator date_time_completing_validator = new ParamValidator(date_time_completing);
			ParamValidator param_user_validator = new ParamValidator(param_user);
			if (param_category_validator.Validate() || date_time_completing_validator.Validate() || param_user_validator.Validate()) return Ok(list_repository.ListByCategoryAndDateTimeCompletingAndUser(param_category, date_time_completing, param_user));
			else return BadRequest(param_category_validator.ListMessage.Concat(param_user_validator.ListMessage.Concat(date_time_completing_validator.ListMessage)));
		}
		[HttpGet("/categoryanddatetimecreatinganduser/{param_category}/{date_time_creating:DateTime}/{param_user}")]
		public IActionResult ListByCategoryAndDateTimeCreatingAndUser(string param_category, DateTime date_time_creating, string param_user) { // Responding with all list which have a specific category, creating date time and user.
			ParamValidator param_category_validator = new ParamValidator(param_category);
			ParamValidator date_time_creating_validator = new ParamValidator(date_time_creating);
			ParamValidator param_user_validator = new ParamValidator(param_user);
			if (param_category_validator.Validate() || date_time_creating_validator.Validate() || param_user_validator.Validate()) return Ok(list_repository.ListByCategoryAndDateTimeCreatingAndUser(param_category, date_time_creating, param_user));
			else return BadRequest(param_category_validator.ListMessage.Concat(param_user_validator.ListMessage.Concat(date_time_creating_validator.ListMessage)));
		}
		[HttpGet("/categoryanddatetimeupdatinganduser/{param_category}/{date_time_updating:DateTime}/{param_user}")]
		public IActionResult ListByCategoryAndDateTimeUpdatingAndUser(string param_category, DateTime date_time_updating, string param_user) { // Responding with all list which have a specific category, updating date time and user.
			ParamValidator param_category_validator = new ParamValidator(param_category);
			ParamValidator date_time_updating_validator = new ParamValidator(date_time_updating);
			ParamValidator param_user_validator = new ParamValidator(param_user);
			if (param_category_validator.Validate() || date_time_updating_validator.Validate() || param_user_validator.Validate()) return Ok(list_repository.ListByCategoryAndDateTimeUpdatingAndUser(param_category, date_time_updating, param_user));
			else return BadRequest(param_category_validator.ListMessage.Concat(param_user_validator.ListMessage.Concat(date_time_updating_validator.ListMessage)));
		}
		[HttpPut("{id_list:int}")]
		public IActionResult Update(int id_list, [FromBody] ListDTO list_dto) { // Responding with an updated list after updating.
			ParamValidator id_list_validator = new ParamValidator(id_list);
			ValidationResult dto_validation_result = new ListDTOValidator().Validate(list_dto);
			if (id_list_validator.Validate() || dto_validation_result.IsValid) return Ok(list_repository.Update(id_list, list_dto));
			else return BadRequest(id_list_validator.ListMessage.Concat(dto_validation_result.Errors.Select(e => e.ErrorMessage)));
		}
		[HttpPatch("{id_list:int}")]
		public IActionResult Patch(int id_list, [FromBody] ListPatchDTO list_patch_dto) { // Responding with a patched list after updating.
			ParamValidator id_list_validator = new ParamValidator(id_list);
			ValidationResult dto_validation_result = new ListPatchDTOValidator().Validate(list_patch_dto);
			if (id_list_validator.Validate() || dto_validation_result.IsValid) return Ok(list_repository.Patch(id_list, list_patch_dto));
			else return BadRequest(id_list_validator.ListMessage.Concat(dto_validation_result.Errors.Select(e => e.ErrorMessage)));
		}
		[HttpPost("listproduct")]
		public IActionResult AddProduct([FromBody] ListProductDTO list_productdto) { // Responding with a list after adding a product.
			ValidationResult dto_validation_result = new ListProductDTOValidator().Validate(list_productdto);
			if (dto_validation_result.IsValid) return Ok(list_repository.AddProduct(list_productdto));
			else return BadRequest(dto_validation_result.Errors.Select(e => e.ErrorMessage));
		}
		[HttpDelete("listproduct/{id_list:int}/{id_product:int}")]
		public IActionResult RemoveProduct(int id_list, int id_product) { // Responding with a list after removing a product.
			ParamValidator id_list_validator = new ParamValidator(id_list);
			ParamValidator id_product_validator = new ParamValidator(id_product);
			if (id_list_validator.Validate() || id_product_validator.Validate()) return Ok(list_repository.RemoveProduct(id_list, id_product));
			else return BadRequest(id_list_validator.ListMessage.Concat(id_product_validator.ListMessage));
		}
	}
}