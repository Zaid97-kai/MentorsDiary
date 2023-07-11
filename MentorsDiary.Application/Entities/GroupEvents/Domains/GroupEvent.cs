using MentorsDiary.Application.Bases.BaseUsers;
using MentorsDiary.Application.Bases.Interfaces.IHaves;
using MentorsDiary.Application.Entities.Events.Domains;
using MentorsDiary.Application.Entities.Groups.Domains;

namespace MentorsDiary.Application.Entities.GroupEvents.Domains;

/// <summary>
/// Class GroupEvent.
/// Implements the <see cref="BaseUserCU" />
/// Implements the <see cref="IHaveId" />
/// </summary>
/// <seealso cref="BaseUserCU" />
/// <seealso cref="IHaveId" />
public class GroupEvent : BaseUserCU, IHaveId
{
    /// <summary>
    /// Gets or sets the identifier.
    /// </summary>
    /// <value>The identifier.</value>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the count participants.
    /// </summary>
    /// <value>The count participants.</value>
    public int? CountParticipants { get; set; }

    /// <summary>
    /// Gets or sets the group identifier.
    /// </summary>
    /// <value>The group identifier.</value>
    public int? GroupId { get; set; }

    /// <summary>
    /// Gets or sets the group.
    /// </summary>
    /// <value>The group.</value>
    public virtual Group? Group { get; set; }

    /// <summary>
    /// Gets or sets the event identifier.
    /// </summary>
    /// <value>The event identifier.</value>
    public int? EventId { get; set; }

    /// <summary>
    /// Gets or sets the event.
    /// </summary>
    /// <value>The event.</value>
    public virtual Event? Event { get; set; }
}
