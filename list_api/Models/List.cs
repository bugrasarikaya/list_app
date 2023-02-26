using System.ComponentModel.DataAnnotations.Schema;
namespace list_api.Models {
	public class List {
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ID { get; set; }
		public int IDCategory { get; set; }
		public int IDUser { get; set; }
		public string Name { get; set; } = null!;
		public string? Description { get; set; }
		public DateTime DateTime { get; set; } = DateTime.Now;
		public double TotalCost { get; set; }
		public string Status { get; set; } = "Uncompleted";
	}
}