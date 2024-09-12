using ECommMVC.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommMVC.BL.Abstact
{
    public interface ICartService
    {
        List<CartItem> AddToCart(List<CartItem> cart, CartItem item);
        decimal CalculateTotal(List<CartItem> cart);
        int GetCartItemCount(List<CartItem> cart);
    }
}
