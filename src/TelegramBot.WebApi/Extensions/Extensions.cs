using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using RestSharp;
using TelegramBot.WebApi.Models;

namespace TelegramBot.WebApi.Extensions
{
    public static class Extensions
    {
        public static T Sync<T>(this Task<T> task)
        {
            return task.GetAwaiter().GetResult();
        }

        public static void Sync(this Task task)
        {
            task.GetAwaiter().GetResult();
        }

        public static Task<IRestResponse> ExecuteTaskAsync(this IRestClient client, IRestRequest request)
        {
            var source = new TaskCompletionSource<IRestResponse>();
            client.ExecuteAsync(request, response =>
            {
                source.SetResult(response);
            });
            return source.Task;
        }

        public static int DateInDays(this DateTime date)
        {
            return date.Year * 365 + date.DayOfYear;
        }
    }
}