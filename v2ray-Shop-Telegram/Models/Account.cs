using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace v2ray_Shop_Telegram.Models
{
    public class Account
    {
        [Key]
        public int UserId { get; set; }

        public int ChatId { get; set; }

        public float Wallet { get; set; }

        public DateTime RegisterDate { get; set; }

        public List<Config> Configs { get; set; }

    }
}
