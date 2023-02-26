using System.ComponentModel.DataAnnotations.Schema;
namespace list_api.Models {
	public class UserViewModel {
		public int ID { get; set; }
		public string Name { get; set; } = null!;
	}
}