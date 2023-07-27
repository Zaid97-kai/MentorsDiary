using System.ComponentModel.DataAnnotations.Schema;
using MentorsDiary.Application.Bases.Interfaces.IHaves;
using MentorsDiary.Application.Entities.GroupEvents.Domains;
using MentorsDiary.Application.Entities.Students.Domains;

namespace MentorsDiary.Application.Entities.GroupEventStudents.Domains;

/// <summary>
/// Class GroupEventStudent.
/// </summary>
public class GroupEventStudent : IHaveId, IHaveName
{
    /// <summary>
    /// Gets or sets the identifier.
    /// </summary>
    /// <value>The identifier.</value>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the group event identifier.
    /// </summary>
    /// <value>The group event identifier.</value>
    public int GroupEventId { get; set; }

    /// <summary>
    /// Gets or sets the group event.
    /// </summary>
    /// <value>The group event.</value>
    public virtual GroupEvent? GroupEvent { get; set; }

    /// <summary>
    /// Gets or sets the student identifier.
    /// </summary>
    /// <value>The student identifier.</value>
    public int StudentId { get; set; }

    /// <summary>
    /// Gets or sets the student.
    /// </summary>
    /// <value>The student.</value>
    public virtual Student? Student { get; set; }

    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    /// <value>The name.</value>
    [NotMapped]
    public string? Name { get; set; }
}