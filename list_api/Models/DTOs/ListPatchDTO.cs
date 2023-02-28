namespace list_api.Models.DTOs {
	public class ListPatchDTO {
		public int IDCategory { get; set; }
		public int IDStatus { get; set; }
		public int IDUser { get; set; }
		public string? Name { get; set; }
		public string? Description { get; set; }
	}
}