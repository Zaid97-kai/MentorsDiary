using Confluent.Kafka;
using System.Text;

namespace MentorsDiary.Kafka.Lib;

public sealed class MessageBus : IDisposable
{
    //private ProducerBuilder<Null, string> _producerBuilder;

    //private readonly IProducer<Null, string> _producer;
    //private IConsumer<Null, string> _consumer;

    //private readonly ProducerConfig _producerConfig;
    //private readonly IDictionary<string, object> _consumerConfig;

    //public MessageBus() : this("localhost") { }

    //public MessageBus(string host)
    //{
    //    _producerConfig = new ProducerConfig
    //    {
    //        BootstrapServers = "bootstrap.servers",
    //    };

    //    _consumerConfig = new Dictionary<string, object>
    //    {
    //        { "group.id", "custom-group"},
    //        { "bootstrap.servers", host }
    //    };

    //    _producerBuilder = new ProducerBuilder<Null, string>(_producerConfig);
    //    _producer = _producerBuilder.Build();

    //    var result = _producer.ProduceAsync("weblog", new Message<Null, string> { Value = "a log message" }).Result;
    //    //_producer = new Producer<Null, string>(_producerConfig, null, new StringSerializer(Encoding.UTF8));
    //}

    //public void SendMessage(string topic, string message)
    //{
    //    _producer.ProduceAsync(topic, null, message);
    //}

    //public void SubscribeOnTopic<T>(string topic, Action<T> action, CancellationToken cancellationToken) where T : class
    //{
    //    var msgBus = new MessageBus();
    //    using (msgBus._consumer = new Consumer<Null, string>(_consumerConfig, null, new StringDeserializer(Encoding.UTF8)))
    //    {
    //        msgBus._consumer.Assign(new List<TopicPartitionOffset> { new TopicPartitionOffset(topic, 0, -1) });

    //        while (true)
    //        {
    //            if (cancellationToken.IsCancellationRequested)
    //                break;

    //            Message<Null, string> msg;
    //            if (msgBus._consumer.Consume(out msg, TimeSpan.FromMilliseconds(10)))
    //            {
    //                action(msg.Value as T);
    //            }
    //        }
    //    }
    //}

    //public void Dispose()
    //{
    //    _producer?.Dispose();
    //    _consumer?.Dispose();
    //}
}