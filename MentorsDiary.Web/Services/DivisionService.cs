using MentorsDiary.Application.Entities.Divisions.Domains;
using MentorsDiary.Kafka.Producer;
using Newtonsoft.Json;

namespace MentorsDiary.Web.Services;

public class DivisionService
{
    private readonly KafkaProducerHostedService _service;

    public DivisionService(KafkaProducerHostedService service)
    {
        _service = service;
    }

    public async Task CreateDivision(Division division, CancellationToken cancellationToken = new())
    {
        var serializeObject = JsonConvert.SerializeObject(division);
        await _service.InitializerProducer(serializeObject, typeof(Division), cancellationToken);
    } 
}