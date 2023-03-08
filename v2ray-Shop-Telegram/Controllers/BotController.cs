using Microsoft.AspNetCore.Mvc;
using System.Threading;
using Telegram.Bot;

namespace v2ray_Shop_Telegram.Controllers
{
    public class BotController : Controller
    {
        private static string Token = "";
        private Thread botThread;
        private TelegramBotClient bot;
        private Telegram.Bot.Types.Update[] update;

        public IActionResult Index()
        {
            return View();
        }
    }
}
