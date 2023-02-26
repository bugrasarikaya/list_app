using list_api.Models;
using list_api.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using list_api.Common;
namespace list_api.Controllers {
	[ApiController]
	[Authorize(Roles = "Admin")]
	[Route("api[controller]")]
	public class ListController : ControllerBase {
		private readonly IListRepository list_repository;
		public ListController(IListRepository list_repository) { // Constructing.
			this.list_repository = list_repository;
		}
		[HttpPost]
		public IActionResult Create([FromBody] List list) { // Responding with a created list after creating.
			if (ModelState.IsValid) {
				List? list_created = list_repository.Create(list);
				return Created("api/List/" + list_created.ID, list_created);
			} else return BadRequest(string.Join(" ", ModelState.Values.SelectMany(mse => mse.Errors).Select(me => me.ErrorMessage)));
		}
		[HttpDelete("{id_list:int}")]
		public IActionResult Delete(int id_list) { // Responding with no content after deleting.
			Validator id_list_validator = new Validator(id_list);
			if (id_list_validator.IDValidator() || ModelState.IsValid) {
				List? list_deleted = list_repository.Delete(id_list);
				if (list_deleted != null) return NoContent();
				else return NotFound();
			} else return BadRequest(string.Join(" ", id_list_validator.ListMessage) + " " + string.Join(" ", ModelState.Values.SelectMany(mse => mse.Errors).Select(me => me.ErrorMessage)));
		}
		[HttpGet("{id_list:int}")] //ListViewModel Get(int id)
		public IActionResult Get(int id_list) { // Responding with a list after getting.
			Validator id_list_validator = new Validator(id_list);
			if (id_list_validator.IDValidator()) return Ok(list_repository.Get(id_list));
			else return BadRequest(string.Join(" ", id_list_validator.ListMessage));
		}
		[HttpGet]
		public IActionResult List() { // Responding with list list.
			return Ok(list_repository.List());
		}
		[HttpGet("{id_category:int}")]
		public IActionResult ListByCategory(int id_category) { // Listing all products which have a specific category.
			Validator id_category_validator = new Validator(id_category);
			if (id_category_validator.IDValidator()) return Ok(list_repository.ListByCategory(id_category));
			else return BadRequest(string.Join(" ", id_category_validator.ListMessage));
		}
		//[HttpGet("{name_category}")]
		//public IActionResult ListByNameCategory(string name_category) { // Listing all products which have a specific category name.
		//	return Ok(list_repository.ListByNameCategory(name_category));
		//}
		[HttpGet("{id_user:int}")]
		public IActionResult ListByUser(int id_user) { // Listing all products which have a specific user.
			Validator id_user_validator = new Validator(id_user);
			if (id_user_validator.IDValidator()) return Ok(list_repository.ListByUser(id_user));
			else return BadRequest(string.Join(" ", id_user_validator.ListMessage));
		}
		//[HttpGet("{name_user}")]
		//public IActionResult ListByNameUser(string name_user) { // Listing all products which have a specific user name.
		//	return Ok(list_repository.ListByNameUser(name_user));
		//}
		[HttpGet("{id_category:int}/{id_user:int}")]
		public IActionResult ListByCategoryAndUser(int id_category, int id_user) { // Listing all products which have a specific category and user.
			Validator id_category_validator = new Validator(id_category);
			Validator id_user_validator = new Validator(id_user);
			if (id_category_validator.IDValidator() || id_user_validator.IDValidator()) return Ok(list_repository.ListByCategoryAndUser(id_category, id_user));
			else return BadRequest(string.Join(" ", id_category_validator.ListMessage) + " " + string.Join(" ", id_user_validator.ListMessage));
		}
		//[HttpGet("{name_category}/{name_user}")]
		//public IActionResult ListByNameCategoryAndNAmeUser(string name_category, string name_user) { // Listing all products which have a specific category name and user name.
		//	return Ok(list_repository.ListByNameCategoryAndNameUser(name_category, name_user));
		//}
		[HttpPut("{id_list:int}")]
		public IActionResult Update(int id_list, [FromBody] List list) { // Responding with an updated list after updating.
			Validator id_list_validator = new Validator(id_list);
			if (id_list_validator.IDValidator() || ModelState.IsValid) return Ok(list_repository.Update(id_list, list));
			else return BadRequest(string.Join(" ", id_list_validator.ListMessage) + " " + string.Join(" ", ModelState.Values.SelectMany(mse => mse.Errors).Select(me => me.ErrorMessage)));
		}
		[HttpGet("{id_list:int}")]
		public IActionResult SetCompleted(int id_list) { // Responding with a list after setting to "Completed".
			Validator id_list_validator = new Validator(id_list);
			if (id_list_validator.IDValidator()) return Ok(list_repository.SetCompleted(id_list));
			else return BadRequest(string.Join(" ", id_list_validator.ListMessage));
		}
		[HttpPost]
		public IActionResult Add([FromBody] ListProduct list_product) { // Responding with a list after adding a product.
			if (ModelState.IsValid) return Ok(list_repository.Add(list_product));
			else return BadRequest(string.Join(" ", ModelState.Values.SelectMany(mse => mse.Errors).Select(me => me.ErrorMessage)));
		}
		[HttpDelete("{id_list:int}/{id_product:int}")]
		public IActionResult Remove(int id_list, int id_product) { // Responding with a list after removing a product.
			Validator id_list_validator = new Validator(id_list);
			Validator id_product_validator = new Validator(id_product);
			if (id_list_validator.IDValidator() || id_product_validator.IDValidator()) {
				ListViewModel? list_view_model = list_repository.Remove(id_list, id_product);
				if (list_view_model != null) return Ok(list_view_model);
				else return NotFound();
			} else return BadRequest(string.Join(" ", id_list_validator.ListMessage) + " " + string.Join(" ", id_product_validator.ListMessage));
		}
		[HttpGet("{id_list:int}")]
		public IActionResult Clear(int id_list) { // Responding with a list after clearing all products.
			Validator id_list_validator = new Validator(id_list);
			if (id_list_validator.IDValidator()) return Ok(list_repository.Clear(id_list));
			else return BadRequest(string.Join(" ", id_list_validator.ListMessage));
		}
	}
}