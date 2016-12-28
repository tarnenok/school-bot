using System;

namespace TelegramBot.WebApi.Models
{
    public class SchoolNews
    {
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public DateTime Date { get; set; }
        public string Url { get; set; }
    }
}