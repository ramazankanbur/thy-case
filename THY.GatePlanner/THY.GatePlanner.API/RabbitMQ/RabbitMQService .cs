using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Threading.Channels;

namespace THY.GatePlanner.API.RabbitMQ
{
    public class RabbitMQService : IRabbitMqService, IDisposable
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public RabbitMQService(string hostname)
        {
            var factory = new ConnectionFactory() { HostName = hostname };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
        }

        public void SendMessage(string queueName, string message)
        {
            // Kuyruğu burada tanımlıyoruz, böylece dinamik olarak kullanılabilir
            _channel.QueueDeclare(queue: queueName,
                                  durable: false,
                                  exclusive: false,
                                  autoDelete: false,
                                  arguments: null);

            var body = Encoding.UTF8.GetBytes(message);
            _channel.BasicPublish(exchange: "",
                                  routingKey: queueName,
                                  basicProperties: null,
                                  body: body);
            Console.WriteLine(" [x] Sent {0} to queue {1}", message, queueName);
        }

        public void StartConsuming(string queueName, Action<string> onMessageReceived)
        {
            // Kuyruğu burada da tanımlıyoruz, böylece dinamik olarak kullanılabilir
            _channel.QueueDeclare(queue: queueName,
                                  durable: false,
                                  exclusive: false,
                                  autoDelete: false,
                                  arguments: null);

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                onMessageReceived(message);
            };

            _channel.BasicConsume(queue: queueName,
                                  autoAck: true,
                                  consumer: consumer);

            Console.WriteLine(" [x] Started consuming from queue {0}", queueName);
        }

        public void Dispose()
        {
            _channel?.Close();
            _connection?.Close();
        }
    }
}


