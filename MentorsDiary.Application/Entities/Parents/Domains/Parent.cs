using System.Text.Json.Serialization;
using MentorsDiary.Application.Bases.BaseUsers;
using MentorsDiary.Application.Bases.Interfaces.IHaves;
using MentorsDiary.Application.Entities.Students.Domains;

namespace MentorsDiary.Application.Entities.Parents.Domains;

/// <summary>
/// Class Parent.
/// Implements the <see cref="BaseUserCU" />
/// Implements the <see cref="IHaveId" />
/// Implements the <see cref="IHaveName" />
/// </summary>
/// <seealso cref="BaseUserCU" />
/// <seealso cref="IHaveId" />
/// <seealso cref="IHaveName" />
public class Parent : BaseUserCU, IHaveId, IHaveName
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
    public string? Name { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this <see cref="Parent"/> is sex.
    /// </summary>
    /// <value><c>true</c> if MAN; otherwise, <c>false</c>.</value>
    public bool Sex { get; set; }

    /// <summary>
    /// Gets or sets the name of the work.
    /// </summary>
    /// <value>The name of the work.</value>
    public string? WorkName { get; set; }

    /// <summary>
    /// Gets or sets the phone.
    /// </summary>
    /// <value>The phone.</value>
    public string? Phone { get; set; }

    /// <summary>
    /// Gets or sets the childrens.
    /// </summary>
    /// <value>The childrens.</value>
    [JsonIgnore]
    public virtual List<Student>? Childrens { get; set; }
}