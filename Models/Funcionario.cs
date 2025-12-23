using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrabalhoApi.Models
{
    public class Funcionario
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome do funcionário é obrigatório.")]
        public string? Nome { get; set; }

        [Required(ErrorMessage = "O cargo é obrigatório.")]
        public string? Cargo { get; set; }

        [Required(ErrorMessage = "O departamento é obrigatório.")]
        public string? Departamento { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "O salário deve ser um valor positivo.")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Salario { get; set; }

        [Required(ErrorMessage = "A data de admissão é obrigatória.")]
        public DateTime DataAdmissao { get; set; }

        // --- ADICIONE ESTAS DUAS LINHAS ABAIXO ---
        [Required(ErrorMessage = "O ID da empresa é obrigatório.")]
        public int EmpresaId { get; set; } // Chave Estrangeira
    }
}
