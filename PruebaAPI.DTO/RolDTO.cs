using System.ComponentModel.DataAnnotations;

namespace PruebaAPI.DTO
{
    public class RolDTO
    {
        public int IdRol { get; set; }
        [Required(ErrorMessage = "El nombre es requerido.")]
        public string name { get; set; }
    }
}
