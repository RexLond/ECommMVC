using ECommMVC.BL.Abstact;
using ECommMVC.Entities;
using ECommMVC.UI.Areas.Admin.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ECommMVC.UI.Controllers
{
    [AllowAnonymous]
    public class SecurityController : Controller
    {
        private IUserService _userService;

        public SecurityController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Login()
        {
            if (!User.Identity.IsAuthenticated)
                return View();
            else
                return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            User onLoginUser = await _userService.GetUserByMailAndPassword(email, password);

            if (onLoginUser != null)
            {
                var claims = new List<Claim>()
                {
                    new Claim("ID", onLoginUser.ID.ToString()),
                    new Claim("FirstName", onLoginUser.FirstName),
                    new Claim("LastName", onLoginUser.LastName),
                    new Claim("Email", onLoginUser.Email),
                    new Claim(ClaimTypes.Role, onLoginUser.Role ?? "User")
                };

                var userIdentity = new ClaimsIdentity(claims, "Security");
                ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
                HttpContext.SignInAsync(principal);

                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        public IActionResult Register()
        {
            if (!User.Identity.IsAuthenticated)
                return View();
            else
                return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(User user, IFormFile photo)
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
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    throw new Exception("This email is already.");
                }
            }
            else
            {
                throw new Exception($"Image upload failed.");
            }
        }

        public IActionResult LogOut()
        {
            if(User.Identity.IsAuthenticated)
            {
                HttpContext.SignOutAsync();
            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
