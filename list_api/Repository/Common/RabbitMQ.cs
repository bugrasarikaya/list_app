using RabbitMQ.Client;
namespace list_api.Repository.Common {
	public static class RabbitMQ  {
		public static void Send() {
			ConnectionFactory lol = new ConnectionFactory() { HostName = "localhost" };
		}
	}
}