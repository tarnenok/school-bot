using System;
using LiteDB;

namespace TelegramBot.WebApi.DB.Models
{
    public class SchoolNews
    {
        [BsonId]
        public string Url { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public DateTime Date { get; set; }
    }
}