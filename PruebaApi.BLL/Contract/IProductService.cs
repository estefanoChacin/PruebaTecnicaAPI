using PruebaAPI.DTO;

namespace PruebaAPI.BLL.Contract
{
    public interface IProductService
    {
        Task<ProductDTO> Get(int id);
        Task<List<ProductDTO>> GeAll();
        Task<ProductDTO> Create(ProductDTO product);
        Task<bool> Update(ProductDTO product);
        Task<bool> Delete(int id);
    }
}
