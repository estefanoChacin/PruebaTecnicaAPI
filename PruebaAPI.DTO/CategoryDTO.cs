using System.ComponentModel.DataAnnotations;

namespace PruebaAPI.DTO
{
    public class CategoryDTO
    {
        public int IdCategoria { get; set; }
        [Required(ErrorMessage = "El nombre es requerido.")]
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}
