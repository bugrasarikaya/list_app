using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using list_api.Services;
namespace list_api.Controllers {
	[ApiController]
	[Authorize(Roles = "Admin")]
	[Route("api/[controller]")]
	public class MessageController : Controller {
		private readonly IMessageService messager;
		public MessageController(IMessageService messager) { // Constructing.
			this.messager = messager;
		}
		[HttpGet]
		public IActionResult List() { // Returning with received messages.
			return Ok(messager.Consume());
		}
	}
}