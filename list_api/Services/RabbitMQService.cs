using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
namespace list_api.Services {
	public class RabbitMQService : IMessageService {
		private readonly RabbitMQConfiguration rabbitmq_configuration;
		public RabbitMQService(IOptions<RabbitMQConfiguration> options) { // Constructing.
			rabbitmq_configuration = options.Value;
		}
		public void Publish(object message) { // Sending a message.
			ConnectionFactory connection_factory = new ConnectionFactory() { HostName = rabbitmq_configuration.HostName, UserName = rabbitmq_configuration.Username, Password = rabbitmq_configuration.Password };
			IConnection connection = connection_factory.CreateConnection();
			IModel channel = connection.CreateModel();
			channel.QueueDeclare(queue: rabbitmq_configuration.QueueName, durable: true, exclusive: false, autoDelete: false);
			channel.BasicPublish(exchange: string.Empty, routingKey: rabbitmq_configuration.QueueName, body: Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message)));
		}
		public IEnumerable<string> Consume() { // Receiving messages.
			ConnectionFactory connection_factory = new ConnectionFactory() { HostName = rabbitmq_configuration.HostName, UserName = rabbitmq_configuration.Username, Password = rabbitmq_configuration.Password };
			IConnection connection = connection_factory.CreateConnection();
			IModel channel = connection.CreateModel();
			channel.QueueDeclare(queue: rabbitmq_configuration.QueueName, durable: true, exclusive: false, autoDelete: false);
			EventingBasicConsumer consumer = new EventingBasicConsumer(channel);
			List<string> list_messages = new List<string>();
			consumer.Received += (model, event_args) => { list_messages.Add(Encoding.UTF8.GetString(event_args.Body.ToArray())); };
			foreach (string list in list_messages) yield return list;
		}
	}
}