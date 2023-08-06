using MentorsDiary.Web.DependencyInjection;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;

namespace MentorsDiary.Web;

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
        
        builder.Services.AddRazorPages();
        builder.Services.AddAuthorization();
        builder.Services.AddAuthentication(opt =>
            {
                opt.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            })
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, opt =>
            {
                opt.Authority = "https://localhost:4000";
                opt.ClientId = "userInteractive";
                opt.ClientSecret = "secret";
                opt.ResponseType = "code";
                opt.Scope.Add("user");
                opt.SaveTokens = true;
            });

        builder.Services.AddServerSideBlazor();
        builder.Services.AddAntDesign();
        builder.Services.AddWebCollection();

        var app = builder.Build();
        
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseAuthentication();
        app.UseRouting();

        app.UseAuthorization();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

        app.MapRazorPages().RequireAuthorization();
        
        app.MapBlazorHub();

        app.MapFallbackToPage("/_Host");

        app.Run();
    }
}