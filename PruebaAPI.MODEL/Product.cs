
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace PruebaAPI.MODEL
{
    public class Product
    {
        [Key]
        public int IdProduct { get; set; }
        [MaxLength(50)]
        public string? Name { get; set; }
        [MaxLength(200)]
        public string? Description { get; set; }
        public int? Stock { get; set; }
        public decimal Price { get; set; }
        public DateTime  DatedCreate { get; set; }
        [ForeignKey("IdCategoria")]
        public int IdCategoria  { get; set; }
        public virtual Category? Categoria { get; set; }
    }
}
