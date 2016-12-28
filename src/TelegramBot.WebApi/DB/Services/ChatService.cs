using System.Collections.Generic;
using System.Linq;
using LiteDB;
using TelegramBot.WebApi.DB.Models;

namespace TelegramBot.WebApi.DB.Services
{
    public class ChatService : IChartService
    {
        public void Upsert(Chat chat)
        {
            using (var db = new LiteDatabase(DBConfig.DataBasePath))
            {
                db.Chats().Upsert(chat);
            }
        }

        public List<Chat> GetByFilter()
        {
            using (var db = new LiteDatabase(DBConfig.DataBasePath))
            {
                return db.Chats().FindAll().ToList();
            }
        }
    }
}