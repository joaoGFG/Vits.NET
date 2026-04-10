using Microsoft.AspNetCore.Mvc;
using VerdantisBusiness;
using VerdantisBusiness.DTOs;
using VerdantisUI.Models;

namespace VerdantisUI.Controllers;

/// <summary>
/// Controller para gerenciamento de Produtores
/// Implementa CRUD completo com validações, tratamento de erros e padrões de projeto
/// </summary>
public class ProdutoresController : Controller
{
    private readonly IProdutorService _produtorService;
    private readonly ILogger<ProdutoresController> _logger;

    public ProdutoresController(IProdutorService produtorService, ILogger<ProdutoresController> logger)
    {
        _produtorService = produtorService;
        _logger = logger;
    }

    /// <summary>
    /// GET: Produtores - Lista todos os produtores
    /// </summary>
    public IActionResult Index()
    {
        try
        {
            var produtores = _produtorService.ListarTodos();
            var viewModels = produtores.Select(p => new ProdutorDetailsViewModel
            {
                Id = p.Id,
                Nome = p.Nome,
                DataCadastro = p.DataCadastro,
                TipoUsuarioId = p.TipoUsuarioId
            }).ToList();

            return View(viewModels);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao listar produtores");
            TempData["Error"] = "Erro ao carregar a lista de produtores.";
            return View(new List<ProdutorDetailsViewModel>());
        }
    }

    /// <summary>
    /// GET: Produtores/Details/5 - Exibe detalhes de um produtor
    /// </summary>
    public IActionResult Details(int id)
    {
        try
        {
            var produtor = _produtorService.ObterPorId(id);
            if (produtor == null)
            {
                TempData["Error"] = "Produtor não encontrado.";
                return RedirectToAction(nameof(Index));
            }

            var viewModel = new ProdutorDetailsViewModel
            {
                Id = produtor.Id,
                Nome = produtor.Nome,
                DataCadastro = produtor.DataCadastro,
                TipoUsuarioId = produtor.TipoUsuarioId
            };

            return View(viewModel);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao buscar produtor com ID {Id}", id);
            TempData["Error"] = "Erro ao carregar os detalhes do produtor.";
            return RedirectToAction(nameof(Index));
        }
    }

    /// <summary>
    /// GET: Produtores/Create - Exibe formulário de criação
    /// </summary>
    public IActionResult Create()
    {
        return View(new ProdutorCreateViewModel());
    }

