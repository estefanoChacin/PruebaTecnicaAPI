
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PruebaAPI.DTO
{
    public class ProductDTO
    {
        public int idProduct { get; set; }

        [Required(ErrorMessage = "El nombre es requerido.")]
        public string? Name { get; set; }
        public string? Description { get; set; }

        [Required(ErrorMessage = "El stock del producto es requerido.")]
        [Range(0, int.MaxValue, ErrorMessage ="el stock no puede se negativo")]
        [DefaultValue(0)]
        public int? Stock { get; set; }

        [Required(ErrorMessage = "El precio es requerido.")]
        [Range(0, int.MaxValue, ErrorMessage = "el precio no puede se negativo")]
        [DefaultValue(0)]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "La categoria es requerido.")]
        public int IdCategoria { get; set; }
    }
}
