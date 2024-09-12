using ECommMVC.BL.Abstact;
using ECommMVC.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommMVC.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CouponController : Controller
    {
        private ICouponService _couponService;

        public CouponController(ICouponService couponService)
        {
            _couponService = couponService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _couponService.GetAllAsync());
        }

        // Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Coupon coupon)
        {
            if (coupon != null)
            {
                await _couponService.CreateAsync(coupon);
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
            return View(await _couponService.GetByIdAsync(id));
        }

        // Update
        public async Task<IActionResult> Edit(int id)
        {
            return View(await _couponService.GetByIdAsync(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Coupon coupon)
        {
            if (coupon != null)
            {
                await _couponService.UpdateAsync(coupon);
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
            await _couponService.DeleteAsync(id);
            return Json(new { success = true, message = "Delete success." });
        }
    }
}
