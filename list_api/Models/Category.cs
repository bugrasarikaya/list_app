using System.ComponentModel.DataAnnotations.Schema;
namespace list_api.Models {
	public class Category {
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ID { get; set; }
		public string Name { get; set; } = null!;
		public ICollection<List>? Lists { get; set; }
		public ICollection<Product>? Products { get; set; }
	}
}