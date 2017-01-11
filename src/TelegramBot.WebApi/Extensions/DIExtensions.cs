using Microsoft.Extensions.DependencyInjection;
using TelegramBot.WebApi.DB.Services;
using TelegramBot.WebApi.Services;

namespace TelegramBot.WebApi.Extensions
{
    public static class DIExtensions
    {
        public static IServiceCollection AddAppDependencies(this IServiceCollection collection)
        {
            collection.AddTransient<SchoolNewService>();
            collection.AddDBServices();
            return collection;
        }

        private static IServiceCollection AddDBServices(this IServiceCollection collection)
        {
            collection.AddTransient<ISchoolNewsService, SchoolNewsService>();
            collection.AddTransient<IChartService, ChatService>();
            collection.AddTransient<IUnhandledMessageService, UnhandledMessageService>();
            return collection;
        }
    }
}