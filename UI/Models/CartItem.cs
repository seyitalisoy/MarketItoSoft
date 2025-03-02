using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entities.Concrete;

namespace UI.Models
{
    public class CartItem
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public decimal Price => Product.Price;  
        public string ProductName => Product.Name;  
    }

}