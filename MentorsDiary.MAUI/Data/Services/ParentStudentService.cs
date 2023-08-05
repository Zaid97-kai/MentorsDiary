using MentorsDiary.Application.Entities.ParentStudents.Domains;
using MentorsDiary.MAUI.Data.Services.Bases;

namespace MentorsDiary.MAUI.Data.Services;

/// <summary>
/// Class ParentStudentService.
/// Implements the <see cref="MentorsDiary.MAUI.Data.Services.Bases.BaseService{MentorsDiary.Application.Entities.ParentStudents.Domains.ParentStudent}" />
/// </summary>
/// <seealso cref="MentorsDiary.MAUI.Data.Services.Bases.BaseService{MentorsDiary.Application.Entities.ParentStudents.Domains.ParentStudent}" />
public class ParentStudentService : BaseService<ParentStudent>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ParentStudentService"/> class.
    /// </summary>
    /// <param name="httpClient">The HTTP client.</param>
    public ParentStudentService(HttpClient httpClient) : base(httpClient)
    {
    }
}