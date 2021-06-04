using PriceCalculationService.Discount;
using PriceCalculationService.Products;
using Shouldly;
using Xunit;

namespace PriceCalculationService.Tests
{
    public class BasketTests
    {
        [Fact]
        public void GivenTheBasketHas1Bread1ButterAnd1Milk_When_ITotalTheBasketThenTheTotalShouldBe_2_95()
        {
            var butter = new ProductInfo("Butter", 0.80m);
            var milk = new ProductInfo("Milk", 1.15m);
            var bread = new ProductInfo("Bread", 1.00m);
            var products = new[] {bread, butter, milk};

            var basket = new Basket(products);

            basket.Add(butter);
            basket.Add(milk);
            basket.Add(bread);

            var total = basket.GetTotal();
            total.ShouldBe(2.95m);
        }

        [Fact]
        public void GivenTheBasketHas2ButterAnd2Bread_WhenITotalTheBasketThenTheTotalShouldBe_3_10()
        {
            var butter = new ProductInfo("Butter", 0.80m);
            var milk = new ProductInfo("Milk", 1.15m);
            var bread = new ProductInfo("Bread", 1.00m);
            var products = new[] {bread, butter, milk};

            var basket = new Basket(products, new[]
            {
                new DiscountInfo
                {
                    //Buy 2 Butter and get a Bread at 50% off 
                    DiscountProduct = bread,
                    LookupProduct = butter,
                    ProductsRequired = 2,
                    DiscountAmount = 50,
                    DiscountType = DiscountType.PercentageOffNumberOfProducts,
                    NumberToDiscount = 1
                }
            });

            basket.Add(butter);
            basket.Add(butter);
            basket.Add(bread);
            basket.Add(bread);

            var total = basket.GetTotal();
            total.ShouldBe(3.10m);
        }

        [Fact]
        public void GivenTheBasketHas4Milk_WhenITotalTheBasketThenTheTotalShouldBe_3_45()
        {
            var butter = new ProductInfo("Butter", 0.80m);
            var milk = new ProductInfo("Milk", 1.15m);
            var bread = new ProductInfo("Bread", 1.00m);
            var products = new[] {bread, butter, milk};

            var basket = new Basket(products, new[]
            {
                new DiscountInfo
                {
                    // Buy 3 Milk and get the 4th milk for free
                    DiscountProduct = milk,
                    LookupProduct = milk,
                    ProductsRequired = 3,
                    DiscountAmount = 1,
                    DiscountType = DiscountType.NumberFree,
                    NumberToDiscount = 1
                }
            });

            basket.Add(milk);
            basket.Add(milk);
            basket.Add(milk);
            basket.Add(milk);

            var total = basket.GetTotal();
            total.ShouldBe(3.45m);
        }

        [Fact]
        public void GivenTheBasketHas2Butter1BreadAnd8Milk_WhenITotalTheBasketThenTheTotalShouldBe_9_00()
        {
            var butter = new ProductInfo("Butter", 0.80m);
            var milk = new ProductInfo("Milk", 1.15m);
            var bread = new ProductInfo("Bread", 1.00m);
            var products = new[] {bread, butter, milk};

            var basket = new Basket(products, new[]
            {
                new DiscountInfo
                {
                    //Buy 2 Butter and get a Bread at 50% off 
                    DiscountProduct = bread,
                    LookupProduct = butter,
                    ProductsRequired = 2,
                    DiscountAmount = 50,
                    DiscountType = DiscountType.PercentageOffNumberOfProducts,
                    NumberToDiscount = 1
                },
                new DiscountInfo
                {
                    // Buy 3 Milk and get the 4th milk for free
                    DiscountProduct = milk,
                    LookupProduct = milk,
                    ProductsRequired = 3,
                    DiscountAmount = 1,
                    DiscountType = DiscountType.NumberFree,
                    NumberToDiscount = 1
                }
            });

            basket.Add(butter);
            basket.Add(butter);
            
            basket.Add(bread);
            
            basket.Add(milk);
            basket.Add(milk);
            basket.Add(milk);
            basket.Add(milk);
            basket.Add(milk);
            basket.Add(milk);
            basket.Add(milk);
            basket.Add(milk);

            var total = basket.GetTotal();
            total.ShouldBe(9.00m);
        }
    }
}