using Microsoft.AspNetCore.Mvc;
using LiteDB;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBot.WebApi.DB;
using Chat = TelegramBot.WebApi.DB.Models.Chat;

namespace TelegramBot.WebApi.Controllers
{
    [Route("/webhook")]
    public class WebHookController : Controller
    {
        private readonly ITelegramBotClient _botClient;

        public WebHookController(ITelegramBotClient botClient)
        {
            _botClient = botClient;
        }

        [HttpPost("")]
        public IActionResult GetMessage([FromBody]Update update)
        {
            var message = update.Message;

            if (message.Text.Contains("/start"))
            {
                using (var db = new LiteDatabase(DBConfig.DataBasePath))
                {
                    db.Chats()
                        .Upsert(new Chat
                        {
                            Id = message.Chat.Id.ToString()
                        });
                }
            }

            return Ok();
        }
    }
}
