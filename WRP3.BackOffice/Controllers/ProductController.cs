using Microsoft.AspNetCore.Mvc;

namespace WRP3.BackOffice.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
