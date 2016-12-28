using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Quartz;
using Telegram.Bot;
using TelegramBot.WebApi.Services;

namespace TelegramBot.WebApi.Jobs
{
    public class GetNewsJob : IJob
    {
        private readonly SchoolNewService _schoolNewService;
        private readonly ITelegramBotClient _botClient;

        public GetNewsJob(SchoolNewService schoolNewService, ITelegramBotClient botClient)
        {
            _schoolNewService = schoolNewService;
            _botClient = botClient;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var news = (await _schoolNewService.GetNewsAsync(DateTime.Now)).ToList();
            if(news.Count == 0) return;

            var chartIds = new List<string>();
            foreach (var schoolNews in news)
            {
                foreach (var chartId in chartIds)
                {
                    await _botClient.SendTextMessageAsync(chartId, schoolNews.Title);
                }
            }
        }
    }
}