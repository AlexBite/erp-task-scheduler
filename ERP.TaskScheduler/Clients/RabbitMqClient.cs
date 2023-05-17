using System.Text;
using System.Text.Json;
using RabbitMQ.Client;

namespace ERP.TaskScheduler.Clients;

public interface IRabbitMqClient
{
    void SendMessage(object obj);
    void SendMessage(string message);
}

public class RabbitMqClient : IRabbitMqClient, IDisposable
{
    private readonly IConnection _connection;
    private readonly IModel _channel;

    public RabbitMqClient(ILogger<RabbitMqClient> logger)
    {
        var factory = new ConnectionFactory()
        {
            HostName = "localhost"
        };

        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
        _channel.ConfirmSelect();
        _channel.QueueDeclare(queue: "TasksQueue",
            durable: false,
            exclusive: false,
            autoDelete: false);
        
        _channel.BasicAcks += (sender, args) =>
        {
            logger.LogInformation("{Sender}, {@Args}", sender, args);
        };
    }

    public void SendMessage(object obj)
    {
        var message = JsonSerializer.Serialize(obj);
        SendMessage(message);
    }

    public void SendMessage(string message)
    {
        var body = Encoding.UTF8.GetBytes(message);

        var properties = _channel.CreateBasicProperties();
        properties.DeliveryMode = 2; // Persistent message
        properties.CorrelationId = 51434.ToString();
        _channel.BasicPublish(exchange: "",
            routingKey: "TasksQueue",
            basicProperties: properties,
            mandatory: true,
            body: body);
    }

    public void Dispose()
    {
        _connection.Dispose();
        _channel.Dispose();
    }
}