using System.Collections.ObjectModel;

namespace ApiCatalogo.Models;

public class Categoria
{
    public int CategoriaId { get; set; }
    public string Nome { get; set; } = null!;
    public string? ImagemUrl { get; set; }

    public ICollection<Produto> Produtos { get; set; } = new Collection<Produto>();
}
