
using System.ComponentModel.DataAnnotations;

namespace PruebaAPI.MODEL
{
    public class Category
    {
        [Key]
        public int IdCategoria { get; set; }
        [MaxLength(50)]
        public string? Name { get; set; }
        [MaxLength(200)]
        public string? Description { get; set; }
    }
}
