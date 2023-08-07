using MentorsDiary.Application.Entities.ParentStudents.Domains;
using MentorsDiary.Web.Data.Services.Bases;

namespace MentorsDiary.Web.Data.Services;

/// <summary>
/// Class ParentStudentService.
/// Implements the <see cref="MentorsDiary.Web.Data.Services.Bases.BaseService{MentorsDiary.Application.Entities.ParentStudents.Domains.ParentStudent}" />
/// </summary>
/// <seealso cref="MentorsDiary.Web.Data.Services.Bases.BaseService{MentorsDiary.Application.Entities.ParentStudents.Domains.ParentStudent}" />
public class ParentStudentService: BaseService<ParentStudent>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ParentStudentService"/> class.
    /// </summary>
    /// <param name="clientFactory">The client factory.</param>
    public ParentStudentService(IHttpClientFactory clientFactory) : base(clientFactory)
    {
    }
}