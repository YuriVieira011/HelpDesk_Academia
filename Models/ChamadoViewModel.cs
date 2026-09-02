using Microsoft.AspNetCore.Identity;

namespace HelpDesk.Models
{
    // Representa um chamado do sistema
    public class Chamado
    {
        // Propriedades do chamado
        // get -> permite ler o valor
        // set -> permite atribuir um valor
        public int Id { get; set; }
        public required string Titulo { get; set; }
        public required string Descricao { get; set; }
        public string Status { get; set; } = "Aberto";
        public DateTime DataAbertura { get; set; } = DateTime.Now;
        public DateTime? DataFechamento { get; set; }
    }

    public class Usuario : IdentityUser
    {
        // Seus campos personalizados entram aqui (ex: Nome, CPF, etc)
    }
}
