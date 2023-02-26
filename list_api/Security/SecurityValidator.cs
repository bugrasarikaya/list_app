using System.Text.RegularExpressions;
namespace list_api.Security {
	public class SecurityValidator {
		private object parameter;
		public List<string> ListMessage { get; set; } = new List<string>();
		public SecurityValidator(object parameter) { // Constructing.
			this.parameter = parameter;
		}
		public  bool RefreshTokenValidator() { // Validating given refresh token.
			if (string.IsNullOrEmpty((string)parameter)) {
				ListMessage.Add("Refresh token cannot be empty.");
				return false;
			} else if (((string)parameter).Length < 36) {
				ListMessage.Add("Refresh token must have at least 36 characters.");
				return false;
			} else if (!new Regex(@"^[{]?[0-9a-fA-F]{8}-([0-9a-fA-F]{4}-){3}[0-9a-fA-F]{12}[}]?$").IsMatch((string)parameter)) {
				ListMessage.Add("Refresh token is not valid.");
				return false;
			} else {
				return true;
			}
		}
	}
}