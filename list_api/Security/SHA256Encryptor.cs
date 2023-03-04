using System.Security.Cryptography;
using System.Text;
namespace list_api.Security {
	public class SHA256Encryptor : IEncryptor {
		public string Encrpyt(string data) { // Encrypting.
			return string.Join(string.Empty, SHA256.HashData(Encoding.UTF8.GetBytes(data)).Select(x => x.ToString("x2")));
		}
	}
}