using System.Net.Http.Json;
using MentorsDiary.Application.Entities.GroupEventStudents.Domains;
using MentorsDiary.MAUI.Data.Services.Bases;

namespace MentorsDiary.MAUI.Data.Services;

/// <summary>
/// Class GroupEventStudentService.
/// Implements the <see cref="MentorsDiary.MAUI.Data.Services.Bases.BaseService{MentorsDiary.Application.Entities.GroupEventStudents.Domains.GroupEventStudent}" />
/// </summary>
/// <seealso cref="MentorsDiary.MAUI.Data.Services.Bases.BaseService{MentorsDiary.Application.Entities.GroupEventStudents.Domains.GroupEventStudent}" />
public class GroupEventStudentService : BaseService<GroupEventStudent>
{
    /// <summary>
    /// The HTTP client
    /// </summary>
    private readonly HttpClient _httpClient;

    /// <summary>
    /// Initializes a new instance of the <see cref="GroupEventStudentService" /> class.
    /// </summary>
    /// <param name="httpClient">The HTTP client.</param>
    public GroupEventStudentService(HttpClient httpClient) : base(httpClient)
    {
        _httpClient = httpClient;
    }

    /// <summary>
    /// Adds the students in group event.
    /// </summary>
    /// <param name="groupEventStudents">The group event students.</param>
    /// <returns>HttpResponseMessage.</returns>
    public async Task<HttpResponseMessage> AddStudentsInGroupEvent(List<GroupEventStudent> groupEventStudents)
    {
        var result = await _httpClient?.PostAsJsonAsync($"api/{BasePath}/AddStudentsInGroupEvent", groupEventStudents)!;
        return result;
    }
}