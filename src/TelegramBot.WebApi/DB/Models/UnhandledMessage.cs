using System;

namespace TelegramBot.WebApi.DB.Models
{
    public class UnhandledMessage
    {
        public string Text { get; set; }
        public string ChartId { get; set; }
        public string UserId { get; set; }
        public DateTime Created { get; set; }
    }
}