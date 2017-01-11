using System.Collections.Generic;
using System.Linq;
using LiteDB;
using TelegramBot.WebApi.DB.Models;
using TelegramBot.WebApi.Models;

namespace TelegramBot.WebApi.DB.Services
{
    public class UnhandledMessageService : IUnhandledMessageService
    {
        private readonly AppSettings _appSettings;

        public UnhandledMessageService(AppSettings appSettings)
        {
            _appSettings = appSettings;
        }

        public void Insert(UnhandledMessage message)
        {
            using (var db = new LiteDatabase(_appSettings.DataBasePath))
            {
                db.UnhandledMessages().Insert(message);
            }
        }

        public IList<UnhandledMessage> GetAll()
        {
            using (var db = new LiteDatabase(_appSettings.DataBasePath))
            {
                return db.UnhandledMessages()
                    .FindAll()
                    .ToList();
            }
        }
    }
}