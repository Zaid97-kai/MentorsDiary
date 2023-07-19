using MentorsDiary.Application.Entities.Divisions.Domains;
using MentorsDiary.Web.Data.Services.Bases;

namespace MentorsDiary.Web.Data.Services;

/// <summary>
/// Class DivisionService.
/// Implements the <see cref="MentorsDiary.Web.Services.Bases.BaseService{MentorsDiary.Application.Entities.Divisions.Domains.Division}" />
/// </summary>
/// <seealso cref="MentorsDiary.Web.Services.Bases.BaseService{MentorsDiary.Application.Entities.Divisions.Domains.Division}" />
public class DivisionService: BaseService<Division>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DivisionService"/> class.
    /// </summary>
    /// <param name="httpClient">The HTTP client.</param>
    public DivisionService(HttpClient? httpClient) : base(httpClient)
    {

    }
}