namespace list_api.Models.DTOs {
	public class ListClientPatchDTO {
		public int IDCategory { get; set; }
		public int IDStatus { get; set; }
		public string? Name { get; set; }
		public string? Description { get; set; }
	}
}