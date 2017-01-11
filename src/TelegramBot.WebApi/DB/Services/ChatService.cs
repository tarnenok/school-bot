using System.Collections.Generic;
using System.Linq;
using LiteDB;
using TelegramBot.WebApi.DB.Models;
using TelegramBot.WebApi.Models;

namespace TelegramBot.WebApi.DB.Services
{
    public class ChatService : IChatService
    {
        private readonly AppSettings _appSettings;

        public ChatService(AppSettings appSettings)
        {
            _appSettings = appSettings;
        }

        public void Upsert(Chat chat)
        {
            using (var db = new LiteDatabase(_appSettings.DataBasePath))
            {
                db.Chats().Upsert(chat);
            }
        }

        public List<Chat> GetByFilter()
        {
            using (var db = new LiteDatabase(_appSettings.DataBasePath))
            {
                return db.Chats().FindAll().ToList();
            }
        }
    }
}