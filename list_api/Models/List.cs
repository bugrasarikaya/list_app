using System.ComponentModel.DataAnnotations.Schema;
namespace list_api.Models {
	public class List {
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ID { get; set; }
		public string Name { get; set; } = null!;
		public string? Description { get; set; }
		public int CategoryID { get; set; }
		public int UserID { get; set; }
		public DateTime DateTime { get; set; }
		public double Cost { get; set; }
	}
}