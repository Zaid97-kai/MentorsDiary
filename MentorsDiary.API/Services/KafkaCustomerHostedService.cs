using System.Text;
using Kafka.Public;
using Kafka.Public.Loggers;

namespace MentorsDiary.API.Services;

public class KafkaCustomerHostedService : IHostedService
{
    private readonly ILogger<KafkaCustomerHostedService> _logger;

    private readonly ClusterClient _cluster;

    public KafkaCustomerHostedService(ILogger<KafkaCustomerHostedService> logger)
    {
        _logger = logger;
        _cluster = new ClusterClient(new Configuration
        {
            Seeds = "localhost:9092"
        }, new ConsoleLogger());
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _cluster.ConsumeFromLatest("division");
        _cluster.MessageReceived += record =>
        {
            _logger.LogInformation($"Received: {Encoding.UTF8.GetString(record.Value as byte[] ?? Array.Empty<byte>())}");
        };
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _cluster.Dispose();
        return Task.CompletedTask;
    }
}