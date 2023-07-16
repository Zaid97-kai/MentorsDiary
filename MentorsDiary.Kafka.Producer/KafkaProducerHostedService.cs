using Confluent.Kafka;
using MentorsDiary.Application.Bases.Interfaces.IHaves;
using MentorsDiary.Application.Entities.Divisions.Domains;
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
    private readonly IProducer<string, string> _producer;

    private readonly HttpClient _httpClient;

    private string _serializeObject;

    private string _type;

    public KafkaProducerHostedService(ILogger<KafkaProducerHostedService> logger, HttpClient httpClient)
    {
        _logger = logger;
        _httpClient = httpClient;
        var config = new ProducerConfig()
        {
            BootstrapServers = "localhost:9092"
        };
        _producer = new ProducerBuilder<string, string>(config).Build();
    }

    public async Task InitializerProducer(string serializeObject, Type type, CancellationToken cancellationToken)
    {
        _serializeObject = serializeObject;
        _type = nameof(Division);

        await StartAsync(cancellationToken);
    }
    
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        if (_type == nameof(Division))
        {
            _logger.LogInformation(_serializeObject);
            var message = new Message<string, string>
            {
                Key = _type,
                Value = _serializeObject
            };
            await _producer.ProduceAsync("division", message, cancellationToken);

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