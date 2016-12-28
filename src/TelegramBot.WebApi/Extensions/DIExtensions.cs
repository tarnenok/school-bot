using Microsoft.Extensions.DependencyInjection;
using TelegramBot.WebApi.Services;

namespace TelegramBot.WebApi.Extensions
{
    public static class DIExtensions
    {
        public static IServiceCollection AddAppDependencies(this IServiceCollection collection)
        {
            collection.AddTransient<SchoolNewService>();
            return collection;
        }
    }
}