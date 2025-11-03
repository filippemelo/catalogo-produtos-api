using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ApiCatalogo.Models;

[Table("Produtos")]
public class Produto
{
    [Key]
    public int ProdutoId { get; set; }

    [Required(ErrorMessage = "O nome é obrigatório.")]
    [StringLength(80, ErrorMessage = "O nome deve conter entre 5 e 80 caracteres.", MinimumLength = 5)]
    public string Nome { get; set; } = null!;

    [Required]
    [StringLength(200, ErrorMessage = "A descrição deve conter no máximo {1} caracteres.")]
    public string? Descricao { get; set; }

    [Required]
    [Column(TypeName = "decimal(10,2)")]
    public decimal Preco { get; set; }

    [Required]
    [StringLength(300, MinimumLength = 10)]
    public string? ImagemUrl { get; set; }

    public float Estoque { get; set; }
    public DateTime DataCadastro { get; set; }

    public int CategoriaId { get; set; }

    [JsonIgnore]
    public Categoria? Categoria { get; set; }
}
