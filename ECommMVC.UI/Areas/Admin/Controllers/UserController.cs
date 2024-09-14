using ECommMVC.BL.Abstact;
using ECommMVC.Entities;
using ECommMVC.UI.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommMVC.UI.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class UserController : Controller
    {
        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _userService.GetAllAsync());
        }

        // Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(User user, IFormFile photo)
        {
            if (user != null)
            {
                if (await _userService.CheckMail(user.Email) == false)
                {
                    if (photo != null && photo.Length > 0)
                    {
                        string imageName = await FileSystem.SaveFileAsync(photo, "wwwroot/images/User");
                        user.Photo = imageName;
                    }

                    await _userService.CreateAsync(user);
                    return RedirectToAction("Index");
                }
                else
                {
                    throw new Exception("This email is already.");
                }
            }
            else
            {
                throw new Exception($"User data null.");
            }
        }

        // Read
        public async Task<IActionResult> Details(int id)
        {
            return View(await _userService.GetByIdAsync(id));
        }

        // Update
        public async Task<IActionResult> Edit(int id)
        {
            return View(await _userService.GetByIdAsync(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(User user, IFormFile photo)
        {
            if (user != null)
            {
                if (photo != null && photo.Length > 0)
                {
                    string imageName = await FileSystem.SaveFileAsync(photo, "wwwroot/images/User");
                    user.Photo = imageName;
                }
                await _userService.UpdateAsync(user);
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
            await _userService.DeleteAsync(id);
            return Json(new { success = true, message = "Delete success." });
        }
    }
}
