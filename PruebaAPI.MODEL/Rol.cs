using System.ComponentModel.DataAnnotations;

namespace PruebaAPI.MODEL
{
    public class Rol
    {
        [Key]
        public int IdRol { get; set; }
        [MaxLength(50)]
        public int name { get; set; }
        [MaxLength(200)]
        public DateTime datedCreate { get; set; }
        public virtual ICollection<User> users { get; set; } = new List<User>();
    }
}
