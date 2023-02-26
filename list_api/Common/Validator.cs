namespace list_api.Common {
	public class Validator {
		private object parameter;
		public List<string> ListMessage { get; set; } = new List<string>();
		public Validator(object parameter) { // Constructing.
			this.parameter = parameter;
		}
		public bool IDValidator() { // Validating given ID.
			if ((int)parameter <= 0) {
				ListMessage.Add("ID must be greater than 0.");
				return false;
			} else return true;
		}
	}
}