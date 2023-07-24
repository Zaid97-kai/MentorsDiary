using MentorsDiary.Application.Entities.Students.Domains;

namespace MentorsDiary.Application.Entities.GroupEvents.Domains;

/// <summary>
/// Class GroupEventStudent.
/// </summary>
public class GroupEventStudent
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
    public List<Student> Students { get; set; }
}