using System.ComponentModel.DataAnnotations.Schema;
namespace list_api.Models {
	public class Brand {
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ID { get; set; }
		public string Name { get; set; } = null!;
		public ICollection<Product>? Products { get; set; }
	}
}