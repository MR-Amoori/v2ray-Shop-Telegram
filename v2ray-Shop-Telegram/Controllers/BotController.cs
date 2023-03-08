using Microsoft.AspNetCore.Mvc;

namespace v2ray_Shop_Telegram.Controllers
{
    public class BotController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
