using System;
using System.ComponentModel.DataAnnotations;

namespace Spotter.Models
{
    public class Maquina
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome do equipamento é obrigatório.")]
        [StringLength(100)]
        [Display(Name = "Nome do Equipamento")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "A marca é obrigatória.")]
        [StringLength(50)]
        public string Marca { get; set; } = string.Empty;

        [Display(Name = "Número de Série / Código")]
        public string? NumeroSerie { get; set; }

        [Display(Name = "Status")]
        public StatusMaquina Status { get; set; } = StatusMaquina.Operacional;

        [Display(Name = "Última Manutenção")]
        [DataType(DataType.Date)]
        public DateTime? UltimaManutencao { get; set; }

        [Display(Name = "Observações")]
        public string? Observacoes { get; set; }
    }

    public enum StatusMaquina
    {
        [Display(Name = "Operacional")]
        Operacional = 1,

        [Display(Name = "Em Manutenção")]
        EmManutencao = 2,

        [Display(Name = "Inoperante")]
        Inoperante = 3
    }
}