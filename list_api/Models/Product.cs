using System.ComponentModel.DataAnnotations.Schema;
namespace list_api.Models {
	public class Product {
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ID { get; set; }
		public int IDCategory { get; set; }
		public string Name { get; set; } = null!;
		public string? Description { get; set; }
		public double Price { get; set; }
	}
}