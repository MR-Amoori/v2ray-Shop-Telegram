using System.ComponentModel.DataAnnotations;

namespace v2ray_Shop_Telegram.Models
{
    public class BotModel
    {
        [Key]
        public int BotId { get; set; }
        public string BotToken { get; set; }
        [MaxLength(255)]
        public string BotName { get; set; }
    }
}
