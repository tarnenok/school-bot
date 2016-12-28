using System.Collections.Generic;
using TelegramBot.WebApi.DB.Models;

namespace TelegramBot.WebApi.DB.Services
{
    public interface IChartService
    {
        void Upsert(Chat chat);
        List<Chat> GetByFilter();
    }
}