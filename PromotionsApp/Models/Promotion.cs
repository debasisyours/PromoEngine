using System;
using System.Collections.Generic;
using System.Text;

namespace PromotionsApp.Models
{
    public class Promotion
    {
        public int Id { get; set; }
        public List<PromotionItem> PromotionItems { get; set; }
        public decimal? PromoPrice { get; set; }
        public decimal? PercentageOff { get; set; }
    }
}
