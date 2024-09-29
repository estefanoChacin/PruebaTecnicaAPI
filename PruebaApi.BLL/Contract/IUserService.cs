using PruebaAPI.DTO;

namespace PruebaAPI.BLL.Contract
{
    public interface IUserService
    {
        Task<UserDTO> Get(int id);
        Task<List<UserDTO>> GeAll();
        Task<UserDTO> Create(UserDTO user);
        Task<UserDTO> Update(UserDTO user);
        Task<bool> Delete(int id);
        Task<string> AuthenticateUser(LoginDTO login);
    }
}
