using System.Threading.Tasks;

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
    }
}