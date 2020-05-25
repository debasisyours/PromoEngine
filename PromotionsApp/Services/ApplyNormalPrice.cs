using PromotionsApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PromotionsApp.Services
{
    public class ApplyNormalPrice
    {
        public InputModel ApplyPrice(InputModel inputModel, List<SkuItem> normalPriceList)
        {
            InputModel output = inputModel;

            if (inputModel != null && inputModel.InputSkuModels != null)
            {
                foreach (var item in inputModel.InputSkuModels)
                {
                    var currentSku = normalPriceList.FirstOrDefault(s => s.Name == item.SkuName);
                    if (currentSku != null)
                    {
                        output.FinalPrice += item.Quantity * currentSku.Price;
                    }
                }
            }

            return output;
        }
    }
}
