namespace list_api.Exceptions {
	public class ForbiddenException : Exception {
		public ForbiddenException() { }
		public ForbiddenException(string message) : base(message) { }
		public ForbiddenException(string message, Exception inner_exception) : base(message, inner_exception) { }
	}
}