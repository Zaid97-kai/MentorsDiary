using MentorsDiary.Application.Entities.Curators.Domains;
using MentorsDiary.MAUI.Data.Services.Bases;

namespace MentorsDiary.MAUI.Data.Services;

/// <summary>
/// Class CuratorService.
/// Implements the <see cref="MentorsDiary.MAUI.Data.Services.Bases.BaseService{MentorsDiary.Application.Entities.Curators.Domains.Curator}" />
/// </summary>
/// <seealso cref="MentorsDiary.MAUI.Data.Services.Bases.BaseService{MentorsDiary.Application.Entities.Curators.Domains.Curator}" />
public class CuratorService: BaseService<Curator>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CuratorService"/> class.
    /// </summary>
    /// <param name="httpClient">The HTTP client.</param>
    public CuratorService(HttpClient httpClient) : base(httpClient)
    {

    }
}