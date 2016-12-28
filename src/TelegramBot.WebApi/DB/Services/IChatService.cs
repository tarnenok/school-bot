using System.Collections.Generic;
using Telegram.Bot.Types;

namespace TelegramBot.WebApi.DB.Services
{
    public interface IChartService
    {
        void Upsert(Chat chat);
        List<Chat> GetByFilter();
    }
}