namespace list_api.Models.DTOs {
	public class ClientListDTO {
		public int IDCategory { get; set; }
		public int IDStatus { get; set; }
		public string Name { get; set; } = null!;
		public string? Description { get; set; }
	}
}