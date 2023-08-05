using MentorsDiary.Application.Entities.Students.Domains;
using MentorsDiary.MAUI.Data.Services.Bases;

namespace MentorsDiary.MAUI.Data.Services;

/// <summary>
/// Class StudentService.
/// Implements the <see cref="MentorsDiary.MAUI.Data.Services.Bases.BaseService{MentorsDiary.Application.Entities.Students.Domains.Student}" />
/// </summary>
/// <seealso cref="MentorsDiary.MAUI.Data.Services.Bases.BaseService{MentorsDiary.Application.Entities.Students.Domains.Student}" />
public class StudentService : BaseService<Student>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="StudentService"/> class.
    /// </summary>
    /// <param name="httpClient">The HTTP client.</param>
    public StudentService(HttpClient httpClient) : base(httpClient)
    {

    }
}