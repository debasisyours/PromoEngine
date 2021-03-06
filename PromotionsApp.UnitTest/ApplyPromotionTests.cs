using Microsoft.VisualStudio.TestTools.UnitTesting;
using PromotionsApp.Models;
using PromotionsApp.Services;
using System.Collections.Generic;
using System.Linq;

namespace PromotionsApp.UnitTest
{
    [TestClass]
    public class ApplyPromotionTests
    {
        PromotionCalculation _promotionCalculation = null;
        List<SkuItem> _skuList = null;

        [TestInitialize]
        public void InitializeTest()
        {
            _promotionCalculation = new PromotionCalculation();
            _skuList = new List<SkuItem>
            {
                new SkuItem
                {
                    Name="A", Price = 50
                },
                new SkuItem
                {
                    Name="B", Price = 30
                },
                new SkuItem
                {
                    Name="C", Price=20
                },
                new SkuItem
                {
                    Name="D", Price=15
                }
            };
        }

        [TestMethod]
        public void When_I_Call_CanApplyPromotion_With_InputModel_Not_Having_Required_Item_I_Should_Get_False()
        {
            // Arrange
            InputModel inputModel = new InputModel
            {
                InputSkuModels = new List<InputSkuModel>
                {
                    new InputSkuModel
                    {
                        SkuName="B", Quantity=2
                    },
                    new InputSkuModel
                    {
                        SkuName="C", Quantity=3
                    }
                }
            };

            Promotion promotion = new Promotion
            {
                PromotionItems = new List<PromotionItem>
                {
                    new PromotionItem
                    {
                        SkuName="A", MinQuantity=3
                    }
                },
                PromoPrice = 80
            };

            // Act
            var canApply = _promotionCalculation.CanApplyPromotion(inputModel, promotion);

            // Assert
            Assert.IsFalse(canApply);
        }

        [TestMethod]
        public void When_I_Call_CanApplyPromotion_With_InputModel_Having_Required_Item_With_Lesser_Quantity_I_Should_Get_False()
        {
            // Arrange
            InputModel inputModel = new InputModel
            {
                InputSkuModels = new List<InputSkuModel>
                {
                    new InputSkuModel
                    {
                        SkuName="B", Quantity=2
                    },
                    new InputSkuModel
                    {
                        SkuName="C", Quantity=3
                    }
                }
            };

            Promotion promotion = new Promotion
            {
                PromotionItems = new List<PromotionItem>
                {
                    new PromotionItem
                    {
                        SkuName="B", MinQuantity=3
                    }
                },
                PromoPrice = 80
            };

            // Act
            var canApply = _promotionCalculation.CanApplyPromotion(inputModel, promotion);

            // Assert
            Assert.IsFalse(canApply);
        }

        [TestMethod]
        public void When_I_Call_CanApplyPromotion_With_InputModel_Having_Required_Item_With_Required_Quantity_I_Should_Get_True()
        {
            // Arrange
            InputModel inputModel = new InputModel
            {
                InputSkuModels = new List<InputSkuModel>
                {
                    new InputSkuModel
                    {
                        SkuName="B", Quantity=2
                    },
                    new InputSkuModel
                    {
                        SkuName="C", Quantity=3
                    }
                }
            };

            Promotion promotion = new Promotion
            {
                PromotionItems = new List<PromotionItem>
                {
                    new PromotionItem
                    {
                        SkuName="B", MinQuantity=2
                    }
                },
                PromoPrice = 80
            };

            // Act
            var canApply = _promotionCalculation.CanApplyPromotion(inputModel, promotion);

            // Assert
            Assert.IsTrue(canApply);
        }

        [TestMethod]
        public void When_I_Call_ApplyPromotionInternal_With_A_Applicable_Promotion_Then_PromotionMinimum_Quantity_Should_Be_Reduced_From_Model()
        {
            // Arrange
            InputModel inputModel = new InputModel
            {
                InputSkuModels = new List<InputSkuModel>
                {
                    new InputSkuModel
                    {
                        SkuName="B", Quantity=2
                    },
                    new InputSkuModel
                    {
                        SkuName="C", Quantity=3
                    }
                }
            };

            Promotion promotion = new Promotion
            {
                PromotionItems = new List<PromotionItem>
                {
                    new PromotionItem
                    {
                        SkuName="B", MinQuantity=2
                    }
                },
                PromoPrice = 80
            };

            // Act
            var output = _promotionCalculation.ApplyPromotionInternal(inputModel, promotion, _skuList);

            // Assert
            Assert.AreEqual(inputModel.InputSkuModels.FirstOrDefault(s => s.SkuName == "B").Quantity, 0);
        }

        [TestMethod]
        public void When_I_Call_ApplyPromotionInternal_With_A_Applicable_Promotion_Then_FinalPrice_Should_Be_Increased_By_PromoPrice()
        {
            // Arrange
            decimal originalPrice = 180;
            InputModel inputModel = new InputModel
            {
                InputSkuModels = new List<InputSkuModel>
                {
                    new InputSkuModel
                    {
                        SkuName="B", Quantity=2
                    },
                    new InputSkuModel
                    {
                        SkuName="C", Quantity=3
                    }
                },
                FinalPrice = originalPrice
            };

            Promotion promotion = new Promotion
            {
                PromotionItems = new List<PromotionItem>
                {
                    new PromotionItem
                    {
                        SkuName="B", MinQuantity=2
                    }
                },
                PromoPrice = 80
            };

            // Act
            var output = _promotionCalculation.ApplyPromotionInternal(inputModel, promotion, _skuList);

            // Assert
            Assert.AreEqual(inputModel.FinalPrice, originalPrice+promotion.PromoPrice);
        }
    }
}
