using ApiCatalogo.Context;
using ApiCatalogo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogo.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProdutosController : ControllerBase
{
    private readonly AppDbContext _context;

    public ProdutosController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Produto>?>> Get()
    {
        var produtos = await _context.Produtos.AsNoTracking().ToListAsync();

        if (produtos is null)
            return NotFound("Produtos não encontrados.");

        return produtos;
    }

    [HttpGet("{id:int:min(1)}", Name = "ObterProduto")]
    public async Task<ActionResult<Produto?>> Get(int id)
    {
        var produto = await _context.Produtos.AsNoTracking().FirstOrDefaultAsync(x => x.ProdutoId == id);
        if (produto is null)
            return NotFound("Produto não encontrado.");

        return Ok(produto);
    }

    [HttpPost]
    public async Task<ActionResult> Post(Produto produto)
    {
        if (produto is null)
            return BadRequest();

        await _context.Produtos.AddAsync(produto);
        await _context.SaveChangesAsync();

        return new CreatedAtRouteResult("ObterProduto", new { id = produto.ProdutoId }, produto);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Put(int id, Produto produto)
    {
        if (id != produto.ProdutoId)
            return BadRequest();

        _context.Entry(produto).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return Ok(produto);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        var produto = await _context.Produtos.FirstOrDefaultAsync(x => x.ProdutoId == id);

        if (produto is null)
            return NotFound("Produto não encontrado.");

        _context.Produtos.Remove(produto);
        _context.SaveChanges();

        return Ok(produto);

    }
}
