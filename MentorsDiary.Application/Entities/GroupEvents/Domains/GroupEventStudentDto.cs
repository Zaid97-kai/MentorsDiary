using MentorsDiary.Application.Entities.GroupEventStudents.Domains;
using MentorsDiary.Application.Entities.Students.Domains;

namespace MentorsDiary.Application.Entities.GroupEvents.Domains;

/// <summary>
/// Class GroupEventStudentDto.
/// </summary>
public class GroupEventStudentDto
{
    /// <summary>
    /// Gets or sets the identifier.
    /// </summary>
    /// <value>The identifier.</value>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the students.
    /// </summary>
    /// <value>The students.</value>
    public List<GroupEventStudent> Students { get; set; }
}