using System.Collections.Generic;
using TelegramBot.WebApi.DB.Models;

namespace TelegramBot.WebApi.DB.Services
{
    public interface IUnhandledMessageService
    {
        void Insert(UnhandledMessage message);
        IList<UnhandledMessage> GetAll();
    }
}