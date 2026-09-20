using Microsoft.AspNetCore.Mvc;

namespace HelpDesk.Controllers;

public class AdminController : Controller
{
    public IActionResult Index_Admin()
    {
        return View();
    }

    public IActionResult FazerChamados_admin()
    {
        return View();
    }
}