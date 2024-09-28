using AutoMapper;
using Microsoft.EntityFrameworkCore.Infrastructure;
using PruebaAPI.BLL.Contract;
using PruebaAPI.DAL.Contract;
using PruebaAPI.DTO;
using PruebaAPI.MODEL;


namespace PruebaAPI.BLL.Service
{
    public class ProductService : IProductService
    {
        private readonly IGenericRepository<Product> _ProductRepository;
        private readonly IGenericRepository<Category> _categoryRepository;

        private readonly IMapper _mapper;

        public ProductService(IGenericRepository<Product> ProductRepository, IMapper mapper, IGenericRepository<Category> categoryRepository)
        {
            _ProductRepository = ProductRepository;
            _mapper = mapper;
            _categoryRepository = categoryRepository;  
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
                Product ProductFilter = await _ProductRepository.Get(e => e.IdProduct == id);

                if (ProductFilter == null || ProductFilter.IdProduct == 0)
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
                //--validar que exista la catergoria en caso de no existir retornar
                Category categoryFiltered = await _categoryRepository.Get(e => e.IdCategoria == Product.IdCategoria);
                if (categoryFiltered == null)
                    throw new TaskCanceledException("El id de la categoria no existe.");

                Product productAdd = _mapper.Map<Product>(Product);
                productAdd.Categoria = categoryFiltered;

                //--crear el producto y validar que realmente se haya creado en caso de no crearse se retorna.
                Product ProductCreated = await _ProductRepository.Create(productAdd);
                if (ProductCreated == null || ProductCreated.IdProduct == 0)
                    throw new TaskCanceledException("Errror. No se pudo crear la producto");

                return _mapper.Map<ProductDTO>(ProductCreated);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<bool> Update(ProductDTO Product)
        {
            try
            {
                Product ProductFiltered = await _ProductRepository.Get(e => e.IdProduct == Product.idProduct);

                if (ProductFiltered == null || ProductFiltered.IdProduct == 0)
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
                Product ProductFiltered = await _ProductRepository.Get(e => e.IdProduct == id);

                if (ProductFiltered == null || ProductFiltered.IdProduct == 0)
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
