using ECommMVC.BL.Abstact;
using ECommMVC.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ECommMVC.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PaymentController : Controller
    {
        private IPaymentService _paymentService;
        private IOrderService _orderService;
        private IUserService _userService;

        public PaymentController(IPaymentService paymentService, IOrderService orderService, IUserService userService)
        {
            _paymentService = paymentService;
            _orderService = orderService;
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _paymentService.GetAllWithRefAsync());
        }

        // Create
        public async Task<IActionResult> Create()
        {
            var nonPayments = await _orderService.GetAllAsync();
            ViewBag.OrderID = new SelectList(nonPayments.Where(p => p.PaymentID == null), "ID", "ID");

            var users = await _userService.GetAllAsync();
            ViewBag.UserID = new SelectList(users.Select(u => new { ID = u.ID, FullName = u.FirstName + " " + u.LastName }), "ID", "FullName");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Payment payment)
        {
            if (payment != null)
            {
                await _paymentService.CreateAsync(payment);
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
            return View(await _paymentService.GetByIdWithRefAsync(id));
        }

        // Update
        public async Task<IActionResult> Edit(int id)
        {
            var nonPayments = await _orderService.GetAllAsync();
            ViewBag.OrderID = new SelectList(nonPayments.Where(p => p.PaymentID == null), "ID", "ID");

            var users = await _userService.GetAllAsync();
            ViewBag.UserID = new SelectList(users.Select(u => new { ID = u.ID, FullName = u.FirstName + " " + u.LastName }), "ID", "FullName");

            return View(await _paymentService.GetByIdAsync(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Payment payment)
        {
            if (payment != null)
            {
                await _paymentService.UpdateAsync(payment);
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
            await _paymentService.DeleteAsync(id);
            return Json(new { success = true, message = "Delete success." });
        }
    }
}
