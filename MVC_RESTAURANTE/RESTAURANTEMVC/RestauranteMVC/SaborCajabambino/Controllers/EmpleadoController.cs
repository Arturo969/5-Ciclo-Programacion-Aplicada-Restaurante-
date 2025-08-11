using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SaborCajabambino.Data;
using SaborCajabambino.Models;

namespace SaborCajabambino.Controllers
{
    public class EmpleadoController : Controller
    {
        private readonly RestauranteProgramacionIiContext _context;

        public EmpleadoController(RestauranteProgramacionIiContext context)
        {
            _context = context;
        }

        // GET: Empleado
        public async Task<IActionResult> Index()
        {
            return View(await _context.Empleados.ToListAsync());
        }

        // GET: Empleado/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empleado = await _context.Empleados
                .FirstOrDefaultAsync(m => m.IdEmpleado == id);
            if (empleado == null)
            {
                return NotFound();
            }

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return PartialView("_Details", empleado); // Retorna una vista parcial para solicitudes AJAX
            }
            // si no es AJAX, devolver vista completa
            return View(empleado);
        }

        // GET: Empleado/Create
        public IActionResult Create()
        {
            var roles = _context.Empleados.Select(e => e.Rol).Distinct().ToList();
            ViewData["Roles"] = new SelectList(roles);
            ViewData["Turnos"] = new SelectList(new[] { "Completo", "Medio tiempo" });
            ViewData["Estados"] = new SelectList(new[] { "Activo", "Vacaciones", "Despedido" });
            return View();
        }

        // POST: Empleado/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdEmpleado,NombreCompleto,Dni,FechaNacimiento,Direccion,Telefono,CorreoElectronico,Rol,Turno,FechaContratacion,Salario,Estado,Usuario,Contrasena")] Empleado empleado)
        {
            if (ModelState.IsValid)
            {
                _context.Add(empleado);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var roles = _context.Empleados.Select(e => e.Rol).Distinct().ToList();
            ViewData["Roles"] = new SelectList(roles);
            ViewData["Turnos"] = new SelectList(new[] { "Completo", "Medio tiempo" });
            ViewData["Estados"] = new SelectList(new[] { "Activo", "Vacaciones", "Despedido" });
            return View(empleado);
        }

        // GET: Empleado/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empleado = await _context.Empleados.FindAsync(id);
            if (empleado == null)
            {
                return NotFound();
            }
            var roles = await _context.Empleados.Select(e => e.Rol).Distinct().ToListAsync();
            ViewData["Roles"] = new SelectList(roles, empleado.Rol);
            ViewData["Turnos"] = new SelectList(new[] { "Completo", "Medio tiempo" }, empleado.Turno);
            ViewData["Estados"] = new SelectList(new[] { "Activo", "Vacaciones", "Despedido" }, empleado.Estado);
            return View(empleado);
        }

        // POST: Empleado/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdEmpleado,NombreCompleto,Dni,FechaNacimiento,Direccion,Telefono,CorreoElectronico,Rol,Turno,FechaContratacion,Salario,Estado,Usuario,Contrasena")] Empleado empleado)
        {
            if (id != empleado.IdEmpleado)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(empleado);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index)); // Redirigir a la acción Index después de guardar los cambios 
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmpleadoExists(empleado.IdEmpleado))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            var roles = _context.Empleados.Select(e => e.Rol).Distinct().ToList();
            ViewData["Roles"] = new SelectList(roles, empleado.Rol);
            ViewData["Turnos"] = new SelectList(new[] { "Completo", "Medio tiempo" }, empleado.Turno);
            ViewData["Estados"] = new SelectList(new[] { "Activo", "Vacaciones", "Despedido" }, empleado.Estado);
            return View(empleado);
        }

        // GET: Empleado/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empleado = await _context.Empleados
                .FirstOrDefaultAsync(m => m.IdEmpleado == id);
            if (empleado == null)
            {
                return NotFound();
            }

            return View(empleado);
        }

        // POST: Empleado/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var empleado = await _context.Empleados.FindAsync(id);
            if (empleado != null)
            {
                _context.Empleados.Remove(empleado);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // POST: Empleado/Buscar
        [HttpGet]
        public async Task<IActionResult> Buscar(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return Json(await _context.Empleados.ToListAsync());
            }

            var empleados = await _context.Empleados
                .Where(e => e.NombreCompleto.Contains(searchTerm) ||
                            e.Dni.Contains(searchTerm) ||
                            e.Rol.Contains(searchTerm) ||
                            (e.Telefono != null && e.Telefono.Contains(searchTerm)))
                .ToListAsync();

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return Json(empleados);
            }

            return View("Index", empleados);
        }


        private bool EmpleadoExists(int id)
        {
            return _context.Empleados.Any(e => e.IdEmpleado == id);
        }
    }
}
