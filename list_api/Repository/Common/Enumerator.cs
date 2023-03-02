namespace list_api.Repository.Common {
	public class Enumerator {
		public enum Role { // Enumerating for user roles.
			Admin = 1,
			User = 2
		}
		public enum Status { // Enumerating for list statuses.
			Completed = 1,
			Uncompleted = 2
		}
	}
}