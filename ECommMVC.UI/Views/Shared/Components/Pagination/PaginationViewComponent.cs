using ECommMVC.BL.Abstact;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ECommMVC.UI.Views.Shared.Components.Pagination
{
    public class PaginationViewComponent : ViewComponent
    {
        public PaginationViewComponent()
        {
            
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
