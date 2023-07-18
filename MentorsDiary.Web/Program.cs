using MentorsDiary.Kafka.Producer;
using MentorsDiary.Web.Data;
using MentorsDiary.Web.Services;

namespace MentorsDiary.Web;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddRazorPages();
        builder.Services.AddServerSideBlazor();
        builder.Services.AddSingleton<WeatherForecastService>();

        builder.Services.AddScoped<KafkaProducerHostedService>();
        builder.Services.AddScoped<DivisionService>();
        builder.Services.AddTransient(sp => new HttpClient
        {
            BaseAddress = new Uri("https://localhost:7269")
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseStaticFiles();

        app.UseRouting();

        app.MapBlazorHub();
        app.MapFallbackToPage("/_Host");

        app.Run();
    }
}