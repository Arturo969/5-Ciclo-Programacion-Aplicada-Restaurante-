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
    public class ProductoIngredienteController : Controller
    {
        private readonly RestauranteProgramacionIiContext _context;

        public ProductoIngredienteController(RestauranteProgramacionIiContext context)
        {
            _context = context;
        }

        // GET: ProductoIngrediente
        public async Task<IActionResult> Index()
        {
            var restauranteProgramacionIiContext = _context.ProductoIngredientes.Include(p => p.IdItemNavigation).Include(p => p.IdProductoNavigation);
            return View(await restauranteProgramacionIiContext.ToListAsync());
        }

        // GET: ProductoIngrediente/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productoIngrediente = await _context.ProductoIngredientes
                .Include(p => p.IdItemNavigation)
                .Include(p => p.IdProductoNavigation)
                .FirstOrDefaultAsync(m => m.IdProducto == id);
            if (productoIngrediente == null)
            {
                return NotFound();
            }

            return View(productoIngrediente);
        }

        // GET: ProductoIngrediente/Create
        public IActionResult Create()
        {
            ViewData["IdItem"] = new SelectList(_context.Inventarios, "IdItem", "IdItem");
            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "IdProducto");
            return View();
        }

        // POST: ProductoIngrediente/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdProducto,IdItem,Cantidad")] ProductoIngrediente productoIngrediente)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productoIngrediente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdItem"] = new SelectList(_context.Inventarios, "IdItem", "IdItem", productoIngrediente.IdItem);
            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "IdProducto", productoIngrediente.IdProducto);
            return View(productoIngrediente);
        }

        // GET: ProductoIngrediente/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productoIngrediente = await _context.ProductoIngredientes.FindAsync(id);
            if (productoIngrediente == null)
            {
                return NotFound();
            }
            ViewData["IdItem"] = new SelectList(_context.Inventarios, "IdItem", "IdItem", productoIngrediente.IdItem);
            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "IdProducto", productoIngrediente.IdProducto);
            return View(productoIngrediente);
        }

        // POST: ProductoIngrediente/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdProducto,IdItem,Cantidad")] ProductoIngrediente productoIngrediente)
        {
            if (id != productoIngrediente.IdProducto)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productoIngrediente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductoIngredienteExists(productoIngrediente.IdProducto))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdItem"] = new SelectList(_context.Inventarios, "IdItem", "IdItem", productoIngrediente.IdItem);
            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "IdProducto", productoIngrediente.IdProducto);
            return View(productoIngrediente);
        }

        // GET: ProductoIngrediente/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productoIngrediente = await _context.ProductoIngredientes
                .Include(p => p.IdItemNavigation)
                .Include(p => p.IdProductoNavigation)
                .FirstOrDefaultAsync(m => m.IdProducto == id);
            if (productoIngrediente == null)
            {
                return NotFound();
            }

            return View(productoIngrediente);
        }

        // POST: ProductoIngrediente/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productoIngrediente = await _context.ProductoIngredientes.FindAsync(id);
            if (productoIngrediente != null)
            {
                _context.ProductoIngredientes.Remove(productoIngrediente);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductoIngredienteExists(int id)
        {
            return _context.ProductoIngredientes.Any(e => e.IdProducto == id);
        }
    }
}
