using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using MentorsDiary.Application.Bases.BaseUsers;
using MentorsDiary.Application.Bases.Interfaces.IHaves;
using MentorsDiary.Application.Entities.Curators.Domains;
using MentorsDiary.Application.Entities.Divisions.Domains;
using MentorsDiary.Application.Entities.GroupEvents.Domains;
using MentorsDiary.Application.Entities.Students.Domains;

namespace MentorsDiary.Application.Entities.Groups.Domains;

/// <summary>
/// Class Group.
/// Implements the <see cref="BaseUserCU" />
/// Implements the <see cref="IHaveId" />
/// Implements the <see cref="IHaveName" />
/// </summary>
/// <seealso cref="BaseUserCU" />
/// <seealso cref="IHaveId" />
/// <seealso cref="IHaveName" />
public class Group : BaseUserCU, IHaveId, IHaveName
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
    /// Gets the long name.
    /// </summary>
    /// <value>The long name.</value>
    [NotMapped]
    [JsonIgnore]
    public string? LongName => $"{Name}[Текущий куратор: {Curator?.Name}]";

    /// <summary>
    /// Gets or sets the division identifier.
    /// </summary>
    /// <value>The division identifier.</value>
    public int? DivisionId { get; set; }

    /// <summary>
    /// Gets or sets the division.
    /// </summary>
    /// <value>The division.</value>
    public virtual Division? Division { get; set; }

    /// <summary>
    /// Gets or sets the curator identifier.
    /// </summary>
    /// <value>The curator identifier.</value>
    public int? CuratorId { get; set; }

    /// <summary>
    /// Gets or sets the curator.
    /// </summary>
    /// <value>The curator.</value>
    public virtual Curator? Curator { get; set; }

    /// <summary>
    /// Gets or sets the students.
    /// </summary>
    /// <value>The students.</value>
    [JsonIgnore]
    public virtual List<Student>? Students { get; set; }

    /// <summary>
    /// Gets or sets the group events.
    /// </summary>
    /// <value>The group events.</value>
    [JsonIgnore]
    public virtual List<GroupEvent>? GroupEvents { get; set; }
}