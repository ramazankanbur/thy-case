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

        public RabbitMQService(string hostname = "localhost", string username = "guest", string password = "guest", int port = 5672)
        {
                var factory = new ConnectionFactory() {HostName = hostname, UserName= username, Password = password, Port= 5672 };

                _connection = factory.CreateConnection();
                _channel = _connection.CreateModel();
           
         }

        public void SendMessage(string queueName, string message)
        {
            _channel.QueueDeclare(queue: queueName,
                                  durable: true,
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

        public string BasicGet(string queueName, out ulong deliveryTag)
        {

            _channel.QueueDeclare(
              queue: queueName,
              durable: true,
              exclusive: false,
              autoDelete: false,
              arguments: null);

            var result = _channel.BasicGet(queue: queueName, autoAck: false);
 
            if (result == null)
            {

                Console.WriteLine(" [x] No message received.");
                deliveryTag = 0;
                return null;
            }

            var body = result.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);

            deliveryTag = result.DeliveryTag;

            return message;
        }
        public void Acknowledge(ulong deliveryTag)
        {
            _channel.BasicAck(deliveryTag: deliveryTag, multiple: false);
        }

        public void Reject(ulong deliveryTag, bool requeue = true)
        {
            _channel.BasicNack(deliveryTag: deliveryTag, multiple: false, requeue: requeue);
        }


        public void StartConsuming(string queueName, Action<string> onMessageReceived)
        {
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


