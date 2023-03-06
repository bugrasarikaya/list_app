namespace list_api.Common {
	public class ParamValidator {
		private bool result = true;
		private object parameter;
		public List<string> ListMessage { get; set; } = new List<string>();
		public ParamValidator(object param) { // Constructing.
			this.parameter = param;
		}
		public bool Validate() { // Validating given ID.
			if (parameter.GetType() == typeof(DateTime)) {
				if ((DateTime)parameter >= DateTime.Now) {
					ListMessage.Add("Date time must be before now.");
					result &= false;
				}
				return result;
			} else if (parameter.GetType() == typeof(int)) {
				if ((int)parameter <= 0) {
					ListMessage.Add("ID must be greater than 0.");
					result &= false;
				}
				return result;
			} else if (parameter.GetType() == typeof(string)) {
				if (int.TryParse((string)parameter, out int param_integer)) {
					if (param_integer <= 0) {
						ListMessage.Add("ID must be greater than 0.");
						result &= false;
					}
					return result;
				} else if (DateTime.TryParse((string)parameter, out DateTime param_datetime)) {
					if (param_datetime >= DateTime.Now) {
						ListMessage.Add("Date time must be before now.");
						result &= false;
					}
					return result;
				} else {
					if (string.IsNullOrEmpty((string)parameter)) {
						ListMessage.Add("Name cannot be empty.");
						result &= false;
					}
					if (((string)parameter).Count() > 100) {
						ListMessage.Add("Name must be at most 100 characters.");
						result &= false;
					}
				}
			}
			return result;
		}
	}
}