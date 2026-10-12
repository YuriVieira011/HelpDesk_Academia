using Microsoft.EntityFrameworkCore;
using Spotter.Models;
using HelpDesk.Models;

namespace Spotter.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Maquina> Maquinas { get; set; }
        public DbSet<Cadastro> Cadastros { get; set; }
        public DbSet<Chamado> Chamados { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Maquina>()
                .ToTable("maquinas")
                .Property(m => m.Status)
                .HasConversion<int>()
                .HasDefaultValue(Spotter.Models.StatusMaquina.Operacional);

        }
    }
}