namespace list_api.Common {
	public class Validator {
		private bool result = true;
		private object param;
		public List<string> ListMessage { get; set; } = new List<string>();
		public Validator(object param) { // Constructing.
			this.param = param;
		}
		public bool Validate() { // Validating given ID.
			if (typeof(object) == typeof(int)) {
				if ((int)param <= 0) {
					ListMessage.Add("ID must be greater than 0.");
					result &= false;
				}
				return result;
			} else if (typeof(object) == typeof(DateTime)) {
				if ((DateTime)param < DateTime.Now) {
					ListMessage.Add("Date time must be before now.");
					result &= false;
				}
				return result;
			} else {
				if (string.IsNullOrEmpty((string)param)) {
					ListMessage.Add("Name cannot be empty.");
					result &= false;
				}
				if (((string)param).Count() > 100) {
					ListMessage.Add("Name must be at most 100 characters.");
					result &= false;
				}
				return result;
			}
		}
	}
}