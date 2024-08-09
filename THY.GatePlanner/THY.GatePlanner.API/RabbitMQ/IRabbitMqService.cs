namespace THY.GatePlanner.API.RabbitMQ
{
    public interface IRabbitMqService
    {
        void SendMessage(string queueName, string message);
        void StartConsuming(string queueName, Action<string> onMessageReceived);
    }
}
