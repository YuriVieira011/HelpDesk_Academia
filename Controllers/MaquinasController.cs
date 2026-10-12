using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Spotter.Data;
using Spotter.Models;

namespace Spotter.Controllers
{
    public class MaquinasController : Controller
    {
        private readonly AppDbContext _context;

        public MaquinasController(AppDbContext context)
        {
            _context = context;
        }

        // Permite visualização para Admin, Professor e Usuário Padrão
        [Authorize(Roles = "Admin,Professor,UsuarioPadrao")]
        public async Task<IActionResult> Index(string? searchString, StatusMaquina? statusFiltro)
        {
            var query = _context.Maquinas.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                query = query.Where(m => m.Nome.Contains(searchString) || m.Marca.Contains(searchString));
            }

            if (statusFiltro.HasValue)
            {
                query = query.Where(m => m.Status == statusFiltro.Value);
            }

            ViewData["CurrentFilter"] = searchString;
            ViewData["CurrentStatus"] = statusFiltro;

            return View(await query.ToListAsync());
        }

        // Apenas Admin e Professor podem criar ou editar equipamentos
        [Authorize(Roles = "Admin,Professor")]
        public IActionResult Create() => View("NovoEquipamento");

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Professor")]
        public async Task<IActionResult> Create(Maquina maquina)
        {
            if (ModelState.IsValid)
            {
                _context.Add(maquina);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View("NovoEquipamento", maquina);
        }

        [Authorize(Roles = "Admin,Professor")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var maquina = await _context.Maquinas.FindAsync(id);
            if (maquina == null) return NotFound();
            return View("Editar", maquina);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Professor")]
        public async Task<IActionResult> Edit(int id, Maquina maquina)
        {
            if (id != maquina.Id) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(maquina);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View("Editar", maquina);
        }

        // Apenas Administradores podem excluir equipamentos
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var maquina = await _context.Maquinas.FirstOrDefaultAsync(m => m.Id == id);
            if (maquina == null) return NotFound();

            return View(maquina);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var maquina = await _context.Maquinas.FindAsync(id);
            if (maquina != null)
            {
                _context.Maquinas.Remove(maquina);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}