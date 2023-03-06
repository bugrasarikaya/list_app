using System.ComponentModel.DataAnnotations.Schema;
namespace list_api.Models {
	public class ListProduct {
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ID { get; set; }
		public int IDList { get; set; }
		public int IDProduct { get; set; }
		public int Quantity { get; set; }
		public int Cost { get; set; }
		public List? List { get; set; }
		public Product? Product { get; set; }
	}
}