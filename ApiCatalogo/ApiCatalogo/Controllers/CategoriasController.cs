using ApiCatalogo.Context;
using ApiCatalogo.Filters;
using ApiCatalogo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogo.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriasController : Controller
{
    private readonly AppDbContext _context;
    private readonly ILogger _logger;

    public CategoriasController(AppDbContext context, ILogger logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet]
    [ServiceFilter(typeof(ApiLoggingFilter))]
    public async Task<ActionResult<IEnumerable<Categoria>>> Get()
    {

        _logger.LogInformation(" ===================== GET api/categorias ========================");

        var categorias = await _context.Categorias.AsNoTracking().ToListAsync();

        if (categorias is null)
            return NotFound("Categorias não encontradas.");

        return categorias;
        
    }

    [HttpGet("{id:int}", Name = "ObterCategoria")]
    public async Task<ActionResult<Categoria>> Get(int id)
    {
        _logger.LogInformation($" ===================== GET api/categorias/id = {id} ========================");

        var categoria = await _context.Categorias.AsNoTracking().FirstOrDefaultAsync(x => x.CategoriaId == id);

        if (categoria is null)
            return NotFound("Categoria não encontrada");

        return Ok(categoria);
        
    }

    [HttpGet("produtos")]
    public async Task<ActionResult<IEnumerable<Categoria>>> GetCategoriaProdutos()
    {
        _logger.LogInformation(" ===================== GET api/categorias/produtos ========================");

        return await _context.Categorias.AsNoTracking().Include(x => x.Produtos).ToListAsync();
        
    }

    [HttpPost]
    public async Task<ActionResult<Categoria>> Post(Categoria categoria)
    {

        if (categoria is null)
            return BadRequest("Dados inválidos!");

        await _context.Categorias.AddAsync(categoria);
        await _context.SaveChangesAsync();

        return new CreatedAtRouteResult("ObterCategoria", new { id = categoria.CategoriaId }, categoria);
        
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<Categoria>> Put(int id, Categoria categoria)
    {
        if (id != categoria.CategoriaId)
            return BadRequest("Dados inválidos!");

        _context.Entry(categoria).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return Ok(categoria);
       
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<Categoria>> Delete(int id)
    {
        var categoria = await _context.Categorias.FirstOrDefaultAsync(x => x.CategoriaId == id);

        if (categoria is null)
            return NotFound("Categoria não encontrada.");

        _context.Categorias.Remove(categoria);
        await _context.SaveChangesAsync();

        return Ok(categoria);
        
    }

}
