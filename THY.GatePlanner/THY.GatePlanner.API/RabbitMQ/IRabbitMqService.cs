namespace THY.GatePlanner.API.RabbitMQ
{
    public interface IRabbitMqService
    {
        void SendMessage(string queueName, string message);
        void StartConsuming(string queueName, Action<string> onMessageReceived);
        string BasicGet(string queueuName, out ulong deliveryTag);
        void Acknowledge(ulong deliveryTag);
        void Reject(ulong deliveryTag, bool requeue = true);

    }
}
