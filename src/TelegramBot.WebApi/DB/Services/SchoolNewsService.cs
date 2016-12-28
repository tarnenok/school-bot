using System.Collections.Generic;
using System.Linq;
using LiteDB;
using TelegramBot.WebApi.DB.Models;

namespace TelegramBot.WebApi.DB.Services
{
    public class SchoolNewsService : ISchoolNewsService
    {
        public void Upsert(IEnumerable<SchoolNews> news)
        {
            using (var db = new LiteDatabase(DBConfig.DataBasePath))
            {
                db.SchollNews().Upsert(news);
            }
        }

        //TODO refactor filtering
        public IEnumerable<SchoolNews> GetByFilter(SchoolNewsFilter filter)
        {
            using (var db = new LiteDatabase(DBConfig.DataBasePath))
            {
                return db.SchollNews()
                    .FindAll()
                    .Where(x => x.Date.Year == filter.DateTime.Year && x.Date.Month == filter.DateTime.Month)
                    .ToList();
            }
        }
    }
}