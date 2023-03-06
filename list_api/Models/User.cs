using System.ComponentModel.DataAnnotations.Schema;
namespace list_api.Models {
	public class User {
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ID { get; set; }
		public int IDRole { get; set; }
		public string Name { get; set; } = null!;
		public string Password { get; set; } = null!;
		public string? RefreshToken { get; set; }
		public DateTime? RefreshTokenExpireDate { get; set; }
		public ICollection<List>? Lists { get; set; }
		public Role? Role { get; set; }
	}
}