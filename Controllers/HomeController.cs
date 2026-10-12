using System.Diagnostics;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using HelpDesk.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Spotter.Data;

namespace HelpDesk.Controllers;

public class HomeController : Controller
{
    private readonly AppDbContext _context;
    private readonly IPasswordHasher<Cadastro> _passwordHasher;

    public HomeController(AppDbContext context, IPasswordHasher<Cadastro> passwordHasher)
    {
        _context = context;
        _passwordHasher = passwordHasher;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Login(string? returnUrl = null)
    {
        return View(new LoginViewModel { ReturnUrl = returnUrl });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var email = model.Email.Trim();
        var cadastro = await _context.Cadastros
            .FirstOrDefaultAsync(usuario => usuario.Email == email);

        if (cadastro is null)
        {
            ModelState.AddModelError(string.Empty, "E-mail ou senha inválidos.");
            return View(model);
        }

        var passwordVerification = VerificarSenha(cadastro, model.Senha);
        if (passwordVerification == PasswordVerificationResult.Failed)
        {
            ModelState.AddModelError(string.Empty, "E-mail ou senha inválidos.");
            return View(model);
        }

        if (passwordVerification == PasswordVerificationResult.SuccessRehashNeeded)
        {
            cadastro.Senha = _passwordHasher.HashPassword(cadastro, model.Senha);
            await _context.SaveChangesAsync();
        }

        var role = cadastro.User switch
        {
            "Admin" => "Admin",
            "Professor" => "Professor",
            _ => "UsuarioPadrao"
        };
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, cadastro.Id.ToString()),
            new Claim(ClaimTypes.Name, cadastro.Nome),
            new Claim(ClaimTypes.Email, cadastro.Email),
            new Claim(ClaimTypes.Role, role)
        };
        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(identity));

        if (role == "Admin")
            return RedirectToAction("Index_Admin", "Admin");

        if (!string.IsNullOrWhiteSpace(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
            return Redirect(model.ReturnUrl);

        return RedirectToAction("Index", "User");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction(nameof(Index));
    }

    private PasswordVerificationResult VerificarSenha(Cadastro cadastro, string senha)
    {
        if (IsPasswordHash(cadastro.Senha))
            return _passwordHasher.VerifyHashedPassword(cadastro, cadastro.Senha, senha);

        return CryptographicOperations.FixedTimeEquals(
            Encoding.UTF8.GetBytes(cadastro.Senha),
            Encoding.UTF8.GetBytes(senha))
            ? PasswordVerificationResult.SuccessRehashNeeded
            : PasswordVerificationResult.Failed;
    }

    private static bool IsPasswordHash(string senha)
    {
        Span<byte> decoded = stackalloc byte[256];
        return Convert.TryFromBase64String(senha, decoded, out var bytesWritten)
            && bytesWritten > 0
            && decoded[0] is 0 or 1;
    }

    public IActionResult Cadastro()
    {
        return View();
    }

    public IActionResult Verificacao()
    {
        return View();
    }

    public IActionResult Lista()
    {
        return View();
    }

    public IActionResult FazerChamados()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
