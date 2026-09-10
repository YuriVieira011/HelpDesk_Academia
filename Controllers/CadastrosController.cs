using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HelpDesk.Models;
using HelpDesk.Data;

namespace HelpDesk.Controllers
{
    public class CadastrosController : Controller
    {
        // 🔵 Campo para acessar o banco de dados
        private readonly AppDbContext _context;

        // 🔵 Injeção do DbContext
        public CadastrosController(AppDbContext context)
        {
            _context = context;
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
                return View(model);

            var cadastro = new Cadastro
            {
                Nome = model.Nome,
                Email = model.Email,
                Senha = model.Senha,
                CEP = model.CEP,
                RG = model.RG,
                Status = "Aberto",
                DataAbertura = DateTime.Now,
                DataFechamento = null
            };

            _context.Cadastros.Add(cadastro);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Lista));
        }

        // 🔌 API
        [HttpPost]
        [Route("api/cadastros")]
        public async Task<IActionResult> CriarViaApi([FromBody] Cadastro model)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            var cadastro = new Cadastro
            {
                Nome = model.Nome,
                Email = model.Email,
                Senha = model.Senha,
                CEP = model.CEP,
                RG = model.RG,
                Status = "Aberto",
                DataAbertura = DateTime.Now,
                DataFechamento = null
            };

            _context.Cadastros.Add(cadastro);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Detalhes), new { id = cadastro.Id }, cadastro);
        }

        public IActionResult Sobre()
        {
            return View();
        }
    }
}