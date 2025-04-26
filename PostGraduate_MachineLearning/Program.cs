using EmotionClassifier.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Services;
using Services.Presenters;
using System.Configuration;
using EmotionClassifier.View.Interfaces;
using EmotionClassifier.Models.FormModels;

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


        var host = CreateHostBuilder().Build();

        var menuForm = host.Services.GetRequiredService<IMenuForm>() as Form;
        var menuPresenter = host.Services.GetRequiredService<MenuFormPresenter>();
        var isthesame = menuPresenter._view.Equals(menuForm);   
        Application.Run(menuForm);
    }

    static IHostBuilder CreateHostBuilder() => Host.CreateDefaultBuilder()
        .ConfigureServices((context, services) =>
        {
            services.AddSingleton<IMenuForm, MenuForm>();
            services.AddTransient<MenuFormModel>();
            services.AddTransient<MenuFormPresenter>();
            services.AddServices();
            services.AddTransient<ILoggerService>(provider =>
            {
                string loggerDirectory = ConfigurationManager.AppSettings["LoggerDirectory"]!;
                return new FileLoggerService(loggerDirectory);
            });
            services.AddSingleton(provider =>
            {
                return new AppSettings
                {
                    InitialFormWidth = int.Parse(ConfigurationManager.AppSettings["initialFormWidth"]!),
                    InitialFormHeight = int.Parse(ConfigurationManager.AppSettings["initialFormHeight"]!),
                    FinalFormHeight = int.Parse(ConfigurationManager.AppSettings["finalFormHeight"]!),
                    APIKey = ConfigurationManager.AppSettings["APIKey"]!,
                    APIKeySecret = ConfigurationManager.AppSettings["APIKeySecret"]!,
                    BearerToken = ConfigurationManager.AppSettings["BearerToken"]!,
                    AccessToken = ConfigurationManager.AppSettings["AccessToken"]!,
                    AccessTokenSecret = ConfigurationManager.AppSettings["AccessTokenSecret"]!,
                    LoggerDirectory = ConfigurationManager.AppSettings["LoggerDirectory"]!
                };
            });
        });
        
}