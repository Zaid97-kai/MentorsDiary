using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MentorsDiary.Kafka.Producer;

/// <summary>
/// Class KafkaProducerHostedService.
/// Implements the <see cref="IHostedService" />
/// </summary>
/// <seealso cref="IHostedService" />
public class KafkaProducerHostedService : IHostedService
{
    /// <summary>
    /// The logger
    /// </summary>
    private readonly ILogger<KafkaProducerHostedService> _logger;

    /// <summary>
    /// The producer
    /// </summary>
    private readonly IProducer<Null, string> _producer;

    /// <summary>
    /// Initializes a new instance of the <see cref="KafkaProducerHostedService"/> class.
    /// </summary>
    /// <param name="logger">The logger.</param>
    public KafkaProducerHostedService(ILogger<KafkaProducerHostedService> logger)
    {
        _logger = logger;
        var config = new ProducerConfig()
        {
            BootstrapServers = "localhost:9092"
        };
        _producer = new ProducerBuilder<Null, string>(config).Build();
    }

    /// <summary>
    /// Start as an asynchronous operation.
    /// </summary>
    /// <param name="cancellationToken">Indicates that the start process has been aborted.</param>
    /// <returns>A Task representing the asynchronous operation.</returns>
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        for (var i = 0; i < 100; i++)
        {
            var value = $"Hello {i}";
            _logger.LogInformation(value);
            var message = new Message<Null, string>
            {
                Value = value
            };
            await _producer.ProduceAsync("kafka-topic-name", message, cancellationToken);

            _producer.Flush(TimeSpan.FromSeconds(10));
        }
    }

    /// <summary>
    /// Triggered when the application host is performing a graceful shutdown.
    /// </summary>
    /// <param name="cancellationToken">Indicates that the shutdown process should no longer be graceful.</param>
    /// <returns>Task.</returns>
    public Task StopAsync(CancellationToken cancellationToken)
    {
        _producer.Dispose();
        return Task.CompletedTask;
    }
}