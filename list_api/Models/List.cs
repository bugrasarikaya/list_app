using System.ComponentModel.DataAnnotations.Schema;
namespace list_api.Models {
	public class List {
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ID { get; set; }
		public int IDCategory { get; set; }
		public int IDStatus { get; set; }
		public int IDUser { get; set; }
		public string Name { get; set; } = null!;
		public string? Description { get; set; }
		public DateTime? DateTimeCompleting { get; set; }
		public DateTime DateTimeUpdating { get; set; }
		public DateTime DateTimeCreating { get; set; }
		public double TotalCost { get; set; }
		public Category? Category { get; set; }
		public ICollection<ListProduct>? ListProducts { get; set; }
		public Status? Status { get; set; }
		public User? User { get; set; }
	}
}