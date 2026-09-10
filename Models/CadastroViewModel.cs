using Microsoft.AspNetCore.Identity;

namespace HelpDesk.Models
{
    public class Cadastro
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
}
