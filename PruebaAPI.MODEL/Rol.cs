namespace PruebaAPI.MODEL
{
    public class Rol
    {
        public int IdRol { get; set; }
        public int name { get; set; }
        public DateTime datedCreate { get; set; }
        public virtual ICollection<User> users { get; set; } = new List<User>();
    }
}
