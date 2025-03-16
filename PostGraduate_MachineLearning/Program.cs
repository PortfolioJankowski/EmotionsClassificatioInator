using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Services;

namespace PostGraduate_MachineLearning;

internal static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.
        ApplicationConfiguration.Initialize();

        var host = CreateHostBuilder()
            .Build();
        var firstForm = host.Services.GetRequiredService<MenuForm>();
        
        Application.Run(firstForm);
    }

    static IHostBuilder CreateHostBuilder() => Host.CreateDefaultBuilder()
        .ConfigureServices((context, services) =>
        {
            services.AddTransient<MenuForm>();
            services.AddServices();
        });
        
}