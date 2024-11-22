using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PF.Models
{
    public class Categoria
    {
        [Key]
        public int IdCategoria { get; set; }
        [Required]
        [MaxLength(40)]
        public string CategoriaNome { get; set; }
        [MaxLength(256)]
        public string? CategoriaDescricao { get; set; }

        public List<Produto> Produtos { get; set; }
    }
}
