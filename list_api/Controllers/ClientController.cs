using list_api.Common;
using list_api.Models;
using list_api.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
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
		[HttpPost("list")]
		public IActionResult CreateList([FromBody] List list) { // Responding with a created list after creating for accessed user.
			if (ModelState.IsValid) {
				List? list_created = client_repository.CreateList(list);
				return Created("api/List/" + list_created.ID, list_created);
			} else return BadRequest(string.Join(" ", ModelState.Values.SelectMany(mse => mse.Errors).Select(me => me.ErrorMessage)));
		}
		[HttpDelete("list/{id_list:int}")]
		public IActionResult DeleteList(int id_list) { // Responding with no content after deleting for accessed user.
			Validator id_list_validator = new Validator(id_list);
			if (id_list_validator.IDValidator()) {
				List? list_deleted = client_repository.Deletelist(id_list);
				if (list_deleted != null) return NoContent();
				else return NotFound();
			} else return BadRequest(string.Join(" ", id_list_validator.ListMessage));
		}
		[HttpGet("list/{id_list:int}")]
		public IActionResult GetList(int id_list) { // Responding with a list after getting for accessed user.
			Validator id_list_validator = new Validator(id_list);
			if (id_list_validator.IDValidator()) {
				ListViewModel? list = client_repository.GetList(id_list);
				if (list != null) return Ok(list);
				else return NotFound();
			} else return BadRequest(string.Join(" ", id_list_validator.ListMessage));
		}
		[HttpGet("list")]
		public IActionResult ListLists() { // Responding with list list for accessed user.
			return Ok(client_repository.ListLists());
		}
		[HttpGet("list")]
		public IActionResult ListListsByCategory(int id_category) { // Responding with list list by category for accessed user.
			Validator id_category_validator = new Validator(id_category);
			if (id_category_validator.IDValidator())
				return Ok(client_repository.ListListsByCategory(id_category));
			else return BadRequest(string.Join(" ", id_category_validator.ListMessage));
		}
		[HttpPut("list/{id_list:int}")]
		public IActionResult UpdateList(int id_list, [FromBody] List list) { // Responding with an updated list for accessed user.
			Validator id_list_validator = new Validator(id_list);
			if (id_list_validator.IDValidator() || ModelState.IsValid) return Ok(client_repository.UpdateList(id_list, list));
			else return BadRequest(string.Join(" ", id_list_validator.ListMessage) + " " + string.Join(" ", ModelState.Values.SelectMany(mse => mse.Errors).Select(me => me.ErrorMessage)));
		}
		[HttpGet("{id_list:int}")]
		public IActionResult SetCompleted(int id_list) { // Responding with a list after setting to "Completed" for accessed user.
			Validator id_list_validator = new Validator(id_list);
			if (id_list_validator.IDValidator() || ModelState.IsValid) return Ok(client_repository.SetCompleted(id_list));
			else return BadRequest(string.Join(" ", id_list_validator.ListMessage) + " " + string.Join(" ", ModelState.Values.SelectMany(mse => mse.Errors).Select(me => me.ErrorMessage)));
		}
		//[HttpGet("listproduct/{id_list:int}")]
		//public IActionResult ListListProducts(int id_list) { // Responding with product list in a specific list after getting for accessed user.
		//	ICollection<Product>? collection_product = client_repository.ListListProducts(id_list);
		//	if (collection_product != null) return Ok(client_repository.ListListProducts(id_list));
		//	else return NotFound();
		//}
		[HttpPost("listproduct")]
		public IActionResult AddProduct([FromBody] ListProduct list_product) { // Responding with list product after adding a product to a list for accessed user.
			if (ModelState.IsValid) return Ok(client_repository.AddProduct(list_product));
			else return BadRequest(string.Join(" ", ModelState.Values.SelectMany(mse => mse.Errors).Select(me => me.ErrorMessage)));
		}
		[HttpDelete("listproduct/{id_list:int}/{id_product:int}")]
		public IActionResult RemoveProduct(int id_list, int id_product) { // Responding with list after removing a product from a list for accessed user.
			Validator id_list_validator = new Validator(id_list);
			if (id_list_validator.IDValidator() || ModelState.IsValid) {
				ListViewModel? list_added_product = client_repository.RemoveProduct(id_list, id_product);
				if (list_added_product != null) return Ok(list_added_product);
				else return NotFound();
			} else return BadRequest(string.Join(" ", id_list_validator.ListMessage) + " " + string.Join(" ", ModelState.Values.SelectMany(mse => mse.Errors).Select(me => me.ErrorMessage)));
		}
		[HttpGet("{id_list:int}")]
		public IActionResult Clear(int id_list) { // Responding with a list after clearing all products.
			Validator id_list_validator = new Validator(id_list);
			if (id_list_validator.IDValidator()) {
				return Ok(client_repository.Clear(id_list));
			} else return BadRequest(string.Join(" ", id_list_validator.ListMessage));
		}
		[HttpGet("{user}")]
		public IActionResult GetUser() { // Responding with a user after getting for accessed user.
			return Ok(client_repository.GetUser());
		}
		[HttpPut("{user}")]
		public IActionResult UpdateUser([FromBody] User user) { // Responding with a user after updating for accessed user.
			if (ModelState.IsValid) return Ok(client_repository.UpdateUser(user));
			else return BadRequest(string.Join(" ", ModelState.Values.SelectMany(mse => mse.Errors).Select(me => me.ErrorMessage)));
		}
	}
}