using System.ComponentModel.DataAnnotations;

public class Empresa
{
    public int Id { get; set; }

    [Required(ErrorMessage = "O nome da empresa é obrigatório")]
    public string Nome { get; set; }

    [Required(ErrorMessage = "O CNPJ é obrigatório")]
    [RegularExpression(@"^\d{14}$", ErrorMessage = "O CNPJ deve conter exatamente 14 dígitos numéricos")]
    public string Cnpj { get; set; }

    public string? Setor { get; set; }
    public string? Endereco { get; set; }
    public DateTime DataFundacao { get; set; }
}
