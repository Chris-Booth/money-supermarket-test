using System;
using System.Collections.Generic;
using PriceCalculationService.Products;
using Shouldly;
using Xunit;

namespace PriceCalculationService.Tests
{
    public class ProductsTest
    {
        public static IEnumerable<object[]> Data => new[]
        {
            new object[] {new ProductInfo("Butter", 0.80m)},
            new object[] {new ProductInfo("Milk", 1.15m)},
            new object[] {new ProductInfo("Bread", 1.00m)},
        };

        [MemberData(nameof(Data))]
        [Theory]
        public void ShouldFindKnowProduct(ProductInfo productInfo)
        {
            var products = new Products.Products(new Dictionary<string, ProductInfo>
            {
                [productInfo.Name] = productInfo
            });

            var butter = products.GetProduct(productInfo.Name);
            butter.Name.ShouldBe(productInfo.Name);
            butter.Price.ShouldBe(productInfo.Price);
        }

        [Fact]
        public void ShouldThrowProductNotFoundExceptionWhenProductNotFound()
        {
            var products = new Products.Products(new Dictionary<string, ProductInfo>());
            Action getProduct = () => products.GetProduct("Not A Product");
            getProduct.ShouldThrow<ProductNotFoundException>().Name.ShouldBe("Not A Product");
        }
    }
}