using PromotionsApp.Models;
using PromotionsApp.Services;
using System;

namespace PromotionsApp
{
    class Program
    {
        public static void Main(string[] args)
        {
            // Loading Sku and Promotions
            InitializePromotion initializePromotion = new InitializePromotion();
            var skuList = initializePromotion.InitializeSku();
            var promotions = initializePromotion.InitializePromo();

            // Getting User inputs
            InputModel inputModel = GetInputsFromUser();

            // Applying promotions
            PromotionCalculation calculation = new PromotionCalculation();
            var output = calculation.ApplyPromotions(inputModel, promotions, skuList);

            // Applying normal price for remaining items
            ApplyNormalPrice applyNormal = new ApplyNormalPrice();
            output = applyNormal.ApplyPrice(output, skuList);
            Console.WriteLine($"Price: {output.FinalPrice}");

            Console.ReadLine();
        }

        private static InputModel GetInputsFromUser()
        {
            InputModel inputModel = new InputModel();

            Console.WriteLine("Count of A:");
            var quantityA = Console.ReadLine();
            int quantity;
            bool success = int.TryParse(quantityA, out quantity);
            if (success && quantity>0)
            {
                inputModel.InputSkuModels.Add(new InputSkuModel
                {
                    SkuName = "A",
                    Quantity = quantity
                });
            }

            Console.WriteLine("Count of B:");
            var quantityB = Console.ReadLine();
            success = int.TryParse(quantityB, out quantity);
            if (success && quantity > 0)
            {
                inputModel.InputSkuModels.Add(new InputSkuModel
                {
                    SkuName = "B",
                    Quantity = quantity
                });
            }

            Console.WriteLine("Count of C:");
            var quantityC = Console.ReadLine();
            success = int.TryParse(quantityC, out quantity);
            if (success && quantity > 0)
            {
                inputModel.InputSkuModels.Add(new InputSkuModel
                {
                    SkuName = "C",
                    Quantity = quantity
                });
            }

            Console.WriteLine("Count of D:");
            var quantityD = Console.ReadLine();
            success = int.TryParse(quantityD, out quantity);
            if (success && quantity > 0)
            {
                inputModel.InputSkuModels.Add(new InputSkuModel
                {
                    SkuName = "D",
                    Quantity = quantity
                });
            }

            return inputModel;
        }

        
    }
}
