using ApiCatalogo.Context;
using ApiCatalogo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogo.Controllers;

[Route("[controller]")]
[ApiController]
public class ProdutosController : ControllerBase
{
    private readonly AppDbContext _context;

    public ProdutosController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Produto>> Get()
    {
        var produtos = _context.Produtos.AsNoTracking().ToList();

        if (produtos is null)
            return NotFound("Produtos não encontrados.");

        return produtos;
    }

    [HttpGet("{id:int}")]
    public ActionResult<Produto> Get(int id)
    {
        var produto = _context.Produtos.FirstOrDefault(x => x.ProdutoId == id);
        if (produto is null)
            return NotFound();

        return produto;
    }

}
