namespace TrabalhoApi.DTOs
{
    public class FuncionarioDTO
    {
        public string Nome { get; set; }
        public string Cargo { get; set; }
        public string Departamento { get; set; }
        public decimal Salario { get; set; }
        public DateTime DataAdmissao { get; set; }
        public int EmpresaId { get; set; } // Adicione aqui também!
    }
}