using System.Collections.Generic;
using TelegramBot.WebApi.DB.Models;

namespace TelegramBot.WebApi.ViewModels
{
    public class AllChatsViewModel
    {
        public List<Chat> Items { get; set; }
        public int Count { get; set; }
    }
}