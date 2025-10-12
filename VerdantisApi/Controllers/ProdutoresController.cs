using VerdantisBusiness;
using VerdantisModel;
using Microsoft.AspNetCore.Mvc;

namespace VerdantisApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProdutoresController(
    IProdutorService produtorService
) : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        var produtores = produtorService.ListarTodos();
        return produtores.Count == 0 ? NoContent() : Ok(produtores);
    }

    [HttpGet("{id}")]
    public IActionResult Get(string id)
    {
        var produtor = produtorService.ObterPorId(id);
        return produtor == null ? NotFound() : Ok(produtor);
    }

    [HttpPost]
    public IActionResult Post([FromBody] ProdutorModel produtor)
    {
        if (string.IsNullOrWhiteSpace(produtor.Nome))
            return BadRequest("Nome é obrigatório.");
        var criado = produtorService.Criar(produtor);
        return CreatedAtAction(nameof(Get), new { id = criado.Id }, criado);
    }

    [HttpPut]
    public IActionResult Put([FromBody] ProdutorModel produtor)
    {
        if (produtor == null)
            return BadRequest("Dados inconsistentes.");
        return produtorService.Atualizar(produtor) ? NoContent() : NotFound();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(string id)
    {
        return produtorService.Remover(id) ? NoContent() : NotFound();
    }
}
