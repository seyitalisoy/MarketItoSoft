using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entities.Concrete;

namespace UI.Models
{
    public class Cart
    {
        public List<CartItem> Items { get; set; } = new List<CartItem>();

        public void AddItem(Product product, int quantity)
        {
            var existingItem = Items.FirstOrDefault(i => i.ProductId == product.Id);
            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                Items.Add(new CartItem
                {
                    ProductId = product.Id,
                    Product = product,  
                    Quantity = quantity
                });
            }
        }


        public void RemoveItem(int productId)
        {
            Items.RemoveAll(i => i.ProductId == productId);
        }

        public decimal TotalPrice()
        {
            return Items.Sum(i => i.Price * i.Quantity);
        }
    }
}