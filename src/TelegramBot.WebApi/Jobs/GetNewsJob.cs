using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Quartz;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBot.WebApi.DB.Models;
using TelegramBot.WebApi.DB.Services;
using TelegramBot.WebApi.Extensions;
using TelegramBot.WebApi.Services;

namespace TelegramBot.WebApi.Jobs
{
    public class GetNewsJob : IJob
    {
        private readonly SchoolNewService _schoolNewLoader;
        private readonly ITelegramBotClient _botClient;
        private readonly IChatService _chatService;
        private readonly ISchoolNewsService _schoolNewsService;
        private readonly ILogger _logger;


        public GetNewsJob(
            SchoolNewService schoolNewLoader,
            ITelegramBotClient botClient,
            IChatService chatService,
            ISchoolNewsService schoolNewsService,
            ILoggerFactory loggerFactory)
        {
            _schoolNewLoader = schoolNewLoader;
            _botClient = botClient;
            _chatService = chatService;
            _schoolNewsService = schoolNewsService;
            _logger = loggerFactory.CreateLogger(nameof(GetNewsJob));
        }

        public async Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation($"Start message pulling at {DateTime.Now}");

            var date = DateTime.Now;
            var news = (await _schoolNewLoader.GetNewsAsync(date)).ToList();
            var charts = _chatService.GetByFilter();
            var newest = GetNewest(news, date).ToList();
            _schoolNewsService.Upsert(newest);

            _logger.LogInformation($"{newest.Count} news were added at {DateTime.Now}");

            foreach (var schoolNews in newest)
            {
                foreach (var chart in charts)
                {
                    await _botClient.SendNews(schoolNews, chart.Id);
                }
            }

            _logger.LogInformation($"Stop message pulling at {DateTime.Now}");
        }

        private IEnumerable<SchoolNews> GetNewest(IEnumerable<SchoolNews> news, DateTime date)
        {
            var existingNews = _schoolNewsService.GetByFilter(new SchoolNewsFilter {DateTime = date}).ToList();
            foreach (var schoolNewse in news)
            {
                if (existingNews.All(x => x.Url != schoolNewse.Url))
                {
                    yield return schoolNewse;
                }
            }
        }
    }
}