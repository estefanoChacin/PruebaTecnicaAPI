using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PruebaAPI.MODEL
{
    public class User
    {
        [Key]
        public int IdUser { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(100)]
        [EmailAddress]
        public string Email { get; set; }
        [MaxLength(500)]
        public string Password { get; set; }
        public DateTime CreatedDate { get; set; }
        [DefaultValue(true)]
        public byte IsActive { get; set; }
        public int IdRol { get; set; }

        [ForeignKey("IdRol")]
        public Rol Rol { get; set; }
    }
}
