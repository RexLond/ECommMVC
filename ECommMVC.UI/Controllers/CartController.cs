using ECommMVC.BL.Abstact;
using ECommMVC.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace ECommMVC.UI.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private ICartService _cartService;
        private IProductService _productService;
        private IUserService _userService;
        private IOrderService _orderService;
        private IOrderDetailService _orderDetailService;
        private IPaymentService _paymentService;
        private const string CartCookieKey = "cart";

        public CartController(ICartService cartService, IProductService productService, IUserService userService, IOrderService orderService, IOrderDetailService orderDetailService, IPaymentService paymentService)
        {
            _cartService = cartService;
            _productService = productService;
            _userService = userService;
            _orderService = orderService;
            _orderDetailService = orderDetailService;
            _paymentService = paymentService;
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

        public async Task<IActionResult> Checkout()
        {
            var claimUserIdStr = User.FindFirst("ID")?.Value;
            if (claimUserIdStr != null)
            {
                int claimUserId = Convert.ToInt32(claimUserIdStr);
                var user = await _userService.GetByIdAsync(claimUserId);
                if (user != null)
                {
                    if (user.Email == User.FindFirst("Email")?.Value && user.FirstName == User.FindFirst("FirstName")?.Value && user.LastName == User.FindFirst("LastName")?.Value)
                    {

                        var cart = GetCartFromCookies();

                        List<Product> products = new List<Product>();
                        foreach (var item in cart)
                        {
                            products.Add(await _productService.GetByIdAsync(item.ProductID));
                        }

                        ViewBag.Cart = cart;
                        ViewBag.Address = $"{user.Address} {user.PostalCode} {user.Region} {user.Region} {user.City}/{user.Country}";
                        ViewBag.Products = products;
                        ViewBag.User = user;

                        ViewBag.PaymentTypes = Enum.GetValues(typeof(PaymentType)).Cast<PaymentType>().Select(e => new SelectListItem
                        {
                            Value = ((int)e).ToString(),
                            Text = e.ToString()
                        }).ToList();
                    }
                    else
                    {
                        Unauthorized();
                    }
                }
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Checkout(Checkout checkout)
        {
            if (ModelState.IsValid)
            {
                var claimUserIdStr = User.FindFirst("ID")?.Value;
                if (claimUserIdStr != null)
                {
                    int claimUserId = Convert.ToInt32(claimUserIdStr);
                    var user = await _userService.GetByIdAsync(claimUserId);
                    if (user != null)
                    {
                        if (user.Email == User.FindFirst("Email")?.Value && user.FirstName == User.FindFirst("FirstName")?.Value && user.LastName == User.FindFirst("LastName")?.Value)
                        {

                            var cart = GetCartFromCookies();

                            decimal totalPrice = 0.00m;
                            decimal totalVAT = 0.00m;
                            decimal totalDiscount = 0.00m;
                            decimal totalPaid = 0.00m;

                            List<Product> products = new List<Product>();
                            
                            foreach (var item in cart)
                            {
                                Product product = await _productService.GetByIdAsync(item.ProductID);
                                products.Add(product);

                                totalPrice += (product.UnitPrice * item.Quantity);
                                totalVAT += (product.UnitPrice * product.VAT);
                                totalDiscount += (product.UnitPrice * product.Discount);
                                totalPaid += ((product.UnitPrice * item.Quantity) * (1 + product.VAT) * (1 - product.Discount));
                            }

                            Order order = new Order
                            {
                                Freight = totalPaid,
                                OrderDate = DateTime.Now,
                                ShipAddress = user.Address,
                                ShipCity = user.City,
                                ShipRegion = user.Region,
                                ShipPostalCode = user.PostalCode,
                                ShipCountry = user.Country,
                                User = user
                            };

                            Payment payment = new Payment
                            {
                                PaymentMethod = checkout.PaymentType.ToString(),
                                Freight = totalPaid,
                                User = user
                            };

                            List<OrderDetail> orderDetails = new List<OrderDetail>();
                            foreach (var item in products)
                            {
                                orderDetails.Add(new OrderDetail
                                {
                                    Order = order,
                                    UnitPrice = item.UnitPrice,
                                    Quantity = cart.First(x => x.ProductID == item.ID).Quantity,
                                    Discount = item.Discount,
                                    VAT = item.VAT,
                                    TotalPrice = item.TotalPrice,
                                    Product = item
                                });
                            }

                            //order.Payment = payment;

                            var savedData = await _orderService.CreateOrderWithPaymentAsync(order, payment);

                            if (savedData == null)
                            {
                                throw new Exception("Order and Payment save not completed!");
                            }
                            
                            List<OrderDetail> createdOrderDetails = await _orderDetailService.CreateOrderDetailByListAsync(orderDetails);

                            if ((createdOrderDetails.Count < 0))
                            {
                                throw new Exception("Order not completed!");
                            }

                            return RedirectToAction("OrderCompleted");
                        }
                        else
                        {
                            Unauthorized();
                        }
                    }
                }
            }

            // Hatalıysa tekrar formu göster
            ViewBag.PaymentTypes = Enum.GetValues(typeof(PaymentType)).Cast<PaymentType>().Select(e => new SelectListItem
            {
                Value = ((int)e).ToString(),
                Text = e.ToString()
            }).ToList();

            return RedirectToAction("Checkout");
        }

        public IActionResult OrderCompleted()
        {
            return View();
        }

        private List<CartItem> GetCartFromCookies()
        {
            var cartJson = Request.Cookies[CartCookieKey];
            if (string.IsNullOrEmpty(cartJson))
            {
                return new List<CartItem>();
            }
            return JsonConvert.DeserializeObject<List<CartItem>>(cartJson);
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
