using System.Collections.Generic;
using TelegramBot.WebApi.DB.Models;

namespace TelegramBot.WebApi.DB.Services
{
    public interface ISchoolNewsService
    {
        void Upsert(IEnumerable<SchoolNews> news);
        IEnumerable<SchoolNews> GetByFilter(SchoolNewsFilter filter);
    }
}