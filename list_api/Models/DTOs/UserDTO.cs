namespace list_api.Models.DTOs {
	public class UserDTO {
		public int IDRole { get; set; }
		public string Name { get; set; } = null!;
		public string Password { get; set; } = null!;
	}
}