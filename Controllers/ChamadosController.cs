using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HelpDesk.Models;
using HelpDesk.Data;

namespace HelpDesk.Controllers
{
    public class ChamadosController : Controller
    {
        // 🔵 Campo para acessar o banco de dados
        private readonly AppDbContext _context;

        // 🔵 Injeção do DbContext
        public ChamadosController(AppDbContext context)
        {
            _context = context;
        }

        // 📋 LISTAGEM
        public async Task<IActionResult> Lista()
        {
            // 🔵 Busca todos os chamados no banco e converte para lista
            var chamados = await _context.Chamados.ToListAsync();
            return View("Lista", chamados);
        }

        public async Task<IActionResult> Lista_Admin()
        {
            var chamados = await _context.Chamados.ToListAsync();
            return View("Lista_Admin", chamados);
        }

        // Redireciona `/Chamados` para `/Chamados/Lista` para evitar 404
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
            var chamado = await _context.Chamados.FirstOrDefaultAsync(c => c.Id == id);

            if (chamado == null)
                return NotFound();

            return View(chamado);
        }

        // 📝 SALVAR (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Criar(Chamado model, bool origemAdmin = false, bool origemUser = false)
        {
            if (!ModelState.IsValid)
                return View(model);

            var chamado = new Chamado
            {
                Nome = model.Nome,
                Descricao = model.Descricao,
                Status = "Aberto",
                DataAbertura = DateTime.Now,
                DataFechamento = null
            };

            _context.Chamados.Add(chamado);
            await _context.SaveChangesAsync();

            return origemAdmin
                ? RedirectToAction(nameof(Lista))
                : origemUser
                    ? RedirectToAction("Index", "User")
                    : RedirectToAction("Index", "Home");
        }

        // 🔌 API
        [HttpPost]
        [Route("api/chamados")]
        public async Task<IActionResult> CriarViaApi([FromBody] Chamado model)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            var chamado = new Chamado
            {
                Nome = model.Nome,
                Descricao = model.Descricao,
                Status = "Aberto",
                DataAbertura = DateTime.Now,
                DataFechamento = null
            };

            _context.Chamados.Add(chamado);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Detalhes), new { id = chamado.Id }, chamado);
        }

        public IActionResult Sobre()
        {
            return View();
        }
    }
}