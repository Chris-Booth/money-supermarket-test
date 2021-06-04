using System.Collections.Generic;
using System.Linq;
using PriceCalculationService.Discount;
using PriceCalculationService.Products;

namespace PriceCalculationService
{
    public sealed class Basket
    {
        private readonly Dictionary<string, BasketItemInfo> _products;
        private readonly Discounts _discounts;

        public Basket(IEnumerable<ProductInfo> products, IEnumerable<DiscountInfo> discounts = default)
        {
            _products = products.ToDictionary(item => item.Name, item => new BasketItemInfo(item, 0));
            _discounts = new Discounts(discounts);
        }

        public void Add(ProductInfo product)
        {
            _products[product.Name].Count++;
        }

        public void Remove(ProductInfo product)
        {
            var basketInfo = _products[product.Name];
            if (basketInfo.Count >= 0)
            {
                basketInfo.Count = 0;
            }
            else
            {
                basketInfo.Count++;
            }
        }

        public decimal GetTotal()
        {
            var total = 0m;
            foreach (var (_, basketItemInfo) in _products)
            {
                var discountInfo = _discounts.GetDiscountInfo(basketItemInfo.Product);
                var thresholdProduct = discountInfo?.GetThresholdProduct();
                if (discountInfo != default
                    && _products.TryGetValue(thresholdProduct, out var thresholdBasketItem)
                    && discountInfo.ThresholdMet(thresholdBasketItem.Count))
                {
                    total += discountInfo.ApplyDiscount(basketItemInfo);
                }
                else
                {
                    total += basketItemInfo.Product.Price * basketItemInfo.Count;
                }
            }

            return total;
        }
    }

    public class BasketItemInfo
    {
        public ProductInfo Product { get; }
        public int Count { get; set; }

        public BasketItemInfo(ProductInfo product, int count)
        {
            Product = product;
            Count = count;
        }
    }
}