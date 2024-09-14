using ECommMVC.BL.Abstact;
using ECommMVC.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ECommMVC.UI.Controllers
{
    public class UserOrderController : Controller
    {
        private IOrderService _orderService;
        private IUserService _userService;

        public UserOrderController(IOrderService orderService, IUserService userService)
        {
            _orderService = orderService;
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            if (ModelState.IsValid)
            {
                var claimUserIdStr = User.FindFirst("ID")?.Value;
                if (claimUserIdStr != null)
                {
                    int claimUserId = Convert.ToInt32(claimUserIdStr);
                    var user = await _userService.GetByIdAsync(claimUserId);
                    if (user != null)
                    {
                        if (user.Email == User.FindFirst("Email")?.Value && user.FirstName == User.FindFirst("FirstName")?.Value && user.LastName == User.FindFirst("LastName")?.Value)
                        {
                            return View((await _orderService.GetAllWithRefAsync()).Where(x => x.UserID == user.ID).ToList());
                        }
                    }
                }
            }
            return View();             
        }
    }
}
