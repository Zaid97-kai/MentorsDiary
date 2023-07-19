using MentorsDiary.Application.Entities.Events.Domains;
using MentorsDiary.Web.Services.Bases;

namespace MentorsDiary.Web.Services;

/// <summary>
/// Class EventService.
/// Implements the <see cref="MentorsDiary.Web.Services.Bases.BaseService{MentorsDiary.Application.Entities.Events.Domains.Event}" />
/// </summary>
/// <seealso cref="MentorsDiary.Web.Services.Bases.BaseService{MentorsDiary.Application.Entities.Events.Domains.Event}" />
public class EventService: BaseService<Event>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EventService"/> class.
    /// </summary>
    /// <param name="httpClient">The HTTP client.</param>
    public EventService(HttpClient? httpClient) : base(httpClient)
    {

    }
}