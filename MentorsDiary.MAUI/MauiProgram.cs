using MentorsDiary.MAUI.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace MentorsDiary.MAUI
{
    /// <summary>
    /// Class MauiProgram.
    /// </summary>
    public static class MauiProgram
    {
        /// <summary>
        /// Creates the maui application.
        /// </summary>
        /// <returns>MauiApp.</returns>
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
		builder.Logging.AddDebug();
#endif

            builder.Services.AddMauiCollection();
            
            return builder.Build();
        }
    }
}