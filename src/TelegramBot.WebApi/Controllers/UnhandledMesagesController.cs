using Microsoft.AspNetCore.Mvc;
using TelegramBot.WebApi.DB.Services;

namespace TelegramBot.WebApi.Controllers
{
    [Route("api/messages/unhandled")]
    public class UnhandledMesagesController : Controller
    {
        private readonly IUnhandledMessageService _unhandledMessageService;

        public UnhandledMesagesController(IUnhandledMessageService unhandledMessageService)
        {
            _unhandledMessageService = unhandledMessageService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_unhandledMessageService.GetAll());
        }
    }
}