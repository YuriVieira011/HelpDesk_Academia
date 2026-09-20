using Microsoft.AspNetCore.Mvc;

namespace HelpDesk.Controllers;

public class UserController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult FazerChamados()
    {
        return View();
    }
}