using MentorsDiary.Application.Bases.BaseUsers;
using MentorsDiary.Application.Bases.Interfaces.IHaves;
using MentorsDiary.Application.Entities.GroupEvents.Domains;
using MentorsDiary.Application.Entities.Groups.Domains;
using MentorsDiary.Application.Entities.Parents.Domains;

namespace MentorsDiary.Application.Entities.Students.Domains;

/// <summary>
/// Class Student.
/// Implements the <see cref="BaseUserCU" />
/// Implements the <see cref="IHaveId" />
/// Implements the <see cref="IHaveImage" />
/// </summary>
/// <seealso cref="BaseUserCU" />
/// <seealso cref="IHaveId" />
/// <seealso cref="IHaveImage" />
public class Student : BaseUserCU, IHaveId, IHaveName, IHaveImage
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
    /// Gets or sets the email.
    /// </summary>
    /// <value>The email.</value>
    public string? Email { get; set; }

    /// <summary>
    /// Gets or sets the phone.
    /// </summary>
    /// <value>The phone.</value>
    public string? Phone { get; set; }

    /// <summary>
    /// Gets or sets the birth date.
    /// </summary>
    /// <value>The birth date.</value>
    public DateTime? BirthDate { get; set; }

    /// <summary>
    /// Gets or sets the address.
    /// </summary>
    /// <value>The address.</value>
    public string? Address { get; set; }

    /// <summary>
    /// Gets or sets the group identifier.
    /// </summary>
    /// <value>The group identifier.</value>
    public int GroupId { get; set; }

    /// <summary>
    /// Gets or sets the group.
    /// </summary>
    /// <value>The group.</value>
    public virtual Group Group { get; set; }

    /// <summary>
    /// Gets or sets the parent.
    /// </summary>
    /// <value>The parent.</value>
    public virtual List<Parent>? Parent { get; set; }

    /// <summary>
    /// Gets or sets the group events.
    /// </summary>
    /// <value>The group events.</value>
    public virtual List<GroupEvent>? GroupEvents { get; set; }

    /// <summary>
    /// Gets or sets the image path.
    /// </summary>
    /// <value>The image path.</value>
    public string? ImagePath { get; set; }
}