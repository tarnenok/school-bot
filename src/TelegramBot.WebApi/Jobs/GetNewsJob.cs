using System;
using System.Threading.Tasks;
using Quartz;

namespace TelegramBot.WebApi.Jobs
{
    public class GetNewsJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            return Console.Out.WriteLineAsync("news have been got");
        }
    }
}