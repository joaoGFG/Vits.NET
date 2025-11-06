using Microsoft.AspNetCore.Mvc;
using VerdantisBusiness;
using VerdantisBusiness.DTOs;
using VerdantisApi.DTOs;

namespace VerdantisApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProdutoresController(IProdutorService produtorService) : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        var produtores = produtorService.ListarTodos();
        if (produtores.Count == 0) return NoContent();

        var hateoasProdutores = produtores.Select(AddHateoasLinks).ToList();
        return Ok(hateoasProdutores);
    }

    [HttpGet("{id:int}")]
    public IActionResult Get(int id)
    {
        var produtor = produtorService.ObterPorId(id);
        if (produtor == null) return NotFound();

        var hateoasProdutor = AddHateoasLinks(produtor);
        return Ok(hateoasProdutor);
    }

    [HttpGet("search")]
    public IActionResult Search(
        [FromQuery] string? nome,
        [FromQuery] int page = 1,
        [FromQuery] int size = 10,
        [FromQuery] string sortBy = "Nome",
        [FromQuery] bool ascending = true)
    {
        var result = produtorService.Search(nome, page, size, sortBy, ascending);
        
        var hateoasItems = result.Items.Select(AddHateoasLinks).ToList();
        
        var links = new List<LinkDto>
        {
            new(Url.Action(nameof(Search), new { nome, page, size, sortBy, ascending })!, "self", "GET")
        };

        if (result.HasNextPage)
        {
            links.Add(new(Url.Action(nameof(Search), new { nome, page = page + 1, size, sortBy, ascending })!, "next", "GET"));
        }

        if (result.HasPreviousPage)
        {
            links.Add(new(Url.Action(nameof(Search), new { nome, page = page - 1, size, sortBy, ascending })!, "prev", "GET"));
        }

        var pagedResult = new PagedHateoasResultDto<ProdutorHateoasDto>(
            hateoasItems,
            result.TotalItems,
            result.Page,
            result.Size,
            result.TotalPages,
            result.HasNextPage,
            result.HasPreviousPage,
            links
        );

        return Ok(pagedResult);
    }

    [HttpPost]
    public IActionResult Post([FromBody] ProdutorCreateDto produtor)
    {
        if (string.IsNullOrWhiteSpace(produtor.Nome))
            return BadRequest("Nome é obrigatório.");
        if (produtor.TipoUsuarioId <= 0)
            return BadRequest("TipoUsuarioId é obrigatório.");

        var criado = produtorService.Criar(produtor);
        var hateoasCriado = AddHateoasLinks(criado);
        
        return CreatedAtAction(nameof(Get), new { id = criado.Id }, hateoasCriado);
    }

    [HttpPut]
    public IActionResult Put([FromBody] ProdutorUpdateDto produtor)
    {
        if (produtor.Id <= 0) return BadRequest("Id é obrigatório.");
        if (string.IsNullOrWhiteSpace(produtor.Nome)) return BadRequest("Nome é obrigatório.");
        if (produtor.TipoUsuarioId <= 0) return BadRequest("TipoUsuarioId é obrigatório.");

        return produtorService.Atualizar(produtor) ? NoContent() : NotFound();
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        return produtorService.Remover(id) ? NoContent() : NotFound();
    }

    // Método privado para adicionar links HATEOAS
    private ProdutorHateoasDto AddHateoasLinks(ProdutorResponseDto produtor)
    {
        var links = new List<LinkDto>
        {
            new(Url.Action(nameof(Get), new { id = produtor.Id })!, "self", "GET"),
            new(Url.Action(nameof(Put))!, "update", "PUT"),
            new(Url.Action(nameof(Delete), new { id = produtor.Id })!, "delete", "DELETE"),
            new(Url.Action(nameof(Get))!, "all", "GET"),
            new(Url.Action(nameof(Search))!, "search", "GET")
        };

        return new ProdutorHateoasDto(
            produtor.Id,
            produtor.Nome,
            produtor.DataCadastro,
            produtor.TipoUsuarioId,
            links
        );
    }
}
