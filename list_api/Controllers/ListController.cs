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
			if (id_list_validator.Validate()) {
				list_repository.Delete(id_list);
				return NoContent();
			} else return BadRequest(id_list_validator.ListMessage);
		}
		[HttpGet("{id_list:int}")]
		public IActionResult Get(int id_list) { // Responding with a list after getting.
			Validator id_list_validator = new Validator(id_list);
			if (id_list_validator.Validate()) return Ok(list_repository.Get(id_list));
			else return BadRequest(id_list_validator.ListMessage);
		}
		[HttpGet]
		public IActionResult List() { // Responding with all list.
			return Ok(list_repository.List());
		}
		[HttpGet("/category/{param_category}")]
		public IActionResult ListByCategory(string param_category) { // Responding with all list which have a specific category.
			Validator param_category_validator = new Validator(param_category);
			if (param_category_validator.Validate()) return Ok(list_repository.ListByCategory(param_category));
			else return BadRequest(param_category_validator.ListMessage);
		}
		[HttpGet("/datetimecompleting/{date_time_completing:DateTime}")]
		public IActionResult ListByDateTimeCompleting(DateTime date_time_completing) { // Responding with all list which have a specific completing date time.
			Validator date_time_completing_validator = new Validator(date_time_completing);
			if (date_time_completing_validator.Validate()) return Ok(list_repository.ListByDateTimeCompleting(date_time_completing));
			else return BadRequest(date_time_completing_validator.ListMessage);
		}
		[HttpGet("/datetimecreating/{date_time_creating:DateTime}")]
		public IActionResult ListByDateTimeCreating(DateTime date_time_creating) { // Responding with all list have a specific creating date time.
			Validator date_time_creating_validator = new Validator(date_time_creating);
			if (date_time_creating_validator.Validate()) return Ok(list_repository.ListByDateTimeCreating(date_time_creating));
			else return BadRequest(date_time_creating_validator.ListMessage);
		}
		[HttpGet("/datetimeupdating/{date_time_updating:DateTime}")]
		public IActionResult ListByDateTimeUpdating(DateTime date_time_updating) { // Responding with all list which have a specific updating date time.
			Validator date_time_updating_validator = new Validator(date_time_updating);
			if (date_time_updating_validator.Validate()) return Ok(list_repository.ListByDateTimeUpdating(date_time_updating));
			else return BadRequest(date_time_updating_validator.ListMessage);
		}
		[HttpGet("/user/{param_user}")]
		public IActionResult ListByUser(string param_user) { // Responding with all list which have a specific user.
			Validator param_user_validator = new Validator(param_user);
			if (param_user_validator.Validate()) return Ok(list_repository.ListByUser(param_user));
			else return BadRequest(param_user_validator.ListMessage);
		}
		[HttpGet("/categoryanddatetimecompleting/{param_category}/{date_time_completing:DateTime}")]
		public IActionResult ListByCategoryAndDateTimeCompleting(string param_category, DateTime date_time_completing) { // Responding with all list which have a specific category and completing date time.
			Validator param_category_validator = new Validator(param_category);
			Validator date_time_completing_validator = new Validator(date_time_completing);
			if (param_category_validator.Validate() || date_time_completing_validator.Validate()) return Ok(list_repository.ListByCategoryAndDateTimeCompleting(param_category, date_time_completing));
			else return BadRequest(param_category_validator.ListMessage.Concat(date_time_completing_validator.ListMessage));
		}
		[HttpGet("/categoryanddatetimecreating/{param_category}/{date_time_creating:DateTime}")]
		public IActionResult ListByCategoryAndDateTimeCreating(string param_category, DateTime date_time_creating) { // Responding with all list which have a specific category and creating date time.
			Validator param_category_validator = new Validator(param_category);
			Validator date_time_creating_validator = new Validator(date_time_creating);
			if (param_category_validator.Validate() || date_time_creating_validator.Validate()) return Ok(list_repository.ListByCategoryAndDateTimeCreating(param_category, date_time_creating));
			else return BadRequest(param_category_validator.ListMessage.Concat(date_time_creating_validator.ListMessage));
		}
		[HttpGet("/categoryanddatetimeupdating/{param_category}/{date_time_updating:DateTime}")]
		public IActionResult ListByCategoryAndDateTimeUpdating(string param_category, DateTime date_time_updating) { // Responding with all list which have a specific category and updating date time.
			Validator param_category_validator = new Validator(param_category);
			Validator date_time_updating_validator = new Validator(date_time_updating);
			if (param_category_validator.Validate() || date_time_updating_validator.Validate()) return Ok(list_repository.ListByCategoryAndDateTimeUpdating(param_category, date_time_updating));
			else return BadRequest(param_category_validator.ListMessage.Concat(date_time_updating_validator.ListMessage));
		}
		[HttpGet("/categoryanduser/{param_category}/{param_user}")]
		public IActionResult ListByCategoryAndUser(string param_category, string param_user) { // Responding with all list which have a specific category and user.
			Validator param_category_validator = new Validator(param_category);
			Validator param_user_validator = new Validator(param_user);
			if (param_category_validator.Validate() || param_user_validator.Validate()) return Ok(list_repository.ListByCategoryAndUser(param_category, param_user));
			else return BadRequest(param_category_validator.ListMessage.Concat(param_user_validator.ListMessage));
		}
		[HttpGet("/datetimecompletinganduser/{date_time_completing:DateTime}/{param_user}")]
		public IActionResult ListByDateTimeCompletingAndUser(DateTime date_time_completing, string param_user) { // Responding with all list which have a specific completing date time and user.
			Validator date_time_completing_validator = new Validator(date_time_completing);
			Validator param_user_validator = new Validator(param_user);
			if (param_user_validator.Validate() || date_time_completing_validator.Validate()) return Ok(list_repository.ListByDateTimeCompletingAndUser(date_time_completing, param_user));
			else return BadRequest(param_user_validator.ListMessage.Concat(date_time_completing_validator.ListMessage));
		}
		[HttpGet("/datetimecreatinganduser/{date_time_creating:DateTime}/{param_user}")]
		public IActionResult ListByDateTimeCreatingAndUser(DateTime date_time_creating, string param_user) { // Responding with all list which have a specific creating date time and user.
			Validator date_time_creating_validator = new Validator(date_time_creating);
			Validator param_user_validator = new Validator(param_user);
			if (param_user_validator.Validate() || date_time_creating_validator.Validate()) return Ok(list_repository.ListByDateTimeCreatingAndUser(date_time_creating, param_user));
			else return BadRequest(param_user_validator.ListMessage.Concat(date_time_creating_validator.ListMessage));
		}
		[HttpGet("/datetimeupdatinganduser/{date_time_updating:DateTime}/{param_user}")]
		public IActionResult ListByDateTimeUpdatingAndUser(DateTime date_time_updating, string param_user) { // Responding with all list which have a specific updating date time and user.
			Validator date_time_updating_validator = new Validator(date_time_updating);
			Validator param_user_validator = new Validator(param_user);
			if (param_user_validator.Validate() || date_time_updating_validator.Validate()) return Ok(list_repository.ListByDateTimeUpdatingAndUser(date_time_updating, param_user));
			else return BadRequest(param_user_validator.ListMessage.Concat(date_time_updating_validator.ListMessage));
		}
		[HttpGet("/categoryanddatetimecompletinganduser/{param_category}/{date_time_completing:DateTime}/{param_user}")]
		public IActionResult ListByCategoryAndDateTimeCompletingAndUser(string param_category, DateTime date_time_completing, string param_user) { // Responding with all list which have a specific category, completing date time and user.
			Validator param_category_validator = new Validator(param_category);
			Validator date_time_completing_validator = new Validator(date_time_completing);
			Validator param_user_validator = new Validator(param_user);
			if (param_category_validator.Validate() || date_time_completing_validator.Validate() || param_user_validator.Validate()) return Ok(list_repository.ListByCategoryAndDateTimeCompletingAndUser(param_category, date_time_completing, param_user));
			else return BadRequest(param_category_validator.ListMessage.Concat(param_user_validator.ListMessage.Concat(date_time_completing_validator.ListMessage)));
		}
		[HttpGet("/categoryanddatetimecreatinganduser/{param_category}/{date_time_creating:DateTime}/{param_user}")]
		public IActionResult ListByCategoryAndDateTimeCreatingAndUser(string param_category, DateTime date_time_creating, string param_user) { // Responding with all list which have a specific category, creating date time and user.
			Validator param_category_validator = new Validator(param_category);
			Validator date_time_creating_validator = new Validator(date_time_creating);
			Validator param_user_validator = new Validator(param_user);
			if (param_category_validator.Validate() || date_time_creating_validator.Validate() || param_user_validator.Validate()) return Ok(list_repository.ListByCategoryAndDateTimeCreatingAndUser(param_category, date_time_creating, param_user));
			else return BadRequest(param_category_validator.ListMessage.Concat(param_user_validator.ListMessage.Concat(date_time_creating_validator.ListMessage)));
		}
		[HttpGet("/categoryanddatetimeupdatinganduser/{param_category}/{date_time_updating:DateTime}/{param_user}")]
		public IActionResult ListByCategoryAndDateTimeUpdatingAndUser(string param_category, DateTime date_time_updating, string param_user) { // Responding with all list which have a specific category, updating date time and user.
			Validator param_category_validator = new Validator(param_category);
			Validator date_time_updating_validator = new Validator(date_time_updating);
			Validator param_user_validator = new Validator(param_user);
			if (param_category_validator.Validate() || date_time_updating_validator.Validate() || param_user_validator.Validate()) return Ok(list_repository.ListByCategoryAndDateTimeUpdatingAndUser(param_category, date_time_updating, param_user));
			else return BadRequest(param_category_validator.ListMessage.Concat(param_user_validator.ListMessage.Concat(date_time_updating_validator.ListMessage)));
		}
		[HttpPut("{id_list:int}")]
		public IActionResult Update(int id_list, [FromBody] ListDTO list_dto) { // Responding with an updated list after updating.
			Validator id_list_validator = new Validator(id_list);
			if (id_list_validator.Validate() || ModelState.IsValid) return Ok(list_repository.Update(id_list, list_dto));
			else return BadRequest(id_list_validator.ListMessage.Concat(ModelState.Values.SelectMany(mse => mse.Errors).Select(me => me.ErrorMessage)));
		}
		[HttpPatch("{id_list:int}")]
		public IActionResult Patch(int id_list, [FromBody] ListPatchDTO list_patch_dto) { // Responding with a patched list after updating.
			Validator id_list_validator = new Validator(id_list);
			if (id_list_validator.Validate() || ModelState.IsValid) return Ok(list_repository.Patch(id_list, list_patch_dto));
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
			if (id_list_validator.Validate() || id_product_validator.Validate()) return Ok(list_repository.RemoveProduct(id_list, id_product));
			else return BadRequest(id_list_validator.ListMessage.Concat(id_product_validator.ListMessage));
		}
	}
}