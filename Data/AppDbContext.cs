using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using HelpDesk.Models;

namespace HelpDesk.Data
{
    public class AppDbContext : IdentityDbContext<Usuario>
    {
        // Construtor que recebe as opções de configuração do DbContext
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // Representa a tabela Chamados no banco
        public DbSet<Chamado> Chamados => Set<Chamado>();
        public DbSet<Cadastro> Cadastros => Set<Cadastro>();
    }
}