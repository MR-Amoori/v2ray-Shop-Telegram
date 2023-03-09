using System;
using System.ComponentModel.DataAnnotations;

namespace v2ray_Shop_Telegram.Models
{
    public class Config
    {
        [Key]
        public int ConfigId { get; set; }

        public string UID { get; set; }

        public String ExpryDate { get; set; }
        
        public DateTime RegisterDate { get; set; }

        public string Protocol { get; set; }

        public int TotalTraffic { get; set; }

        public int UsageTraffic { get; set; }
       
        public string Link { get; set; }


        public Account Account { get; set; }
    }
}
