using MentorsDiary.API.Services;
using MentorsDiary.Application.DependencyInjection;
using MentorsDiary.Persistence.DependencyInjection;

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

        // Add services to the container.

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        #region CUSTOM SERVICES
        
        builder.Services.AddHostedService<KafkaCustomerHostedService>();
        builder.Services.AddApplication();
        builder.Services.AddPersistence(builder.Configuration);

        #endregion

        var app = builder.Build();

        // Configure the HTTP request pipeline.
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