namespace list_api.Models {
	public class ListViewModel {
		public int ID { get; set; }
		public string Name { get; set; } = null!;
		public string? Description { get; set; }
		public Category Category { get; set; } = null!;
		public ICollection<ProductViewModel> Products { get; set; } = null!;
		public UserViewModel User { get; set; } = null!;
		public DateTime DateTime { get; set; }
		public double TotalCost { get; set; }
		public string Status { get; set; } = null!;
	}
}