using Microsoft.EntityFrameworkCore;
using Telegram.Bot.Types;
using v2ray_Shop_Telegram.Models;

namespace v2ray_Shop_Telegram.Data
{
    public class v2rayShopContext : DbContext
    {
        public v2rayShopContext(DbContextOptions<v2rayShopContext> options) : base(options)
        {
        }

        public DbSet<BotModel> Bots { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            base.OnModelCreating(modelBuilder);
        }
    }
}
