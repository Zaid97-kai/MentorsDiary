using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace MentorsDiary.Application.Account;

/// <summary>
/// Class AuthOptions.
/// </summary>
public class AuthOptions
{
    /// <summary>
    /// The issuer
    /// </summary>
    public const string ISSUER = "MyAuthServer";

    /// <summary>
    /// The audience
    /// </summary>
    public const string AUDIENCE = "MyAuthClient";

    /// <summary>
    /// The key
    /// </summary>
    const string KEY = "mysupersecret_secretkey!123";

    /// <summary>
    /// Gets the symmetric security key.
    /// </summary>
    /// <returns>SymmetricSecurityKey.</returns>
    public static SymmetricSecurityKey GetSymmetricSecurityKey() => new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
}