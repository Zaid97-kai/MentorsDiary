using MentorsDiary.Application.Entities.GroupEvents.Domains;
using MentorsDiary.Web.Data.Services.Bases;

namespace MentorsDiary.Web.Data.Services;

/// <summary>
/// Class GroupEventService.
/// Implements the <see cref="GroupEvent" />
/// </summary>
/// <seealso cref="GroupEvent" />
public class GroupEventService: BaseService<GroupEvent>
{
    /// <summary>
    /// The HTTP client
    /// </summary>
    private readonly HttpClient _httpClient;

    /// <summary>
    /// Initializes a new instance of the <see cref="GroupEventService" /> class.
    /// </summary>
    /// <param name="httpClient">The HTTP client.</param>
    public GroupEventService(HttpClient? httpClient) : base(httpClient)
    {
        _httpClient = httpClient;
    }

    /// <summary>
    /// Adds the students in group event.
    /// </summary>
    /// <param name="groupEventStudent">The group event student.</param>
    /// <returns>HttpResponseMessage.</returns>
    public async Task<HttpResponseMessage> AddStudentsInGroupEvent(GroupEventStudent groupEventStudent)
    {
        var result = await _httpClient?.PostAsJsonAsync($"api/{BasePath}/AddStudentsInGroupEvent", groupEventStudent)!;
        return result;
    }
}