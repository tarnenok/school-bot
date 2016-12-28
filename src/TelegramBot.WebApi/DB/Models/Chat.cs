using LiteDB;

namespace TelegramBot.WebApi.DB.Models
{
    public class Chat
    {
        [BsonId]
        public string Id { get; set; }
    }
}