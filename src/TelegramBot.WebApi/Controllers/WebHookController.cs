using Microsoft.AspNetCore.Mvc;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBot.WebApi.DB.Services;
using Chat = TelegramBot.WebApi.DB.Models.Chat;

namespace TelegramBot.WebApi.Controllers
{
    [Route("/webhook")]
    public class WebHookController : Controller
    {
        private readonly ITelegramBotClient _botClient;
        private readonly IChartService _chartService;

        public WebHookController(ITelegramBotClient botClient, IChartService chartService)
        {
            _botClient = botClient;
            _chartService = chartService;
        }

        [HttpPost("")]
        public IActionResult GetMessage([FromBody]Update update)
        {
            var message = update.Message;

            if (message.Text.Contains("/start"))
            {
                _chartService.Upsert(new Chat
                {
                    Id = message.Chat.Id.ToString()
                });
            }

            return Ok();
        }
    }
}
