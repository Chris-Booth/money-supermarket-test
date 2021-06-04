using PriceCalculationService.Products;

namespace PriceCalculationService.Discount
{
    public class DiscountInfo
    {
        public decimal ProductsRequired { get; set; }
        public ProductInfo LookupProduct { get; set; }
        public ProductInfo DiscountProduct { get; set; }
        public DiscountType DiscountType { get; set; }
        public decimal DiscountAmount { get; set; }
        public int NumberToDiscount { get; set; }

        public bool ThresholdMet(int productCount)
        {
            return productCount >= ProductsRequired;
        }

        public string GetThresholdProduct()
        {
            return LookupProduct.Name;
        }

        public decimal ApplyDiscount(BasketItemInfo value)
        {
            switch (DiscountType)
            {
                case DiscountType.NumberFree:
                    var discountAmount = value.Count > ProductsRequired ? (value.Count - value.Count % ProductsRequired)/ProductsRequired * DiscountAmount: DiscountAmount;
                    var totalToPayFor = value.Count - discountAmount;
                    if (totalToPayFor < 0)
                        totalToPayFor = 0;
                    return value.Product.Price * totalToPayFor;
                case DiscountType.PercentageOffNumberOfProducts:
                    var productsToDiscount = value.Count < NumberToDiscount ? value.Count : NumberToDiscount;
                    return value.Product.Price * (value.Count - productsToDiscount)
                           + value.Product.Price * productsToDiscount / 100 * DiscountAmount;
                default:
                    return value.Product.Price * value.Count;
            }
        }
    }
}