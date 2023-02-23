using list_api.Models;
using list_api.Security.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
namespace list_api.Security {
	public class TokenHandler {
		public IConfiguration configuration { get; set; }
		public TokenHandler(IConfiguration configuration) {
			this.configuration = configuration;
		}
		public Token CreateAccsessToken(User user) {
			Token token_model = new Token();
			SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Token: SecurityKey"]!));
			SigningCredentials signing_credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
			token_model.Expiration = DateTime.Now.AddMinutes(15);
			JwtSecurityToken security_token = new JwtSecurityToken(issuer: configuration["Token: Issuer"], audience: configuration["Token: Audience"], expires: token_model.Expiration, notBefore: DateTime.Now, signingCredentials: signing_credentials);
			JwtSecurityTokenHandler token_handler = new JwtSecurityTokenHandler();
			token_model.AcessToken = token_handler.WriteToken(security_token);
			token_model.RefreshToken = CreateRefreshToken();
			return token_model;
		}
		public string CreateRefreshToken() {
			return Guid.NewGuid().ToString();
		}
	}
}