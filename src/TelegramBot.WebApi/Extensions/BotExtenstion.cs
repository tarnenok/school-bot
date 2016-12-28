using System;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBot.WebApi.DB.Models;

namespace TelegramBot.WebApi.Extensions
{
    public static class BotExtenstion
    {
        public static Task SendNews(this ITelegramBotClient botClient, SchoolNews schoolNews, string chartId)
        {
            return botClient.SendPhotoAsync(
                chartId,
                new FileToSend(new Uri(schoolNews.ImageUrl)),
                $"{schoolNews.Date:d}\n{schoolNews.Title}\n{schoolNews.Url}"
            );
        }
    }
}