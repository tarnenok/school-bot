using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LiteDB;
using Quartz;
using Telegram.Bot;
using TelegramBot.WebApi.DB;
using TelegramBot.WebApi.DB.Models;
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

            var charts = new List<Chat>();
            using (var db = new LiteDatabase(DBConfig.DataBasePath))
            {
                charts = db.Chats().FindAll().ToList();
                db.SchollNews().Upsert(news);
            }
            foreach (var schoolNews in news)
            {
                foreach (var chart in charts)
                {
                    await _botClient.SendTextMessageAsync(chart.Id, schoolNews.Title);
                }
            }
        }
    }
}