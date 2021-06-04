using System.Collections.Generic;
using System.Linq;
using PriceCalculationService.Products;

namespace PriceCalculationService.Discount
{
    public class Discounts
    {
        private readonly Dictionary<ProductInfo, DiscountInfo> _discounts;

        public Discounts(IEnumerable<DiscountInfo> discounts)
        {
            _discounts = discounts?.ToDictionary(item => item.DiscountProduct, item => item) ??
                         new Dictionary<ProductInfo, DiscountInfo>();
        }

        public DiscountInfo GetDiscountInfo(ProductInfo product)
        {
            return _discounts.TryGetValue(product, out var discountInfo) ? discountInfo : default;
        }
    }
}