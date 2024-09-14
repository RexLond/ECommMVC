using ECommMVC.BL.Abstact;
using ECommMVC.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Printing;

namespace ECommMVC.UI.Controllers
{
    [AllowAnonymous]
    public class ShopController : Controller
    {
        private ICategoryService _categoryService;
        private IProductService _productService;
        private IReviewService _reviewService;

        public ShopController(ICategoryService categoryService, IProductService productService, IReviewService reviewService)
        {
            _categoryService = categoryService;
            _productService = productService;
            _reviewService = reviewService;
        }

        // Shopping Page Index
        [HttpGet("shop/category/{id}/{page?}")]
        public async Task<IActionResult> Index(int id, int? page)
        {
            int pageNumber = page ?? 1 ;

            var category = await _categoryService.GetByIdAsync(id);

            if (category != null || id == 0)
            {
                if (id != null)
                {
                    int pageSize = 18;
                    int totalPage = 0;
                    
                    List<Product> products = new List<Product>();

                    if (id > 0)
                    {
                        products = (await _productService.GetAllAsync())
                            .Where(w => w.CategoryID == id)
                            .OrderBy(o => o.ID)
                            .Skip((pageNumber - 1) * pageSize)
                            .Take(pageSize)
                            .ToList();
                    }
                    else if (id == 0)
                    {
                        products = (await _productService.GetAllAsync())
                            .OrderBy(o => o.ID)
                            .Skip((pageNumber - 1) * pageSize)
                            .Take(pageSize)
                            .ToList();    
                    }

                    totalPage = (int)Math.Ceiling((double)products.Count() / pageSize);
                    if (totalPage < 1)
                        totalPage = 1;

                    ViewBag.PageRange = GetPageRange(totalPage, pageNumber);
                    ViewBag.PageNumber = pageNumber;
                    ViewBag.TotalPage = totalPage;
                    ViewBag.Products = products;

                    if (category != null)
                    {
                        ViewData["Title"] = category.Name + " - Page: " + pageNumber;
                    }
                    else
                    {
                        ViewData["Title"] = "All Categories" + " - Page: " + pageNumber;
                    }
                    
                    return View();
                }
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // Product Details
        public async Task<IActionResult> Details(int id)
        {
            var product = await _productService.GetByIdWithRefAsync(id);
            var reviews = (await _reviewService.GetAllWithRefAsync()).Where(x => x.ProductID == id && x.IsApproved == true);
            if (product != null)
            {
                ViewBag.Reviews = reviews;
                ViewData["Title"] = product.Name;
                return View(product);
            }
            return RedirectToAction("Index", "Home");
        }

        private List<int> GetPageRange(int totalPage, int pageNumber)
        {
            List<int> pageRange = new List<int>();

            if (totalPage <= 3)
            {
                for (int i = 1; i <= totalPage; i++)
                {
                    pageRange.Add(i);
                }
            }
            else
            {
                int startPage = pageNumber - 1;
                int endPage = pageNumber + 1;

                if (pageNumber <= 2)
                {
                    startPage = 1;
                    endPage = 3;
                }
                else
                {
                    startPage = pageNumber - 1;
                    endPage = pageNumber;
                    if ((pageNumber + 1) <= totalPage)
                    {
                        endPage = pageNumber + 1;
                    }
                    else
                    {
                        startPage--;
                    }
                }

                for (int i = startPage; i < endPage + 1; i++)
                {
                    pageRange.Add(i);
                }
            }

            return pageRange;
        }
    }
}
