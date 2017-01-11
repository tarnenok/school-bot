using Microsoft.AspNetCore.Mvc;
using TelegramBot.WebApi.DB.Services;
using TelegramBot.WebApi.ViewModels;

namespace TelegramBot.WebApi.Controllers
{
    [Route("api/charts")]
    public class ChartsController: Controller
    {
        private readonly IChatService _chatService;

        public ChartsController(IChatService chatService)
        {
            _chatService = chatService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var items = _chatService.GetByFilter();
            return Ok(new AllChatsViewModel
            {
                Items = items,
                Count = items.Count
            });
        }
    }
}