using System.ComponentModel.DataAnnotations;

namespace PruebaAPI.DTO
{
    public class UserDTO
    {
        public int IdUser { get; set; }
        [Required(ErrorMessage ="El nombre es requerido.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "El email es requerido.")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "la contraseña es requerida.")]
        public string Password { get; set; }
        [Required(ErrorMessage = "El id del rol es requerido")]
        public int IdRol { get; set; }
        public byte isActive { get; set; }
    }
}
