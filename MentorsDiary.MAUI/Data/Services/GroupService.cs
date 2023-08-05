using MentorsDiary.Application.Entities.Groups.Domains;
using MentorsDiary.MAUI.Data.Services.Bases;

namespace MentorsDiary.MAUI.Data.Services;

/// <summary>
/// Class GroupService.
/// Implements the <see cref="MentorsDiary.MAUI.Data.Services.Bases.BaseService{MentorsDiary.Application.Entities.Groups.Domains.Group}" />
/// </summary>
/// <seealso cref="MentorsDiary.MAUI.Data.Services.Bases.BaseService{MentorsDiary.Application.Entities.Groups.Domains.Group}" />
public class GroupService : BaseService<Group>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GroupService"/> class.
    /// </summary>
    /// <param name="httpClient">The HTTP client.</param>
    public GroupService(HttpClient httpClient) : base(httpClient)
    {

    }
}