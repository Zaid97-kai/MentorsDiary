using System.Text.Json.Serialization;
using MentorsDiary.Application.Bases.BaseUsers;
using MentorsDiary.Application.Bases.Interfaces.IHaves;
using MentorsDiary.Application.Entities.Students.Domains;

namespace MentorsDiary.Application.Entities.Parents.Domains;

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
    /// Gets or sets the mother childrens.
    /// </summary>
    /// <value>The mother childrens.</value>
    [JsonIgnore]
    public virtual List<Student>? MotherChildrens { get; set; }

    /// <summary>
    /// Gets or sets the father childrens.
    /// </summary>
    /// <value>The father childrens.</value>
    [JsonIgnore]
    public virtual List<Student>? FatherChildrens { get; set; }
}