using ECommMVC.BL.Abstact;
using ECommMVC.Entities;
using Microsoft.AspNetCore.Mvc;
using ECommMVC.UI.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ECommMVC.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private IProductService _productService;
        private ICategoryService _categoryService;

        public ProductController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _productService.GetAllWithRefAsync());
        }

        // Create
        public async Task<IActionResult> Create()
        {
            ViewBag.CategoryID = new SelectList(await _categoryService.GetAllAsync(), "ID", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product, IFormFile image)
        {
            if (product != null)
            {
                if (image != null && image.Length > 0)
                {
                    string imageName = await FileSystem.SaveFileAsync(image, "wwwroot/images/Product");
                    product.Image = imageName;
                }
                product.TotalPrice = (product.UnitPrice) * (1 + product.VAT) * (1 - product.Discount);

                await _productService.CreateAsync(product);
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
            return View(await _productService.GetByIdWithRefAsync(id));
        }

        // Update
        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.CategoryID = new SelectList(await _categoryService.GetAllAsync(), "ID", "Name");
            return View(await _productService.GetByIdAsync(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Product product, IFormFile image)
        {
            if (product != null)
            {
                if (image != null && image.Length > 0)
                {
                    string imageName = await FileSystem.SaveFileAsync(image, "wwwroot/images/Product");
                    product.Image = imageName;
                }
                product.TotalPrice = (product.UnitPrice) * (1 + product.VAT) * (1 - product.Discount);

                await _productService.UpdateAsync(product);
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
            await _productService.DeleteAsync(id);
            return Json(new { success = true, message = "Delete success." });
        }
    }
}
