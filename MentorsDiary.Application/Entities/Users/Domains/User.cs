using MentorsDiary.Application.Bases.BaseUsers;
using MentorsDiary.Application.Bases.Enums;
using MentorsDiary.Application.Bases.Interfaces.IHaves;
using MentorsDiary.Application.Entities.Curators.Domains;
using MentorsDiary.Application.Entities.Divisions.Domains;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using HttpService.Model.User.Domain;

namespace MentorsDiary.Application.Entities.Users.Domains;

/// <summary>
/// Class User.
/// Implements the <see cref="BaseUserCU" />
/// Implements the <see cref="IHaveId" />
/// Implements the <see cref="IHaveName" />
/// Implements the <see cref="IHaveImage" />
/// </summary>
/// <seealso cref="BaseUserCU" />
/// <seealso cref="IHaveId" />
/// <seealso cref="IHaveName" />
/// <seealso cref="IHaveImage" />
public class User : BaseUserCU, IHaveId, IHaveName, IHaveImage, IBaseUser
{
    /// <summary>
    /// Gets or sets the identifier.
    /// </summary>
    /// <value>The identifier.</value>
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    /// <value>The name.</value>
    [Display(Name = "Имя")]
    public string? Name { get; set; }

    /// <summary>
    /// Gets or sets the password.
    /// </summary>
    /// <value>The password.</value>
    [Display(Name = "Пароль")]
    public string? Password { get; set; }

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
    /// Gets or sets the image path.
    /// </summary>
    /// <value>The image path.</value>
    public string? ImagePath { get; set; }

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
    /// Gets or sets the role.
    /// </summary>
    /// <value>The role.</value>
    public EnumRoles? Role { get; set; }

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
}