namespace PriceCalculationService.Products
{
    public class ProductInfo
    {
        public ProductInfo(string name, decimal price)
        {
            Name = name;
            Price = price;
        }
        
        public string Name { get; }
        public decimal Price { get; }
    }
}