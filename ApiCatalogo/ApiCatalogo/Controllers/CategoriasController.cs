using ApiCatalogo.Context;
using ApiCatalogo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogo.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriasController : Controller
{
    private readonly AppDbContext _context;

    public CategoriasController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Categoria>>> Get()
    {
        try
        {
            var categorias = await _context.Categorias.AsNoTracking().ToListAsync();

            if (categorias is null)
                return NotFound("Categorias não encontradas.");

            return categorias;
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Ocorreu um problema ao tratar a sua solicitação.");
        }
        
    }

    [HttpGet("{id:int}", Name = "ObterCategoria")]
    public async Task<ActionResult<Categoria>> Get(int id)
    {
        try
        {
            var categoria = await _context.Categorias.AsNoTracking().FirstOrDefaultAsync(x => x.CategoriaId == id);

            if (categoria is null)
                return NotFound("Categoria não encontrada");

            return Ok(categoria);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Ocorreu um problema ao tratar a sua solicitação.");
        }
        
    }

    [HttpGet("produtos")]
    public async Task<ActionResult<IEnumerable<Categoria>>> GetCategoriaProdutos()
    {
        try
        {
            return await _context.Categorias.AsNoTracking().Include(x => x.Produtos).ToListAsync();
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Ocorreu um problema ao tratar a sua solicitação.");
        }
    }

    [HttpPost]
    public async Task<ActionResult<Categoria>> Post(Categoria categoria)
    {
        try
        {
            if (categoria is null)
                return BadRequest("Dados inválidos!");

            await _context.Categorias.AddAsync(categoria);
            await _context.SaveChangesAsync();

            return new CreatedAtRouteResult("ObterCategoria", new { id = categoria.CategoriaId }, categoria);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Ocorreu um problema ao tratar a sua solicitação.");
        }
        
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<Categoria>> Put(int id, Categoria categoria)
    {
        try
        {
            if (id != categoria.CategoriaId)
                return BadRequest("Dados inválidos!");

            _context.Entry(categoria).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(categoria);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Ocorreu um problema ao tratar a sua solicitação.");
        }
        
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<Categoria>> Delete(int id)
    {
        try
        {
            var categoria = await _context.Categorias.FirstOrDefaultAsync(x => x.CategoriaId == id);

            if (categoria is null)
                return NotFound("Categoria não encontrada.");

            _context.Categorias.Remove(categoria);
            await _context.SaveChangesAsync();

            return Ok(categoria);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Ocorreu um problema ao tratar a sua solicitação.");
        }
        
    }

}
