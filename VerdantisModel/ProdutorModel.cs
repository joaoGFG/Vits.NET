namespace VerdantisModel;

public class ProdutorModel
{
    public string? Id { get; set; }
    public required string Nome { get; set; }
    public required string Propriedade { get; set; }
    public string? Localizacao { get; set; }
    public string? TipoCultura { get; set; }
    public int TamanhoHectares { get; set; }

    public ProdutorModel()
    {
        Id = Guid.NewGuid().ToString();
    }
}
