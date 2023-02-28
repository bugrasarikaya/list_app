namespace list_api.Models.DTOs {
	public class ProductPatchDTO {
		public int IDCategory { get; set; }
		public string? Name { get; set; }
		public string? Description { get; set; }
		public double Price { get; set; }
	}
}