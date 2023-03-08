using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Text;
using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using v2ray_Shop_Telegram.Data;

namespace v2ray_Shop_Telegram.Controllers
{
    public class BotController : Controller
    {
        private v2rayShopContext _context;

        private static string Token = "";
        private Thread botThread;
        private TelegramBotClient bot;
        private Telegram.Bot.Types.Update[] update;
        private ReplyKeyboardMarkup mainKeyboardMarkup;


        public BotController(v2rayShopContext context)
        {
            _context = context;
        }

        public IActionResult Start()
        {

            botThread = new Thread(new ThreadStart(runBot));
            botThread.Start();

            return View();
        }

        public void Stop()
        {
            botThread.Abort();
        }

        void runBot()
        {
            KeyboardButton[] row1 = { new KeyboardButton("🔧 " + "ابزار های ربات" + " 🔧")/*, new KeyboardButton("📝" + " لیست " + "📝")*/ };
            KeyboardButton[] row2 = { new KeyboardButton("📒 " + "راهنما" + " 📒"), new KeyboardButton("👨🏻‍💻 " + "درباره برنامه نویس" + " 👨🏻‍💻") };
            mainKeyboardMarkup.Keyboard = new KeyboardButton[][] { row1, row2 };
            mainKeyboardMarkup = new ReplyKeyboardMarkup(mainKeyboardMarkup.Keyboard);

            Token = _context.Bots.FirstOrDefault().BotToken;
            bot = new TelegramBotClient(Token);
            int offset = 0;
            while (true)
            {
                try
                {
                    update = bot.GetUpdatesAsync(offset).Result;
                }
                catch (AggregateException)
                {
                    botThread.Abort();
                }


                foreach (var up in update)
                {
                    offset = up.Id + 1;

                    if (up.Message == null)
                        continue;

                    var text = up.Message.Text;
                    var from = up.Message.From;
                    var chatId = up.Message.Chat.Id;


                    if (text.Contains("/start"))
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.AppendLine($"سلام {from.FirstName} خوش آمدید 🌹");
                        sb.AppendLine("درباره برنامه نویس : /AboutUs");
                        sb.AppendLine("راهنما : /Help");
                        sb.AppendLine("");
                        sb.AppendLine("🤖 @NameDN_bot 🤖");
                        bot.SendTextMessageAsync(chatId, sb.ToString(), ParseMode.Html, default, default,default,default,default,default,mainKeyboardMarkup);
                    }



                }
            }
        }


    }
}
