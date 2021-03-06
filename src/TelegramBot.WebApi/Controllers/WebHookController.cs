﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBot.WebApi.DB.Models;
using TelegramBot.WebApi.DB.Services;
using TelegramBot.WebApi.Extensions;
using Chat = TelegramBot.WebApi.DB.Models.Chat;

namespace TelegramBot.WebApi.Controllers
{
    [Route("/webhook")]
    public class WebHookController : Controller
    {
        private readonly ITelegramBotClient _botClient;
        private readonly IChatService _chatService;
        private readonly ISchoolNewsService _schoolNewsService;
        private readonly IUnhandledMessageService _unhandledMessageService;

        public WebHookController(
            ITelegramBotClient botClient,
            IChatService chatService,
            ISchoolNewsService schoolNewsService,
            IUnhandledMessageService unhandledMessageService)
        {
            _botClient = botClient;
            _chatService = chatService;
            _schoolNewsService = schoolNewsService;
            _unhandledMessageService = unhandledMessageService;
        }

        [HttpPost("")]
        public async Task<IActionResult> GetMessage([FromBody]Update update)
        {
            var message = update.Message;

            if (message.Text.Contains("/start"))
            {
                _chatService.Upsert(new Chat
                {
                    Id = message.Chat.Id.ToString()
                });
                await _botClient.SendTextMessageAsync(message.Chat.Id, HelpText);
            }else if (message.Text.Contains("/help"))
            {
                await _botClient.SendTextMessageAsync(message.Chat.Id, HelpText);
            }else if (message.Text.Contains("/week"))
            {
                var news = _schoolNewsService.GetByFilter(new SchoolNewsFilter())
                    .Where(x => x.Date.DateInDays() > DateTime.Now.DateInDays() - 7);
                foreach (var schoolNewse in news)
                {
                    await _botClient.SendNews(schoolNewse, message.Chat.Id.ToString());
                }
            }else if (message.Text.Contains("/month"))
            {
                var news = _schoolNewsService.GetByFilter(new SchoolNewsFilter())
                    .Where(x => x.Date.DateInDays() > DateTime.Now.DateInDays() - 31);
                foreach (var schoolNewse in news)
                {
                    await _botClient.SendNews(schoolNewse, message.Chat.Id.ToString());
                }
            }
            else
            {
                await _botClient.SendTextMessageAsync(message.Chat.Id, UhandledText);
                _unhandledMessageService.Insert(new UnhandledMessage
                {
                    Text = message.Text,
                    ChartId = message.Chat.Id,
                    Created = DateTime.Now.ToUniversalTime(),
                });
            }

            return Ok();
        }

        private string HelpText => "Привет! Я бот Средней школы №1 г.п. Шарковщина. Моя задача - оповещать о новостях школы.\n"+
                                   "Вот список комманд, которые я могу выполнять\n"+
                                   "/month - Показать список новостей за месяц\n"+
                                   "/week - Показать список новостей за неделю\n"+
                                   "/help - Узнай что я могу делать";

        private string UhandledText => "Я тебя не понимаю, попробуй еще раз";
    }
}
