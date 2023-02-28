namespace list_api.Exceptions {
	public class ConflictException : Exception {
		public ConflictException() { }
		public ConflictException(string message) : base(message) { }
		public ConflictException(string message, Exception inner_exception) : base(message, inner_exception) { }
	}
}