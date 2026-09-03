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
        public required string Nome { get; set; }
        public required string Email { get; set; }
        public required string Senha { get; set; }
        public required string CEP { get; set; }
        public required string RG { get; set; }
        public string Status { get; set; } = "Aberto";
        public DateTime DataAbertura { get; set; } = DateTime.Now;
        public DateTime? DataFechamento { get; set; }
    }

    public class Usuario : IdentityUser
    {
        // Seus campos personalizados entram aqui (ex: Nome, CPF, etc)
    }
}
