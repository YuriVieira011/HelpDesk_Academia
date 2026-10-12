using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using HelpDesk.Models;
using Spotter.Data;

namespace HelpDesk.Controllers
{
    public class CadastrosController : Controller
    {
        // 🔵 Campo para acessar o banco de dados
        private readonly AppDbContext _context;
        private readonly IPasswordHasher<Cadastro> _passwordHasher;

        // 🔵 Injeção do DbContext
        public CadastrosController(AppDbContext context, IPasswordHasher<Cadastro> passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        // 📋 LISTAGEM
        public async Task<IActionResult> Lista()
        {
            // 🔵 Busca todos os cadastros no banco e converte para lista
            var cadastros = await _context.Cadastros.ToListAsync();
            return View("Lista", cadastros);
        }

        // Redireciona `/Cadastros` para `/Cadastros/Lista` para evitar 404
        public IActionResult Index()
        {
            return RedirectToAction(nameof(Lista));
        }

        // 📋 LISTA (compatibilidade com view Lista.cshtml)
        /*public async Task<IActionResult> Lista()
        {
            var chamados = await _context.Chamados.ToListAsync();
            return View("Lista", chamados);
        }*/

        // 🔎 DETALHES
        public async Task<IActionResult> Detalhes(int id)
        {
            // 🔵 Busca o chamado pelo ID
            var cadastro = await _context.Cadastros.FirstOrDefaultAsync(c => c.Id == id);

            if (cadastro == null)
                return NotFound();

            return View(cadastro);
        }

        // 📝 SALVAR (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Criar(Cadastro model)
        {
            if (!ModelState.IsValid)
                return View("~/Views/Home/Cadastro.cshtml", model);

            model.Email = model.Email.Trim();
            if (await _context.Cadastros.AnyAsync(cadastro => cadastro.Email == model.Email))
            {
                ModelState.AddModelError(nameof(model.Email), "Já existe um cadastro com este e-mail.");
                return View("~/Views/Home/Cadastro.cshtml", model);
            }

            var cadastro = new Cadastro
            {
                Nome = model.Nome,
                Email = model.Email,
                Senha = _passwordHasher.HashPassword(model, model.Senha),
                CEP = model.CEP,
                RG = model.RG,
                User = "Usuário",
                DataAbertura = DateTime.Now,
            };

            _context.Cadastros.Add(cadastro);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "User");
        }

        // 🔌 API
        [HttpPost]
        [Route("api/cadastros")]
        public async Task<IActionResult> CriarViaApi([FromBody] Cadastro model)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            model.Email = model.Email.Trim();
            if (await _context.Cadastros.AnyAsync(cadastro => cadastro.Email == model.Email))
                return Conflict(new { message = "Já existe um cadastro com este e-mail." });

            var cadastro = new Cadastro
            {
                Nome = model.Nome,
                Email = model.Email,
                Senha = _passwordHasher.HashPassword(model, model.Senha),
                CEP = model.CEP,
                RG = model.RG,
                User = "Usuário",
                DataAbertura = DateTime.Now,
            };

            _context.Cadastros.Add(cadastro);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Detalhes), new { id = cadastro.Id }, new
            {
                cadastro.Id,
                cadastro.Nome,
                cadastro.Email,
                cadastro.CEP,
                cadastro.RG,
                cadastro.User,
                cadastro.DataAbertura
            });
        }

        public IActionResult Sobre()
        {
            return View();
        }
    }
}