    /// <summary>
    /// POST: Produtores/Create - Processa criação de produtor
    /// Validações: ModelState, Required, StringLength
    /// </summary>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(ProdutorCreateViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(viewModel);
        }

        try
        {
            var dto = new ProdutorCreateDto(viewModel.Nome.Trim(), viewModel.TipoUsuarioId, viewModel.Senha);
            var criado = _produtorService.Criar(dto);

            TempData["Success"] = $"Produtor '{criado.Nome}' cadastrado com sucesso!";
            return RedirectToAction(nameof(Details), new { id = criado.Id });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao criar produtor");
            ModelState.AddModelError("", "Erro ao cadastrar o produtor. Tente novamente.");
            return View(viewModel);
        }
    }

    /// <summary>
    /// GET: Produtores/Edit/5 - Exibe formulário de edição
    /// </summary>
    public IActionResult Edit(int id)
    {
        try
        {
            var produtor = _produtorService.ObterPorId(id);
            if (produtor == null)
            {
                TempData["Error"] = "Produtor não encontrado.";
                return RedirectToAction(nameof(Index));
            }

            var viewModel = new ProdutorEditViewModel
            {
                Id = produtor.Id,
                Nome = produtor.Nome,
                TipoUsuarioId = produtor.TipoUsuarioId,
                DataCadastro = produtor.DataCadastro
            };

            return View(viewModel);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao carregar produtor com ID {Id} para edição", id);
            TempData["Error"] = "Erro ao carregar o produtor para edição.";
            return RedirectToAction(nameof(Index));
        }
    }

    /// <summary>
    /// POST: Produtores/Edit/5 - Processa edição de produtor
    /// Validações: ModelState, ID matching, Required, StringLength
    /// </summary>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, ProdutorEditViewModel viewModel)
    {
        if (id != viewModel.Id)
        {
            TempData["Error"] = "Dados inválidos.";
            return RedirectToAction(nameof(Index));
        }

        if (!ModelState.IsValid)
        {
            return View(viewModel);
        }

        try
        {
            var dto = new ProdutorUpdateDto(viewModel.Id, viewModel.Nome.Trim(), viewModel.TipoUsuarioId);
            var atualizado = _produtorService.Atualizar(dto);

            if (!atualizado)
            {
                TempData["Error"] = "Produtor não encontrado.";
                return RedirectToAction(nameof(Index));
            }

            TempData["Success"] = "Produtor atualizado com sucesso!";
            return RedirectToAction(nameof(Details), new { id = viewModel.Id });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao atualizar produtor com ID {Id}", id);
            ModelState.AddModelError("", "Erro ao atualizar o produtor. Tente novamente.");
            return View(viewModel);
        }
    }

    /// <summary>
    /// GET: Produtores/Delete/5 - Exibe confirmação de exclusão
    /// </summary>
    public IActionResult Delete(int id)
    {
        // PROTEÇÃO: Impedir exclusão do usuário padrão
        if (id == 1)
        {
            TempData["Error"] = "O usuário 'Usuário Exemplo' não pode ser excluído pois é o usuário padrão do sistema.";
            return RedirectToAction(nameof(Index));
        }

        try
        {
            var produtor = _produtorService.ObterPorId(id);
            if (produtor == null)
            {
                TempData["Error"] = "Produtor não encontrado.";
                return RedirectToAction(nameof(Index));
            }

            var viewModel = new ProdutorDeleteViewModel
            {
                Id = produtor.Id,
                Nome = produtor.Nome,
                DataCadastro = produtor.DataCadastro,
                TipoUsuarioId = produtor.TipoUsuarioId
            };

            return View(viewModel);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao carregar produtor com ID {Id} para exclusão", id);
            TempData["Error"] = "Erro ao carregar o produtor para exclusão.";
            return RedirectToAction(nameof(Index));
        }
    }

    /// <summary>
    /// POST: Produtores/Delete/5 - Processa exclusão de produtor
    /// </summary>
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        try
        {
            var removido = _produtorService.Remover(id);
            if (!removido)
            {
                TempData["Error"] = "Produtor não encontrado.";
                return RedirectToAction(nameof(Index));
            }

            TempData["Success"] = "Produtor removido com sucesso!";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao remover produtor com ID {Id}", id);
            TempData["Error"] = "Erro ao remover o produtor. Tente novamente.";
            return RedirectToAction(nameof(Delete), new { id });
        }
    }

    /// <summary>
    /// GET: Produtores/Search - Pesquisa produtores com filtros, paginação e ordenação
    /// </summary>
    public IActionResult Search(string? nome, int page = 1, int pageSize = 10, string sortBy = "Nome", bool ascending = true)
    {
        try
        {
            var result = _produtorService.Search(nome, page, pageSize, sortBy, ascending);

            var viewModel = new ProdutorSearchViewModel
            {
                Nome = nome,
                Page = page,
                PageSize = pageSize,
                SortBy = sortBy,
                Ascending = ascending,
                Resultados = result.Items.Select(p => new ProdutorDetailsViewModel
                {
                    Id = p.Id,
                    Nome = p.Nome,
                    DataCadastro = p.DataCadastro,
                    TipoUsuarioId = p.TipoUsuarioId
                }).ToList(),
                TotalItems = result.TotalItems,
                TotalPages = result.TotalPages,
                HasNextPage = result.HasNextPage,
                HasPreviousPage = result.HasPreviousPage
            };

            return View(viewModel);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao pesquisar produtores com termo '{Nome}'", nome);
            TempData["Error"] = "Erro ao realizar a pesquisa.";
            return View(new ProdutorSearchViewModel());
        }
    }
}