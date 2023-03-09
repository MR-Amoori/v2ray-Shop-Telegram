using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using v2ray_Shop_Telegram.Data;
using v2ray_Shop_Telegram.Models;

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

            return RedirectToAction("Index", "Home");
        }

        public void Stop()
        {
            botThread.Abort();
        }

        void runBot()
        {
            List<KeyboardButton> keyboardButtons = new List<KeyboardButton>()
            {
                new KeyboardButton("🔧 " + "شروع مجدد" + " 🔧"),
                new KeyboardButton("📒 " + "خرید" + " 📒"),
            };

            mainKeyboardMarkup = new ReplyKeyboardMarkup(keyboardButtons);

            Token = "6019278623:AAGrvsrExUtcXYzm7keU-HvWX9CgLgVN8Y8";
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


                    if (text.Contains("/start")|| text.Contains("🔧 شروع مجدد 🔧"))
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.AppendLine($"سلام {from.FirstName} خوش آمدید 🌹");
                        sb.AppendLine("");
                        sb.AppendLine("راهنما : /Help");
                        sb.AppendLine("");
                        sb.AppendLine("🤖 @NameDN_bot 🤖");
                        bot.SendTextMessageAsync(chatId, sb.ToString(), ParseMode.Html, default, default, default, default, default, default, mainKeyboardMarkup);
                    }

                    else if (text.Contains("📒 خرید 📒"))
                    {
                        
                    }
                }
            }
        }


        [HttpPost]
        public IActionResult AddBot(BotModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    BotModel modelResult = new BotModel()
                    {
                        BotName = model.BotName,
                        BotToken = model.BotToken
                    };
                    _context.Bots.Add(modelResult);
                    _context.SaveChanges();
                    return RedirectToAction("Index", "Home");
                }
                catch
                {
                    ModelState.AddModelError("", "ناموفق");
                    return RedirectToAction("Index", "Home");
                }

            }
            else
            {
                ModelState.AddModelError("", "ناموفق");
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
