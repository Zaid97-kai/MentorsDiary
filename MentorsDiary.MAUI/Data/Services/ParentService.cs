using MentorsDiary.Application.Entities.Parents.Domains;
using MentorsDiary.MAUI.Data.Services.Bases;

namespace MentorsDiary.MAUI.Data.Services;

/// <summary>
/// Class ParentService.
/// Implements the <see cref="MentorsDiary.MAUI.Data.Services.Bases.BaseService{MentorsDiary.Application.Entities.Parents.Domains.Parent}" />
/// </summary>
/// <seealso cref="MentorsDiary.MAUI.Data.Services.Bases.BaseService{MentorsDiary.Application.Entities.Parents.Domains.Parent}" />
public class ParentService : BaseService<Parent>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ParentService"/> class.
    /// </summary>
    /// <param name="httpClient">The HTTP client.</param>
    public ParentService(HttpClient httpClient) : base(httpClient)
    {

    }
}