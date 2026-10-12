using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Spotter.Data;

namespace HelpDesk.Controllers;

public class AdminController : Controller
{
    private readonly AppDbContext _context;

    public AdminController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index_Admin()
    {
        var maquinas = await _context.Maquinas.AsNoTracking().ToListAsync();
        return View(maquinas);
    }

    public IActionResult FazerChamados_admin()
    {
        return View();
    }
}