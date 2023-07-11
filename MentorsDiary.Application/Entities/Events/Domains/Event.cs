using MentorsDiary.Application.Bases.BaseUsers;
using MentorsDiary.Application.Bases.Interfaces.IHaves;

namespace MentorsDiary.Application.Entities.Events.Domains;

/// <summary>
/// Class Event.
/// Implements the <see cref="BaseUserCU" />
/// Implements the <see cref="IHaveId" />
/// Implements the <see cref="IHaveName" />
/// Implements the <see cref="IHaveImage" />
/// </summary>
/// <seealso cref="BaseUserCU" />
/// <seealso cref="IHaveId" />
/// <seealso cref="IHaveName" />
/// <seealso cref="IHaveImage" />
public class Event : BaseUserCU, IHaveId, IHaveName, IHaveImage
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
    /// Gets or sets the comment.
    /// </summary>
    /// <value>The comment.</value>
    public string? Comment { get; set; }

    /// <summary>
    /// Gets or sets the date event.
    /// </summary>
    /// <value>The date event.</value>
    public DateTime? DateEvent { get; set; }

    /// <summary>
    /// Gets or sets the image path.
    /// </summary>
    /// <value>The image path.</value>
    public string? ImagePath { get; set; }
}