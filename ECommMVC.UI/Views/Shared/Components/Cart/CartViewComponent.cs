using ECommMVC.BL.Abstact;
using ECommMVC.Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ECommMVC.UI.Views.Shared.Components.Cart
{
    public class CartViewComponent : ViewComponent
    {
        private ICartService _cartService;

        public CartViewComponent(ICartService cartService)
        {
            _cartService = cartService;
        }

        public IViewComponentResult Invoke()
        {
            var cartJson = HttpContext.Request.Cookies["cart"];
            var cart = string.IsNullOrEmpty(cartJson) ? new List<CartItem>() : JsonConvert.DeserializeObject<List<CartItem>>(cartJson);
            var itemCount = _cartService.GetCartItemCount(cart);
            return View("Default", itemCount);
        }
    }
}
