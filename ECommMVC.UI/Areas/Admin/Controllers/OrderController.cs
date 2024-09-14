using ECommMVC.BL.Abstact;
using ECommMVC.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Reflection;

namespace ECommMVC.UI.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class OrderController : Controller
    {
        private IOrderService _orderService;
        private IShipperService _shipperService;
        private IUserService _userService;
        private IPaymentService _paymentService;
        private IOrderDetailService _orderDetailService;

        public OrderController(IOrderService orderService, IShipperService shipperService, IUserService userService, IPaymentService paymentService, IOrderDetailService orderDetailService)
        {
            _orderService = orderService;
            _shipperService = shipperService;
            _userService = userService;
            _paymentService = paymentService;
            _orderDetailService = orderDetailService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _orderService.GetAllWithRefAsync());
        }

        // Create
        public async Task<IActionResult> Create()
        {
            ViewBag.ShipperID = new SelectList(await _shipperService.GetAllAsync(), "ID", "Name");

            var users = await _userService.GetAllAsync();
            ViewBag.UserID = new SelectList(users.Select(u => new {ID = u.ID, FullName = u.FirstName + " " + u.LastName}), "ID", "FullName");
            
            ViewBag.PaymentID = new SelectList(await _paymentService.GetAllAsync(), "ID", "ID");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Order order)
        {
            if (order != null)
            {
                if (order.PaymentID == -1)
                {
                    order.PaymentID = null;
                }
                await _orderService.CreateAsync(order);
                return RedirectToAction("Index");
            }
            else
            {
                throw new Exception($"Data cannot be null.");
            }
        }

        // Read
        public async Task<IActionResult> Details(int id)
        {
            var allOrderDetails = await _orderDetailService.GetAllWithRefAsync();
            var orderDetails = allOrderDetails.Where(x => x.OrderID == id);

            //List<decimal> details = new List<decimal>();
            decimal totalPrice = 0;

            foreach (var detail in orderDetails)
            {
                decimal unitPrice = detail.UnitPrice;
                int quantity = detail.Quantity;
                decimal vat = detail.VAT;
                decimal discount = detail.Discount;

                decimal result = (unitPrice * quantity) * (1 + vat) * (1 - discount);

                //details.Add(result);
                totalPrice += result;
            }
            //ViewBag.Details = details;

            ViewBag.TotalPrice = totalPrice;
            
            ViewBag.OrderDetails = orderDetails;

            return View(await _orderService.GetByIdWithRefAsync(id));
        }

        // Update
        public async Task<IActionResult> Edit(int id)
        {
            // Shipper
            ViewBag.ShipperID = new SelectList(await _shipperService.GetAllAsync(), "ID", "Name");

            // User
            var users = await _userService.GetAllAsync();
            ViewBag.UserID = new SelectList(users.Select(u => new { ID = u.ID, FullName = u.FirstName + " " + u.LastName }), "ID", "FullName");

            // Payment
            ViewBag.PaymentID = new SelectList(await _paymentService.GetAllAsync(), "ID", "ID");

            return View(await _orderService.GetByIdAsync(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Order order)
        {
            if (order != null)
            {
                if (order.PaymentID == -1)
                {
                    order.PaymentID = null;
                }

                // OrderDetails
                decimal totalPrice = 0;
                var orderDetails = (await _orderDetailService.GetAllAsync()).Where(x => x.OrderID == order.ID).ToList();
                foreach (var orderDetail in orderDetails)
                {
                    totalPrice += orderDetail.TotalPrice;
                }
                order.Freight = totalPrice;

                await _orderService.UpdateAsync(order);
                return RedirectToAction("Index");
            }
            else
            {
                throw new Exception($"Data cannot be null.");
            }
        }

        // Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _orderService.DeleteAsync(id);
            return Json(new { success = true, message = "Delete success." });
        }
    }
}
