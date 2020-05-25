using System;
using System.Collections.Generic;
using System.Text;

namespace PromotionsApp.Models
{
    public class InputModel
    {
        public List<InputSkuModel> InputSkuModels { get; set; }
        public decimal FinalPrice { get; set; }

        public InputModel()
        {
            InputSkuModels = new List<InputSkuModel>();
        }
    }
}
