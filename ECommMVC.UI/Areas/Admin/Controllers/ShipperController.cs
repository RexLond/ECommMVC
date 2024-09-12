using ECommMVC.BL.Abstact;
using ECommMVC.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ECommMVC.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ShipperController : Controller
    {
        private IShipperService _shipperService;

        public ShipperController(IShipperService shipperService)
        {
            _shipperService = shipperService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _shipperService.GetAllAsync());
        }


        // Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Shipper shipper)
        {
            if (shipper != null)
            {
                await _shipperService.CreateAsync(shipper);
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
            return View(await _shipperService.GetByIdAsync(id));
        }

        // Update
        public async Task<IActionResult> Edit(int id)
        {
            return View(await _shipperService.GetByIdAsync(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Shipper shipper)
        {
            if (shipper != null)
            {
                await _shipperService.UpdateAsync(shipper);
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
            await _shipperService.DeleteAsync(id);
            return Json(new { success = true, message = "Delete success." });
        }
    }
}
