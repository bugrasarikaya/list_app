namespace list_api.Models.DTOs {
	public class ProductDTO {
		public int IDBrand { get; set; }
		public int IDCategory { get; set; }
		public string Name { get; set; } = null!;
		public string? Description { get; set; }
		public double Price { get; set; }
	}
}