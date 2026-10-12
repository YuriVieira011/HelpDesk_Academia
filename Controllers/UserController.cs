using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Spotter.Data;

namespace HelpDesk.Controllers;

public class UserController : Controller
{
    private readonly AppDbContext _context;

    public UserController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var maquinas = await _context.Maquinas.AsNoTracking().ToListAsync();
        return View(maquinas);
    }

    public IActionResult FazerChamados()
    {
        return View();
    }
}