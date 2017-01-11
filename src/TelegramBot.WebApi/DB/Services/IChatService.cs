using System.Collections.Generic;
using TelegramBot.WebApi.DB.Models;

namespace TelegramBot.WebApi.DB.Services
{
    public interface IChatService
    {
        void Upsert(Chat chat);
        List<Chat> GetByFilter();
    }
}