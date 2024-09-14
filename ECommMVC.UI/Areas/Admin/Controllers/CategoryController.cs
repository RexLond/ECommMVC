using ECommMVC.BL.Abstact;
using ECommMVC.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ECommMVC.UI.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;

namespace ECommMVC.UI.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _categoryService.GetAllAsync());
        }

        // Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category, IFormFile image)
        {
            if (category != null)
            {
                if (image != null && image.Length > 0)
                {
                    string imageName = await FileSystem.SaveFileAsync(image, "wwwroot/images/Category");
                    category.Image = imageName;
                }
                await _categoryService.CreateAsync(category);
                return RedirectToAction("Index");
            }
            else
            {
                throw new Exception($"Image upload failed.");
            }
        }

        // Read
        public async Task<IActionResult> Details(int id)
        {
            return View(await _categoryService.GetByIdAsync(id));
        }

        // Update
        public async Task<IActionResult> Edit(int id)
        {
            return View(await _categoryService.GetByIdAsync(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Category category, IFormFile image)
        {
            if (category != null)
            {
                if (image != null && image.Length > 0)
                {
                    string imageName = await FileSystem.SaveFileAsync(image, "wwwroot/images/Category");
                    category.Image = imageName;
                }  
                await _categoryService.UpdateAsync(category);
                return RedirectToAction("Index");
            }
            else
            {
                throw new Exception($"Image upload failed.");
            }
        }

        // Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _categoryService.DeleteAsync(id);
            return Json(new { success = true, message = "Delete success." });
        }
    }
}
