using System.Collections.Generic;
using System.Linq;
using LiteDB;
using TelegramBot.WebApi.DB.Models;
using TelegramBot.WebApi.Models;

namespace TelegramBot.WebApi.DB.Services
{
    public class SchoolNewsService : ISchoolNewsService
    {
        private readonly AppSettings _appSettings;

        public SchoolNewsService(AppSettings appSettings)
        {
            _appSettings = appSettings;
        }

        public void Upsert(IEnumerable<SchoolNews> news)
        {
            using (var db = new LiteDatabase(_appSettings.DataBasePath))
            {
                db.SchollNews().Upsert(news);
            }
        }

        //TODO refactor filtering
        public IEnumerable<SchoolNews> GetByFilter(SchoolNewsFilter filter)
        {
            using (var db = new LiteDatabase(_appSettings.DataBasePath))
            {
                return db.SchollNews()
                    .FindAll()
                    .ToList();
            }
        }
    }
}