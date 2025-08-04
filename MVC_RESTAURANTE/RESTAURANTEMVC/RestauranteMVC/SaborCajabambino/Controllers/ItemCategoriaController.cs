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
    public class ItemCategoriaController : Controller
    {
        private readonly RestauranteProgramacionIiContext _context;

        public ItemCategoriaController(RestauranteProgramacionIiContext context)
        {
            _context = context;
        }

        // GET: ItemCategoria
        public async Task<IActionResult> Index()
        {
            return View(await _context.ItemCategoria.ToListAsync());
        }

        // GET: ItemCategoria/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemCategorium = await _context.ItemCategoria
                .FirstOrDefaultAsync(m => m.IdItemCategoria == id);
            if (itemCategorium == null)
            {
                return NotFound();
            }

            return View(itemCategorium);
        }

        // GET: ItemCategoria/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ItemCategoria/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdItemCategoria,Categoria,Descripcion")] ItemCategorium itemCategorium)
        {
            if (ModelState.IsValid)
            {
                _context.Add(itemCategorium);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(itemCategorium);
        }

        // GET: ItemCategoria/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemCategorium = await _context.ItemCategoria.FindAsync(id);
            if (itemCategorium == null)
            {
                return NotFound();
            }
            return View(itemCategorium);
        }

        // POST: ItemCategoria/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdItemCategoria,Categoria,Descripcion")] ItemCategorium itemCategorium)
        {
            if (id != itemCategorium.IdItemCategoria)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(itemCategorium);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemCategoriumExists(itemCategorium.IdItemCategoria))
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
            return View(itemCategorium);
        }

        // GET: ItemCategoria/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemCategorium = await _context.ItemCategoria
                .FirstOrDefaultAsync(m => m.IdItemCategoria == id);
            if (itemCategorium == null)
            {
                return NotFound();
            }

            return View(itemCategorium);
        }

        // POST: ItemCategoria/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var itemCategorium = await _context.ItemCategoria.FindAsync(id);
            if (itemCategorium != null)
            {
                _context.ItemCategoria.Remove(itemCategorium);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemCategoriumExists(int id)
        {
            return _context.ItemCategoria.Any(e => e.IdItemCategoria == id);
        }
    }
}
