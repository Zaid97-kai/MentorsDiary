using MentorsDiary.Application.Entities.Users.Domains;
using MentorsDiary.MAUI.Data.Services.Bases;

namespace MentorsDiary.MAUI.Data.Services;

/// <summary>
/// Class UserService.
/// Implements the <see cref="MentorsDiary.MAUI.Data.Services.Bases.BaseService{MentorsDiary.Application.Entities.Users.Domains.User}" />
/// </summary>
/// <seealso cref="MentorsDiary.MAUI.Data.Services.Bases.BaseService{MentorsDiary.Application.Entities.Users.Domains.User}" />
public class UserService : BaseService<User>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UserService"/> class.
    /// </summary>
    /// <param name="httpClient">The HTTP client.</param>
    public UserService(HttpClient httpClient) : base(httpClient)
    {

    }
}