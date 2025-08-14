using Microsoft.AspNetCore.Mvc;
using SaborCajabambino.Models;
using SaborCajabambino.Data;

namespace SaborCajabambino.Controllers.login
{
    public class LoginController : Controller
    {
        private readonly RestauranteProgramacionIiContext _context; // Asume el nombre de tu contexto de base de datos

        // Constructor para inyectar el contexto de la base de datos
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
        [ValidateAntiForgeryToken] // Medida de seguridad recomendada
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Busca al empleado por usuario y contraseña.
                // IMPORTANTE: Por seguridad, las contraseñas DEBEN estar hasheadas en un entorno real.
                // Aquí usamos una comparación simple para el ejemplo.
                var empleado = _context.Empleados
                    .FirstOrDefault(e => e.Usuario == model.Usuario && e.Contrasena == model.Contrasena);

                if (empleado != null)
                {
                    // Si las credenciales son correctas, podrías guardar el Id del empleado en la sesión
                    // o en una cookie de autenticación para mantener la sesión abierta.
                    // Por simplicidad, solo te redirigimos al inicio.
                    // Aquí podrías agregar la lógica de autenticación real (ej. SignInAsync).
                    return RedirectToAction("Index", "Home");
                }

                // Si las credenciales son incorrectas
                ModelState.AddModelError(string.Empty, "Usuario o contraseña incorrectos.");
            }

            // Si el modelo no es válido o las credenciales son incorrectas,
            // regresa a la vista de login con los errores.
            return View(model);
        }
    }
}
