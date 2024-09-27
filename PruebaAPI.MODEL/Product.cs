
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PruebaAPI.MODEL
{
    public class Product
    {
        [Key]
        public int idProduct { get; set; }
        [MaxLength(50)]
        public string? Name { get; set; }
        [MaxLength(200)]
        public string? Description { get; set; }
        public int? Stock { get; set; }
        public decimal? Price { get; set; }
        public DateTime  DatedCreate { get; set; }
        public virtual Category? Categoria { get; set; }
    }
}
