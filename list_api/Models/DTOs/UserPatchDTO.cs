namespace list_api.Models.DTOs {
	public class UserPatchDTO {
		public int IDRole { get; set; }
		public string? Name { get; set; }
		public string? Password { get; set; }
	}
}