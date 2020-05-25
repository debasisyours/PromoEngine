using Microsoft.VisualStudio.TestTools.UnitTesting;
using PromotionsApp.Models;
using PromotionsApp.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace PromotionsApp.UnitTest
{
    [TestClass]
    public class ApplyNormalPriceTests
    {
        ApplyNormalPrice _applyNormalPrice = null;

        [TestInitialize]
        public void InitializeTest()
        {
            _applyNormalPrice = new ApplyNormalPrice();
        }

        [TestMethod]
        public void When_I_Pass_Null_Input_I_Should_Get_Null()
        {
            // Arrange
            InputModel inputModel = null;
            List<SkuItem> skuItems = null;

            // Act
            var output = _applyNormalPrice.ApplyPrice(inputModel, skuItems);

            // Assert
            Assert.IsNull(output);
        }

        [TestMethod]
        public void When_I_Pass_InputModel_With_Sku_Not_Present_In_SkuList_I_Should_Get_Zero_Change_In_Final_Price()
        {
            // Arrange
            decimal originalPrice = 180;

            InputModel inputModel = new InputModel
            {
                InputSkuModels = new List<InputSkuModel>
                {
                    new InputSkuModel
                    {
                        SkuName="A", Quantity=3
                    }
                },
                FinalPrice = originalPrice
            };

            List<SkuItem> skuItems = new List<SkuItem> { 
                new SkuItem
                {
                    Name="B", Price=20
                }
            };

            // Act
            var output = _applyNormalPrice.ApplyPrice(inputModel, skuItems);

            // Assert
            Assert.AreEqual(output.FinalPrice, inputModel.FinalPrice);
        }

        [TestMethod]
        public void When_I_Pass_InputModel_With_Sku_Present_In_SkuList_I_Should_Get_Non_Zero_Change_In_Final_Price()
        {
            // Arrange
            decimal originalPrice = 180;
            decimal skuPrice = 80;
            int quantity = 2;

            InputModel inputModel = new InputModel
            {
                InputSkuModels = new List<InputSkuModel>
                {
                    new InputSkuModel
                    {
                        SkuName="A", Quantity=quantity
                    }
                },
                FinalPrice = originalPrice
            };

            List<SkuItem> skuItems = new List<SkuItem> {
                new SkuItem
                {
                    Name="A", Price=skuPrice
                }
            };

            // Act
            var output = _applyNormalPrice.ApplyPrice(inputModel, skuItems);

            // Assert
            Assert.AreEqual(output.FinalPrice, originalPrice + quantity * skuPrice);
        }
    }
}
