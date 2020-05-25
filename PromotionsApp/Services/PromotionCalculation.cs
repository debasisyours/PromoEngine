using PromotionsApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PromotionsApp.Services
{
    public class PromotionCalculation
    {
        public InputModel ApplyPromotions(InputModel inputModel, List<Promotion> promotions, List<SkuItem> skuItems)
        {
            foreach(var promotion in promotions)
            {
                while(CanApplyPromotion(inputModel, promotion))
                {
                    inputModel = ApplyPromotionInternal(inputModel, promotion, skuItems);
                }
            }

            return inputModel;
        }

        public bool CanApplyPromotion(InputModel inputModel, Promotion promotion)
        {
            bool success = true;

            for(int loop = 0; loop < promotion.PromotionItems.Count; loop++)
            {
                var currentItem = inputModel.InputSkuModels.FirstOrDefault(s => s.SkuName == promotion.PromotionItems[loop].SkuName);
                if (currentItem != null && currentItem.Quantity>= promotion.PromotionItems[loop].MinQuantity)
                {
                    // looks good
                }
                else
                {
                    success = false;
                    break;
                }
            }

            return success;
        }

        public InputModel ApplyPromotionInternal(InputModel inputModel, Promotion promotion, List<SkuItem> skuItems)
        {
            decimal originalPrice = 0;

            if (promotion.PercentageOff.HasValue)
            {
                for (int loop = 0; loop < promotion.PromotionItems.Count; loop++)
                {
                    var currentItem = inputModel.InputSkuModels.FirstOrDefault(s => s.SkuName == promotion.PromotionItems[loop].SkuName);
                    var skuItem = skuItems.FirstOrDefault(s => s.Name == promotion.PromotionItems[loop].SkuName);
                    
                    if (currentItem != null && skuItem!=null)
                    {
                        currentItem.Quantity -= promotion.PromotionItems[loop].MinQuantity;
                        originalPrice += promotion.PromotionItems[loop].MinQuantity * skuItem.Price;
                    }
                }

                inputModel.FinalPrice += (1 - (promotion.PercentageOff.Value / 100)) * originalPrice;
            }
            else
            {
                for (int loop = 0; loop < promotion.PromotionItems.Count; loop++)
                {
                    var currentItem = inputModel.InputSkuModels.FirstOrDefault(s => s.SkuName == promotion.PromotionItems[loop].SkuName);
                    
                    if (currentItem != null)
                    {
                        currentItem.Quantity -= promotion.PromotionItems[loop].MinQuantity;
                    }
                }

                inputModel.FinalPrice += promotion.PromoPrice.Value;
            }

            return inputModel;
        }
    }
}
