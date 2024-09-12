using ECommMVC.BL.Abstact;
using ECommMVC.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommMVC.BL.Concrete
{
    public class CartManager : ICartService
    {
        public List<CartItem> AddToCart(List<CartItem> cart, CartItem item)
        {
            var existingItem = cart.Find(x => x.ProductID == item.ProductID);
            if (existingItem != null)
            {
                existingItem.Quantity += item.Quantity;
            }
            else
            {
                cart.Add(item);
            }
            return cart;
        }

        public decimal CalculateTotal(List<CartItem> cart)
        {
            decimal total = 0;
            foreach (var item in cart)
            {
                total += item.Price * item.Quantity;
            }
            return total;
        }

        public int GetCartItemCount(List<CartItem> cart)
        {
            return cart.Sum(x => x.Quantity);
        }
    }
}
