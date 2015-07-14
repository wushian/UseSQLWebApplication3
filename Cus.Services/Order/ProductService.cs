using Cus.Business.Order;
using Cus.Interfaces.Order;
using Cus.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cus.Services.Order
{
    public class ProductService
    {
        private IProduct _Product = null;
        public ProductService(IProduct Product)
        {
            _Product = Product;
        }

        public object GetProductPriceByProductID(int ProductID)
        {
            return _Product.GetProductPriceByProductID(ProductID);
        }

        public IEnumerable<Products> GetProducts()
        {
            return _Product.GetProducts();
        }
    }
}
