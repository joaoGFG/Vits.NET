namespace VerdantisModel;

public class ProdutorModel
{
    public int Id { get; set; }

    public required string Nome { get; set; }

    public DateTime DataCadastro { get; set; }

    public int TipoUsuarioId { get; set; }

    public required string Senha { get; set; } 
}
