using System;
using System.Collections.Generic;

namespace PriceCalculationService.Products
{
    public class Products
    {
        private readonly IDictionary<string, ProductInfo> _products;

        public Products(IDictionary<string, ProductInfo> products)
        {
            _products = products;
        }

        public ProductInfo GetProduct(string name)
        {
            if (_products.TryGetValue(name, out var product))
            {
                return product;
            }

            throw new ProductNotFoundException(name);
        }
    }

    public class ProductNotFoundException : Exception
    {
        public string Name { get; }

        public ProductNotFoundException(string name)
        {
            Name = name;
        }
    }
}