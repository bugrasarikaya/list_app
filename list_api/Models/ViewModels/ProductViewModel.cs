namespace list_api.Models.ViewModels {
	public class ProductViewModel {
		public int ID { get; set; }
		public int IDCategory { get; set; }
		public string Name { get; set; } = null!;
		public string NameCategory { get; set; } = null!;
		public string? Description { get; set; }
		public double Price { get; set; }
		public double Quantity { get; set; }
		public double Cost { get; set; }
	}
}