using ECommMVC.BL.Abstact;
using ECommMVC.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ECommMVC.UI.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class TicketController : Controller
    {
        private ITicketService _ticketService;
        private IUserService _userService;

        public TicketController(ITicketService ticketService, IUserService userService)
        {
            _ticketService = ticketService;
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _ticketService.GetAllWithRefAsync());
        }

        // Create
        public async Task<IActionResult> Create()
        {
            var users = await _userService.GetAllAsync();
            ViewBag.UserID = new SelectList(users.Select(u => new { ID = u.ID, FullName = u.FirstName + " " + u.LastName }), "ID", "FullName");


            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Ticket ticket)
        {
            if (ticket != null)
            {
                await _ticketService.CreateAsync(ticket);
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
            return View(await _ticketService.GetByIdWithRefAsync(id));
        }

        // Update
        public async Task<IActionResult> Edit(int id)
        {
            var users = await _userService.GetAllAsync();
            ViewBag.UserID = new SelectList(users.Select(u => new { ID = u.ID, FullName = u.FirstName + " " + u.LastName }), "ID", "FullName");

            return View(await _ticketService.GetByIdAsync(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Ticket ticket)
        {
            if (ticket != null)
            {
                await _ticketService.UpdateAsync(ticket);
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
            await _ticketService.DeleteAsync(id);
            return Json(new { success = true, message = "Delete success." });
        }
    }
}
