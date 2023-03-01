using list_api.Models.DTOs;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
namespace list_api.Security {
	public class TokenHandler {
		public IConfiguration configuration { get; set; }
		public TokenHandler(IConfiguration configuration) { // Constructing.
			this.configuration = configuration;
		}
		public Token CreateAccsessToken(UserTokenDTO user_token_dto) { // Creating access and refresh tokens.
			Token token_model = new Token();
			SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Token:SecurityKey"]!));
			SigningCredentials signing_credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
			token_model.Expiration = DateTime.Now.AddMinutes(30);
			JwtSecurityToken security_token = new JwtSecurityToken(issuer: configuration["Token:Issuer"], audience: configuration["Token:Audience"], claims: new List<Claim> { new Claim(ClaimTypes.NameIdentifier, user_token_dto.ID.ToString()), new Claim(ClaimTypes.Role, user_token_dto.NameRole) }, expires: token_model.Expiration, notBefore: DateTime.Now, signingCredentials: signing_credentials);
			JwtSecurityTokenHandler token_handler = new JwtSecurityTokenHandler();
			token_model.AcessToken = token_handler.WriteToken(security_token);
			token_model.RefreshToken = Guid.NewGuid().ToString();
			return token_model;
		}
	}
}