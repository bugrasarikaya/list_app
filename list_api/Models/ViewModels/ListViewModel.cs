namespace list_api.Models.ViewModels {
	public class ListViewModel {
		public int ID { get; set; }
		public string Name { get; set; } = null!;
		public string? Description { get; set; }
		public string NameCategory { get; set; } = null!;
		public ICollection<ListProductViewModel> ListViewModels { get; set; } = null!;
		public string NameUser { get; set; } = null!;
		public DateTime? DateTimeCompleting { get; set; }
		public DateTime DateTimeCreating { get; set; }
		public DateTime DateTimeUpdating { get; set; }
		public double TotalCost { get; set; }
		public string NameStatus { get; set; } = null!;
	}
}