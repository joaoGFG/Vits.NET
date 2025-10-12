using Microsoft.AspNetCore.Mvc;
using VerdantisBusiness;
using VerdantisBusiness.DTOs;

namespace VerdantisApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProdutoresController(IProdutorService produtorService) : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        var produtores = produtorService.ListarTodos();
        return produtores.Count == 0 ? NoContent() : Ok(produtores);
    }

    [HttpGet("{id:int}")]
    public IActionResult Get(int id)
    {
        var produtor = produtorService.ObterPorId(id);
        return produtor == null ? NotFound() : Ok(produtor);
    }

    [HttpPost]
    public IActionResult Post([FromBody] ProdutorCreateDto produtor)
    {
        if (string.IsNullOrWhiteSpace(produtor.Nome))
            return BadRequest("Nome é obrigatório.");
        if (produtor.TipoUsuarioId <= 0)
            return BadRequest("TipoUsuarioId é obrigatório.");

        var criado = produtorService.Criar(produtor);
        return CreatedAtAction(nameof(Get), new { id = criado.Id }, criado);
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
}
