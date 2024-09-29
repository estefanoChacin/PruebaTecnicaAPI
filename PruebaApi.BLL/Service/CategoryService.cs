using AutoMapper;
using PruebaAPI.BLL.Contract;
using PruebaAPI.DAL.Contract;
using PruebaAPI.DTO;
using PruebaAPI.MODEL;


namespace PruebaAPI.BLL.Service
{
    public class CategoryService: ICategoryService
    {
        private readonly IGenericRepository<Category> _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(IGenericRepository<Category> categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<List<CategoryDTO>> GeAll()
        {
            try
            {
                var queryCategoria = await _categoryRepository.Getall();
                return _mapper.Map<List<CategoryDTO>>(queryCategoria).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<CategoryDTO> Get(int id)
        {
            try
            {
                Category categoryFilter = await _categoryRepository.Get(e => e.IdCategoria == id);

                if (categoryFilter == null )
                    throw new TaskCanceledException("La cateogria no existe");

                return _mapper.Map<CategoryDTO>(categoryFilter);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<CategoryDTO> Create(CategoryDTO category)
        {
            try
            {
                category.IdCategoria = 0;

                Category categoryCreated = await _categoryRepository.Create(_mapper.Map<Category>(category));

                if (categoryCreated == null)
                    throw new TaskCanceledException("Errror. No se pudo crear la categoria");

                return _mapper.Map<CategoryDTO>(categoryCreated);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<CategoryDTO> Update(CategoryDTO category)
        {
            try
            {
                Category categoryFiltered = await _categoryRepository.Get(e => e.IdCategoria == category.IdCategoria);

                if (categoryFiltered == null )
                    throw new TaskCanceledException("La categoria no existe");

                categoryFiltered.Name = category.Name;
                categoryFiltered.Description = category.Description;

                Category categoryUpdated = await _categoryRepository.Update(categoryFiltered);

                return _mapper.Map<CategoryDTO>(categoryUpdated);
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
                Category categoryFiltered = await _categoryRepository.Get(e => e.IdCategoria == id);

                if (categoryFiltered == null )
                    throw new TaskCanceledException("La categoria no existe.");

                bool response = await _categoryRepository.Delete(categoryFiltered);

                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
