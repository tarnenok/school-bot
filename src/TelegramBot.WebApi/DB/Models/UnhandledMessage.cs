using System;

namespace TelegramBot.WebApi.DB.Models
{
    public class UnhandledMessage
    {
        public string Text { get; set; }
        public long ChartId { get; set; }
        public DateTime Created { get; set; }
    }
}