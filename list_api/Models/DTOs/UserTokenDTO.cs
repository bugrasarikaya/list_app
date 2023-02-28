namespace list_api.Models.DTOs {
	public class UserTokenDTO {
		public string Name { get; set; } = null!;
		public string Password { get; set; } = null!;
		public string? RefreshToken { get; set; }
		public DateTime RefreshTokenExpireDate { get; set; }
	}
}