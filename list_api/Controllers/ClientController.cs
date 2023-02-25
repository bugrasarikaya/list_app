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
			} else return BadRequest(ModelState);
		}
		[HttpDelete("list/{id_list:int}")]
		public IActionResult Delete(int id_list) { // Responding with no content after deleting for accessed user.
			if (ModelState.IsValid) {
				List? list_deleted = client_repository.Deletelist(id_list);
				if (list_deleted != null) return NoContent();
				else return NotFound();
			} else return BadRequest(ModelState);
		}
		[HttpGet("list/{id_list:int}")]
		public IActionResult Get(int id_list) { // Responding with a list after getting for accessed user.
			if (ModelState.IsValid) {
				List? list = client_repository.GetList(id_list);
				if (list != null) return Ok(list);
				else return NotFound();
			} else return BadRequest(ModelState);
		}
		[HttpGet("list")]
		public IActionResult ListLists() { // Responding with list list after getting for accessed user.
			return Ok(client_repository.ListLists());
		}
		[HttpPut("list/{id_list:int}")]
		public IActionResult UpdateList(int id_list, [FromBody] List list) { // Responding with an updated list after updating for accessed user.
			if (ModelState.IsValid) {
				List? list_updated = client_repository.UpdateList(id_list, list);
				if (list_updated != null) return Ok(list_updated);
				else return NotFound();
			} else return BadRequest(ModelState);
		}
		[HttpGet("listproduct/{id_list:int}")]
		public IActionResult ListListProducts(int id_list) { // Responding with product list in a specific list after getting for accessed user.
			ICollection<Product>? collection_product = client_repository.ListListProducts(id_list);
			if (collection_product != null) return Ok(client_repository.ListListProducts(id_list));
			else return NotFound();
		}
		[HttpPost("listproduct")]
		public IActionResult AddProduct([FromBody] ListProduct list_product) { // Responding with list product after adding to a list for accessed user.
			if (ModelState.IsValid) {
				ListProduct? list_product_created = client_repository.AddProduct(list_product);
				return Created("api/ListProduct/" + list_product_created.ID, list_product_created);
			} else return BadRequest(ModelState);
		}
		[HttpDelete("listproduct/{id_list:int}/{id_product:int}")]
		public IActionResult RemoveProduct(int id_list, int id_product) { // Responding with no content after removing from a list for accessed user.
			if (ModelState.IsValid) {
				ListProduct? list_product_deleted = client_repository.RemoveProduct(id_list, id_product);
				if (list_product_deleted != null) return NoContent();
				else return NotFound();
			} else return BadRequest(ModelState);
		}
		[HttpGet("{user}")]
		public IActionResult GetUser() { // Responding with a user after getting for accessed user.
			User? user = client_repository.GetUser();
			if (user != null) return Ok(user);
			else return NotFound();
		}
		[HttpPut("{user}")]
		public IActionResult UpdateUser([FromBody] User user) { // Responding with a user after updating for accessed user.
			if (ModelState.IsValid) {
				User? user_updated = client_repository.UpdateUser(user);
				if (user_updated != null) return Ok(user_updated);
				else return NotFound();
			} else return BadRequest(ModelState);
		}
	}
}