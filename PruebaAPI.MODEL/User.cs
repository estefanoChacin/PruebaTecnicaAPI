using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

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
        public DateTime createdDate { get; set; }
        [DefaultValue(true)]
        public bool isActive { get; set; }
    }
}
