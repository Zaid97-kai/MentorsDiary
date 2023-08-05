using MentorsDiary.Application.Entities.Divisions.Domains;
using MentorsDiary.MAUI.Data.Services.Bases;

namespace MentorsDiary.MAUI.Data.Services;

/// <summary>
/// Class DivisionService.
/// Implements the <see cref="MentorsDiary.MAUI.Data.Services.Bases.BaseService{MentorsDiary.Application.Entities.Divisions.Domains.Division}" />
/// </summary>
/// <seealso cref="MentorsDiary.MAUI.Data.Services.Bases.BaseService{MentorsDiary.Application.Entities.Divisions.Domains.Division}" />
public class DivisionService: BaseService<Division>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DivisionService"/> class.
    /// </summary>
    /// <param name="httpClient">The HTTP client.</param>
    public DivisionService(HttpClient httpClient) : base(httpClient)
    {

    }
}