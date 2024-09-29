
using PruebaAPI.DTO;

namespace PruebaAPI.BLL.Contract
{
    public interface IRolService
    {
        Task<RolDTO> Get(int id);
        Task<List<RolDTO>> GeAll();
        Task<RolDTO> Create(RolDTO rol);
        Task<RolDTO> Update(RolDTO rol);
        Task<bool> Delete(int id);

    }
}
