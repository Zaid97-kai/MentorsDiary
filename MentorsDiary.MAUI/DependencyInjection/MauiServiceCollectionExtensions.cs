using MentorsDiary.MAUI.Data.Services;

namespace MentorsDiary.MAUI.DependencyInjection;

/// <summary>
/// Class MauiServiceCollectionExtensions.
/// </summary>
public static class MauiServiceCollectionExtensions
{
    /// <summary>
    /// Adds the maui collection.
    /// </summary>
    /// <param name="serviceCollection">The service collection.</param>
    /// <returns>IServiceCollection.</returns>
    public static IServiceCollection AddMauiCollection(this IServiceCollection serviceCollection)
    {
        #region ПОДКЛЮЧЕНИЕ СЕРВИСОВ

        serviceCollection.AddSingleton<UserService>();
        serviceCollection.AddSingleton<DivisionService>();
        serviceCollection.AddScoped<CuratorService>();
        serviceCollection.AddSingleton<GroupService>();
        serviceCollection.AddSingleton<ParentService>();
        serviceCollection.AddSingleton<GroupEventService>();
        serviceCollection.AddSingleton<GroupEventStudentService>();
        serviceCollection.AddSingleton<ParentStudentService>();
        serviceCollection.AddSingleton<EventService>();
        serviceCollection.AddSingleton<StudentService>();

        #endregion

        serviceCollection.AddTransient(_ => new HttpClient
        {
            BaseAddress = new Uri("https://localhost:7269")
        });
        
        return serviceCollection;
    }
}