using System.ComponentModel.DataAnnotations.Schema;
using MentorsDiary.Application.Bases.Interfaces.IHaves;
using MentorsDiary.Application.Entities.Parents.Domains;
using MentorsDiary.Application.Entities.Students.Domains;

namespace MentorsDiary.Application.Entities.ParentStudents.Domains;

/// <summary>
/// Class ParentStudent.
/// Implements the <see cref="IHaveId" />
/// Implements the <see cref="IHaveName" />
/// </summary>
/// <seealso cref="IHaveId" />
/// <seealso cref="IHaveName" />
public class ParentStudent : IHaveId, IHaveName
{
    /// <summary>
    /// Gets or sets the identifier.
    /// </summary>
    /// <value>The identifier.</value>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    /// <value>The name.</value>
    [NotMapped]
    public string? Name { get; set; }

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
    /// Gets or sets the parent identifier.
    /// </summary>
    /// <value>The parent identifier.</value>
    public int ParentId { get; set; }

    /// <summary>
    /// Gets or sets the parent.
    /// </summary>
    /// <value>The parent.</value>
    public virtual Parent? Parent { get; set; }
}