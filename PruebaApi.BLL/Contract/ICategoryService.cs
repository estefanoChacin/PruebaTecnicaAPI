using PruebaAPI.DTO;


namespace PruebaAPI.BLL.Contract
{
    public interface ICategoryService
    {
        Task<CategoryDTO> Get(int id);
        Task<List<CategoryDTO>> GeAll();
        Task<CategoryDTO> Create(CategoryDTO category);
        Task<bool> Update(CategoryDTO category);
        Task<bool> Delete(int id);
    }
}
