using System.ComponentModel.DataAnnotations.Schema;
namespace list_api.Models {
	public class ListProduct {
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ID { get; set; }
		public int ListID { get; set; }
		public int ProductID { get; set; }
		public int Quantity { get; set; }
	}
}