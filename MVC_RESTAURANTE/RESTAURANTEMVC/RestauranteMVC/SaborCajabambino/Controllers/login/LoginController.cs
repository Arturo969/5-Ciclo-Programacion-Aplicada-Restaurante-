using Microsoft.AspNetCore.Mvc;
using SaborCajabambino.Models;
using SaborCajabambino.Data;

namespace SaborCajabambino.Controllers.login
{
    public class LoginController : Controller
    {
        private readonly RestauranteProgramacionIiContext _context; 
        public LoginController(RestauranteProgramacionIiContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken] 
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var empleado = _context.Empleados
                    .FirstOrDefault(e => e.Usuario == model.Usuario && e.Contrasena == model.Contrasena);

                if (empleado != null)
                {
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "Usuario o contraseña incorrectos.");
            }
            return View(model);
        }
    }
}
