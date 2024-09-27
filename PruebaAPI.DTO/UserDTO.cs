namespace PruebaAPI.DTO
{
    public class UserDTO
    {
        public int IdUser { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string createdDate { get; set; }
        public bool isActive { get; set; }
    }
}
