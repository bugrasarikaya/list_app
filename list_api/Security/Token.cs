namespace list_api.Security {
	public class Token {
		public string AcessToken { get; set; } = null!;
		public string? RefreshToken { get; set; }
		public DateTime Expiration { get; set; }
	}
}