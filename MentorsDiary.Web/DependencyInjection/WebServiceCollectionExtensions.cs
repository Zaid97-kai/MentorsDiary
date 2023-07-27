using HttpService.Services;
using MentorsDiary.Web.Data.Authentication;
using MentorsDiary.Web.Data.Services;
using Microsoft.AspNetCore.Components.Authorization;

namespace MentorsDiary.Web.DependencyInjection;

/// <summary>
/// Class WebServiceCollectionExtensions.
/// </summary>
public static class WebServiceCollectionExtensions
{
    /// <summary>
    /// Adds the web collection.
    /// </summary>
    /// <param name="serviceCollection">The service collection.</param>
    /// <returns>IServiceCollection.</returns>
    public static IServiceCollection AddWebCollection(this IServiceCollection serviceCollection)
    {
        #region ПОДКЛЮЧЕНИЕ СЕРВИСОВ

        serviceCollection.AddSingleton<AuthenticationService>();
        serviceCollection.AddSingleton<UserService>();
        serviceCollection.AddSingleton<DivisionService>();
        serviceCollection.AddScoped<CuratorService>();
        serviceCollection.AddSingleton<GroupService>();
        serviceCollection.AddSingleton<ParentService>();
        serviceCollection.AddSingleton<GroupEventService>();
        serviceCollection.AddSingleton<GroupEventStudentService>();
        serviceCollection.AddSingleton<EventService>();
        serviceCollection.AddSingleton<StudentService>();

        #endregion

        serviceCollection.AddScoped<WebsiteAuthenticator>();
        serviceCollection.AddScoped<AuthenticationStateProvider>(sp => sp.GetRequiredService<WebsiteAuthenticator>());

        serviceCollection.AddTransient(_ => new HttpClient
        {
            BaseAddress = new Uri("https://localhost:7269")
        });
        
        return serviceCollection;
    }
}