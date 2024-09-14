using ECommMVC.BL.Abstact;
using ECommMVC.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ECommMVC.UI.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class ReviewController : Controller
    {
        private IReviewService _reviewService;
        private IProductService _productService;
        private IUserService _userService;

        public ReviewController(IReviewService reviewService, IProductService productService, IUserService userService)
        {
            _reviewService = reviewService;
            _productService = productService;
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _reviewService.GetAllWithRefAsync());
        }

        // Create
        public async Task<IActionResult> Create()
        {
            var products = await _productService.GetAllAsync();
            ViewBag.ProductID = new SelectList(products, "ID", "Name");

            var users = await _userService.GetAllAsync();
            ViewBag.UserID = new SelectList(users.Select(u => new { ID = u.ID, FullName = u.FirstName + " " + u.LastName }), "ID", "FullName");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Review review)
        {
            if (review != null)
            {
                await _reviewService.CreateAsync(review);
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
            return View(await _reviewService.GetByIdWithRefAsync(id));
        }

        // Update
        public async Task<IActionResult> Edit(int id)
        {
            var products = await _productService.GetAllAsync();
            ViewBag.ProductID = new SelectList(products, "ID", "Name");

            var users = await _userService.GetAllAsync();
            ViewBag.UserID = new SelectList(users.Select(u => new { ID = u.ID, FullName = u.FirstName + " " + u.LastName }), "ID", "FullName");

            return View(await _reviewService.GetByIdAsync(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Review review)
        {
            if (review != null)
            {
                await _reviewService.UpdateAsync(review);
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
            await _reviewService.DeleteAsync(id);
            return Json(new { success = true, message = "Delete success." });
        }
    }
}
