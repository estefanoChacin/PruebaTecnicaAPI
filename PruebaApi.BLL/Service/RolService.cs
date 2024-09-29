using PruebaAPI.BLL.Contract;
using PruebaAPI.DAL.Contract;
using PruebaAPI.DTO;
using PruebaAPI.MODEL;
using AutoMapper;

namespace PruebaAPI.BLL.Service
{
    public class RolService : IRolService
    {
        private readonly IGenericRepository<Rol> _rolRepository;
        private readonly IMapper _mapper;

        public RolService(IGenericRepository<Rol> rolRepository, IMapper mapper)
        {
            _rolRepository = rolRepository;
            _mapper = mapper;
        }

        public async Task<List<RolDTO>> GeAll()
        {
            try
            {
                var queyRol = await _rolRepository.Getall();
                return _mapper.Map<List<RolDTO>>(queyRol).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<RolDTO> Get(int id)
        {
            try
            {
                Rol rolFiltered = await _rolRepository.Get(e=> e.IdRol == id);

                if (rolFiltered == null || rolFiltered.IdRol == 0)
                    throw new TaskCanceledException("El rol no existe");

                return _mapper.Map<RolDTO>(rolFiltered);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<RolDTO> Create(RolDTO rol)
        {
            try
            {
                rol.IdRol = 0;
                Rol rolCreated = await _rolRepository.Create(_mapper.Map<Rol>(rol));

                if (rolCreated == null || rolCreated.IdRol == 0)
                    throw new TaskCanceledException("Errror. No se pudo crear rol");

                return _mapper.Map<RolDTO>(rolCreated);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<RolDTO> Update(RolDTO rol)
        {
            try
            {
                Rol rolFiltered = await _rolRepository.Get(e => e.IdRol == rol.IdRol);

                if (rolFiltered == null || rolFiltered.IdRol == 0)
                    throw new TaskCanceledException("El rol no existe");

                rolFiltered.name = rol.name;

                Rol rolUpdated = await _rolRepository.Update(rolFiltered);
                
                return _mapper.Map<RolDTO>(rolUpdated);
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
                Rol rolFiltered = await _rolRepository.Get(e => e.IdRol == id);

                if (rolFiltered == null || rolFiltered.IdRol == 0)
                    throw new TaskCanceledException("El rol no existe");

                bool response = await _rolRepository.Delete(rolFiltered);

                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
