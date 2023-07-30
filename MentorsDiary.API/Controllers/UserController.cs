using MentorsDiary.API.Controllers.Bases;
using MentorsDiary.Application.Account;
using MentorsDiary.Application.Entities.Users.Domains;
using MentorsDiary.Application.Entities.Users.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MentorsDiary.API.Controllers;

/// <summary>
/// Class UserController.
/// Implements the <see cref="IUserRepository" />
/// </summary>
/// <seealso cref="IUserRepository" />
public class UserController : BaseController<User, IUserRepository>
{
    /// <summary>
    /// The env
    /// </summary>
    private readonly IWebHostEnvironment _env;

    /// <summary>
    /// The repository
    /// </summary>
    private readonly IUserRepository _repository;

    /// <summary>
    /// Initializes a new instance of the <see cref="UserController" /> class.
    /// </summary>
    /// <param name="repository">The repository.</param>
    /// <param name="env">The env.</param>
    public UserController(IUserRepository repository, IWebHostEnvironment env) : base(repository)
    {
        _repository = repository;
        _env = env;
    }

    /// <summary>
    /// Logins the specified user.
    /// </summary>
    /// <param name="user">The user.</param>
    /// <returns>IResult.</returns>
    [HttpPost("Login")]
    public async Task<IResult> Login([FromBody] User user)
    {
        var users = await _repository.GetAll();
        var person = users!.FirstOrDefault(p => p.Name == user.Name && p.Password == user.Password);
        
        if (person is null) 
            return Results.Unauthorized();

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, person.Name ?? string.Empty),
            new Claim(ClaimTypes.Role, person.Role.ToString() ?? string.Empty)
        };
        
        var jwt = new JwtSecurityToken(
            issuer: AuthOptions.ISSUER,
            audience: AuthOptions.AUDIENCE,
            claims: claims,
            expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(2)),
            signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
        var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
        
        var response = new AuthResponse
        {
            Code = encodedJwt,
            Message = person.Id
        };

        return Results.Json(response);
    }

    /// <summary>
    /// Saves the image.
    /// </summary>
    /// <param name="files">The files.</param>
    /// <returns>IFormFile.</returns>
    [HttpPost("UploadAvatar")]
    public async Task<IFormFile> UploadAvatar([FromForm] List<IFormFile> files)
    {
        if (!Directory.Exists(Path.Combine(_env.ContentRootPath, "resources")))
        {
            Directory.CreateDirectory(Path.Combine(_env.ContentRootPath, "resources"));
        }
        var path = Path.Combine(_env.ContentRootPath, "resources", files[0].FileName);

        await using FileStream fs = new(path, FileMode.Create);
        await files[0].CopyToAsync(fs);

        return files[0];
    }

    /// <summary>
    /// Retrieves image.
    /// </summary>
    /// <param name="avatarPath">Path to the image.</param>
    /// <returns>File.</returns>
    [HttpGet("GetAvatar/{avatarPath}")]
    public async Task<IActionResult> GetAvatar(string avatarPath)
    {
        if (!Directory.Exists(Path.Combine(_env.ContentRootPath, "resources")))
        {
            Directory.CreateDirectory(Path.Combine(_env.ContentRootPath, "resources"));
        }

        if (System.IO.File.Exists(Path.Combine(_env.ContentRootPath, "resources", avatarPath)))
        {
            var fileBytes = await System.IO.File.ReadAllBytesAsync(Path.Combine(_env.ContentRootPath, "resources", avatarPath));
            return File(fileBytes, "image/png");
        }
        else
        {
            return NotFound();
        }
    }
}