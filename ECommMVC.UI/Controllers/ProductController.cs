using Microsoft.AspNetCore.Mvc;

namespace ECommMVC.UI.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
