using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TelegramBot.WebApi.Models;

namespace TelegramBot.WebApi.Extensions
{
    public static class ConfigExtensions
    {
        public static IServiceCollection AddAppSettings(this IServiceCollection services, IConfigurationRoot config)
        {
            var settings = config.GetAppSettings();
            services.AddSingleton(settings);
            return services;
        }

        public static AppSettings GetAppSettings(this IConfigurationRoot config)
        {
            var settings = new AppSettings();
            config.Bind(settings);
            return settings;
        }
    }
}