using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Quartz;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBot.WebApi.DB.Models;
using TelegramBot.WebApi.DB.Services;
using TelegramBot.WebApi.Services;

namespace TelegramBot.WebApi.Jobs
{
    public class GetNewsJob : IJob
    {
        private readonly SchoolNewService _schoolNewLoader;
        private readonly ITelegramBotClient _botClient;
        private readonly IChartService _chartService;
        private readonly ISchoolNewsService _schoolNewsService;

        public GetNewsJob(
            SchoolNewService schoolNewLoader,
            ITelegramBotClient botClient,
            IChartService chartService,
            ISchoolNewsService schoolNewsService)
        {
            _schoolNewLoader = schoolNewLoader;
            _botClient = botClient;
            _chartService = chartService;
            _schoolNewsService = schoolNewsService;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var date = DateTime.Now;
            var news = (await _schoolNewLoader.GetNewsAsync(date)).ToList();

            if(news.Count == 0) return;

            var charts = _chartService.GetByFilter();

            var newest = GetNewest(news, date).ToList();
            _schoolNewsService.Upsert(newest);

            foreach (var schoolNews in newest)
            {
                foreach (var chart in charts)
                {
                    await _botClient.SendPhotoAsync(
                        chart.Id,
                        new FileToSend(new Uri(schoolNews.ImageUrl)),
                        $"{schoolNews.Title}\n{schoolNews.Url}"
                    );
                }
            }
        }

        private IEnumerable<SchoolNews> GetNewest(IEnumerable<SchoolNews> news, DateTime date)
        {
            var existingNews = _schoolNewsService.GetByFilter(new SchoolNewsFilter {DateTime = date}).ToList();
            foreach (var schoolNewse in news)
            {
                if (!existingNews.Any(x => x.Date.Year == date.Year && x.Date.Month == date.Month))
                {
                    yield return schoolNewse;
                }
            }
        }
    }
}