namespace list_api.Services {
	public interface IMessageService {
		void Publish(object message);
		public IEnumerable<string> Consume();
	}
}