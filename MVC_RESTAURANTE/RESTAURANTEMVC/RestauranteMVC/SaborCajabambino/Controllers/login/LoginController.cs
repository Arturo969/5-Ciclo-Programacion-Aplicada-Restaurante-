using Microsoft.AspNetCore.Mvc;

namespace SaborCajabambino.Controllers.login
{
    public class LoginController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
    }
}
