using MentorsDiary.Application.Entities.Students.Domains;
using MentorsDiary.Web.Services.Bases;

namespace MentorsDiary.Web.Services;

/// <summary>
/// Class StudentService.
/// Implements the <see cref="MentorsDiary.Web.Services.Bases.BaseService{MentorsDiary.Application.Entities.Students.Domains.Student}" />
/// </summary>
/// <seealso cref="MentorsDiary.Web.Services.Bases.BaseService{MentorsDiary.Application.Entities.Students.Domains.Student}" />
public class StudentService: BaseService<Student>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="StudentService"/> class.
    /// </summary>
    /// <param name="httpClient">The HTTP client.</param>
    public StudentService(HttpClient? httpClient) : base(httpClient)
    {

    }
}