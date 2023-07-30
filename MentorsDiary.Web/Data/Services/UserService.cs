using MentorsDiary.Application.Entities.Bases.Filters;
using MentorsDiary.Application.Entities.Users.Domains;
using MentorsDiary.Web.Data.Services.Bases;
using System.Net.Http;

namespace MentorsDiary.Web.Data.Services;

/// <summary>
/// Class UserService.
/// Implements the <see cref="MentorsDiary.Web.Data.Services.Bases.BaseService{MentorsDiary.Application.Entities.Users.Domains.User}" />
/// </summary>
/// <seealso cref="MentorsDiary.Web.Data.Services.Bases.BaseService{MentorsDiary.Application.Entities.Users.Domains.User}" />
public class UserService: BaseService<User>
{
    /// <summary>
    /// The HTTP client
    /// </summary>
    private readonly HttpClient? _httpClient;

    /// <summary>
    /// Initializes a new instance of the <see cref="UserService"/> class.
    /// </summary>
    /// <param name="httpClient">The HTTP client.</param>
    public UserService(HttpClient? httpClient) : base(httpClient)
    {
        _httpClient = httpClient;
    }

    /// <summary>
    /// Logins the specified user.
    /// </summary>
    /// <param name="user">The user.</param>
    /// <returns>HttpResponseMessage.</returns>
    public virtual async Task<HttpResponseMessage> Login(User user)
    {
        var result = await _httpClient?.PostAsJsonAsync($"api/{BasePath}/login", user)!;
        return result;
    }
}