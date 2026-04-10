using System.ComponentModel.DataAnnotations;

namespace VerdantisUI.Models;

/// <summary>
/// ViewModel para criação de produtor
/// </summary>
public class ProdutorCreateViewModel
{
    [Required(ErrorMessage = "O nome é obrigatório")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome deve ter entre 3 e 100 caracteres")]
    [Display(Name = "Nome do Produtor")]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "O tipo de usuário é obrigatório")]
    [Display(Name = "Tipo de Usuário")]
    [Range(1, int.MaxValue, ErrorMessage = "Selecione um tipo de usuário válido")]
    public int TipoUsuarioId { get; set; }

    [Required(ErrorMessage = "A senha é obrigatória")]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "A senha deve ter no mínimo 6 caracteres")]
    [DataType(DataType.Password)]
    [Display(Name = "Senha")]
    public string Senha { get; set; } = string.Empty;
}

/// <summary>
/// ViewModel para edição de produtor
/// </summary>
public class ProdutorEditViewModel
{
    [Required]
    public int Id { get; set; }

    [Required(ErrorMessage = "O nome é obrigatório")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome deve ter entre 3 e 100 caracteres")]
    [Display(Name = "Nome do Produtor")]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "O tipo de usuário é obrigatório")]
    [Display(Name = "Tipo de Usuário")]
    [Range(1, int.MaxValue, ErrorMessage = "Selecione um tipo de usuário válido")]
    public int TipoUsuarioId { get; set; }

    [Display(Name = "Data de Cadastro")]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = false)]
    public DateTime DataCadastro { get; set; }
}

/// <summary>
/// ViewModel para exibição de detalhes do produtor
/// </summary>
public class ProdutorDetailsViewModel
{
    public int Id { get; set; }

    [Display(Name = "Nome do Produtor")]
    public string Nome { get; set; } = string.Empty;

    [Display(Name = "Data de Cadastro")]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = false)]
    public DateTime DataCadastro { get; set; }

    [Display(Name = "Tipo de Usuário")]
    public int TipoUsuarioId { get; set; }
}

/// <summary>
/// ViewModel para pesquisa de produtores com paginação
/// </summary>
public class ProdutorSearchViewModel
{
    [Display(Name = "Buscar por Nome")]
    [StringLength(100, ErrorMessage = "O termo de busca deve ter no máximo 100 caracteres")]
    public string? Nome { get; set; }

    [Display(Name = "Página")]
    [Range(1, int.MaxValue, ErrorMessage = "Página deve ser maior que 0")]
    public int Page { get; set; } = 1;

    [Display(Name = "Itens por Página")]
    [Range(5, 100, ErrorMessage = "Deve exibir entre 5 e 100 itens por página")]
    public int PageSize { get; set; } = 10;

    [Display(Name = "Ordenar Por")]
    public string SortBy { get; set; } = "Nome";

    [Display(Name = "Ordem Crescente")]
    public bool Ascending { get; set; } = true;

    // Resultados da pesquisa
    public List<ProdutorDetailsViewModel> Resultados { get; set; } = new();
    
    [Display(Name = "Total de Itens")]
    public int TotalItems { get; set; }
    
    [Display(Name = "Total de Páginas")]
    public int TotalPages { get; set; }
    
    public bool HasNextPage { get; set; }
    public bool HasPreviousPage { get; set; }
}

/// <summary>
/// ViewModel para confirmação de exclusão de produtor
/// </summary>
public class ProdutorDeleteViewModel
{
    public int Id { get; set; }

    [Display(Name = "Nome do Produtor")]
    public string Nome { get; set; } = string.Empty;

    [Display(Name = "Data de Cadastro")]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}")]
    public DateTime DataCadastro { get; set; }

    [Display(Name = "Tipo de Usuário")]
    public int TipoUsuarioId { get; set; }
}