using LiteDB;

namespace TelegramBot.WebApi.DB.Models
{
    public class User
    {
        [BsonId]
        public string Id { get; set; }
    }
}