using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WRP3.BackOffice.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        public IActionResult SignOut()
        {
            return RedirectToAction("Index", "Home");
        }
    }
}
