using AutoMapper;
using PruebaAPI.BLL.Contract;
using PruebaAPI.DAL.Contract;
using PruebaAPI.DTO;
using PruebaAPI.MODEL;


namespace PruebaAPI.BLL.Service
{
    public class ProductService : IProductService
    {
        private readonly IGenericRepository<Product> _ProductRepository;
        private readonly IMapper _mapper;

        public ProductService(IGenericRepository<Product> ProductRepository, IMapper mapper)
        {
            _ProductRepository = ProductRepository;
            _mapper = mapper;
        }

        public async Task<List<ProductDTO>> GeAll()
        {
            try
            {
                var queryProduct = await _ProductRepository.Getall();
                return _mapper.Map<List<ProductDTO>>(queryProduct).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ProductDTO> Get(int id)
        {
            try
            {
                Product ProductFilter = await _ProductRepository.Get(e => e.idProduct == id);

                if (ProductFilter == null || ProductFilter.idProduct == 0)
                    throw new TaskCanceledException("La producto no existe");

                return _mapper.Map<ProductDTO>(ProductFilter);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ProductDTO> Create(ProductDTO Product)
        {
            try
            {
                Product ProductCreated = await _ProductRepository.Create(_mapper.Map<Product>(Product));

                if (ProductCreated == null || ProductCreated.idProduct == 0)
                    throw new TaskCanceledException("Errror. No se pudo crear la producto");

                return _mapper.Map<ProductDTO>(ProductCreated);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> Update(ProductDTO Product)
        {
            try
            {
                Product ProductFiltered = await _ProductRepository.Get(e => e.idProduct == Product.idProduct);

                if (ProductFiltered == null || ProductFiltered.idProduct == 0)
                    throw new TaskCanceledException("La producto no existe");

                ProductFiltered.Name = Product.Name;
                ProductFiltered.Description = Product.Description;

                bool response = await _ProductRepository.Update(ProductFiltered);

                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                Product ProductFiltered = await _ProductRepository.Get(e => e.idProduct == id);

                if (ProductFiltered == null || ProductFiltered.idProduct == 0)
                    throw new TaskCanceledException("La producto no existe.");

                bool response = await _ProductRepository.Delete(ProductFiltered);

                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
