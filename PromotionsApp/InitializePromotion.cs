using PromotionsApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PromotionsApp
{
    public class InitializePromotion
    {
        public List<SkuItem> InitializeSku()
        {
            List<SkuItem> skuItems = new List<SkuItem>();

            skuItems.Add(new SkuItem
            {
                Name = "A",
                Price = 50
            });

            skuItems.Add(new SkuItem
            {
                Name = "B",
                Price = 30
            });

            skuItems.Add(new SkuItem
            {
                Name = "C",
                Price = 20
            });

            skuItems.Add(new SkuItem
            {
                Name = "D",
                Price = 15
            });

            return skuItems;
        }

        public List<Promotion> InitializePromo()
        {
            List<Promotion> promotions = new List<Promotion>();

            // 3 'A' for 130
            promotions.Add(new Promotion
            {
                PromotionItems = new List<PromotionItem>
                {
                    new PromotionItem
                    {
                        SkuName="A", MinQuantity=3
                    }
                },
                PromoPrice = 130
            });

            // 2 'B' for 45
            promotions.Add(new Promotion
            {
                PromotionItems = new List<PromotionItem>
                {
                    new PromotionItem
                    {
                        SkuName="B", MinQuantity=2
                    }
                },
                PromoPrice = 45
            });

            // 'C' and 'D' for 30
            promotions.Add(new Promotion
            {
                PromotionItems = new List<PromotionItem>
                {
                    new PromotionItem
                    {
                        SkuName="C", MinQuantity=1
                    },
                    new PromotionItem
                    {
                        SkuName="D", MinQuantity=1
                    }
                },
                PromoPrice = 30
            });

            return promotions;
        }
    }
}
