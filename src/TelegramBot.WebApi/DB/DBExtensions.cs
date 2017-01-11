using LiteDB;
using TelegramBot.WebApi.DB.Models;

namespace TelegramBot.WebApi.DB
{
    public static class DBExtensions
    {
        public static LiteCollection<Chat> Chats(this LiteDatabase db)
        {
            return db.GetCollection<Chat>("chats");
        }

        public static LiteCollection<SchoolNews> SchollNews(this LiteDatabase db)
        {
            return db.GetCollection<SchoolNews>("news");
        }

        public static LiteCollection<UnhandledMessage> UnhandledMessages(this LiteDatabase db)
        {
            return db.GetCollection<UnhandledMessage>("unhandled_messages");
        }
    }
}