using MentorsDiary.Application.DependencyInjection;
using MentorsDiary.Persistence.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace MentorsDiary.API;

/// <summary>
/// Class Program.
/// </summary>
public class Program
{
    /// <summary>
    /// Defines the entry point of the application.
    /// </summary>
    /// <param name="args">The arguments.</param>
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        #region CUSTOM SERVICES

        builder.Services.AddApplication();
        builder.Services.AddPersistence(builder.Configuration);

        #endregion

        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opt =>
            {
                opt.Authority = "https://localhost:4000";
                opt.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateAudience = false
                };
            });

        builder.Services.AddAuthorization(opt =>
        {
            opt.AddPolicy("M2M", policy =>
            {
                policy.RequireAuthenticatedUser()
                    .RequireClaim("scope", "app");
            });
            opt.AddPolicy("Interactive", policy =>
            {
                policy.RequireAuthenticatedUser()
                    .RequireClaim("scope", "user");
            });
        });

        var app = builder.Build();
        
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}