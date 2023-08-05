using MentorsDiary.Application.Entities.GroupEvents.Domains;
using MentorsDiary.MAUI.Data.Services.Bases;

namespace MentorsDiary.MAUI.Data.Services;

/// <summary>
/// Class GroupEventService.
/// Implements the <see cref="MentorsDiary.MAUI.Data.Services.Bases.BaseService{MentorsDiary.Application.Entities.GroupEvents.Domains.GroupEvent}" />
/// </summary>
/// <seealso cref="MentorsDiary.MAUI.Data.Services.Bases.BaseService{MentorsDiary.Application.Entities.GroupEvents.Domains.GroupEvent}" />
public class GroupEventService: BaseService<GroupEvent>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GroupEventService"/> class.
    /// </summary>
    /// <param name="httpClient">The HTTP client.</param>
    public GroupEventService(HttpClient httpClient) : base(httpClient)
    {
    }
}