using ECommMVC.BL.Abstact;
using ECommMVC.Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ECommMVC.UI.Controllers
{
    public class CartController : Controller
    {
        private ICartService _cartService;
        private IProductService _productService;
        private const string CartCookieKey = "cart";

        public CartController(ICartService cartService, IProductService productService)
        {
            _cartService = cartService;
            _productService = productService;
        }

        public IActionResult Index()
        {
            var cart = GetCartFromCookies();
            return View(cart);
        }

        public async Task<IActionResult> AddToCart(int id, int quantity)
        {
            var product = await _productService.GetByIdAsync(id);
            var item = new CartItem
            {
                ProductID = id,
                ProductName = product.Name,
                Quantity = quantity,
                Price = product.TotalPrice,
            };

            var cart = GetCartFromCookies();
            cart = _cartService.AddToCart(cart, item);
            SaveCartToCookies(cart);

            return RedirectToAction("Index");
        }

        public IActionResult ClearCart()
        {
            Response.Cookies.Delete(CartCookieKey);
            return RedirectToAction("Index");
        }

        public IActionResult Checkout()
        {
            var cart = GetCartFromCookies();
            // Siparis olusturma islemleri
            return View();
        }


        private List<CartItem> GetCartFromCookies()
        {
            var cartJson = Request.Cookies[CartCookieKey];
            if (string.IsNullOrEmpty(cartJson))
            {
                return new List<CartItem>();
            }
            return JsonConvert.DeserializeObject<List<CartItem>>(cartJson); // ???
        }

        private void SaveCartToCookies(List<CartItem> cart)
        {
            var cartJson = JsonConvert.SerializeObject(cart);
            Response.Cookies.Append(CartCookieKey, cartJson, new CookieOptions
            {
                Expires = System.DateTime.Now.AddDays(7),
                HttpOnly = true
            });
        }
    }
}
