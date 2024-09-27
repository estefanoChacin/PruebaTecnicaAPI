using AutoMapper;
using PruebaAPI.BLL.Contract;
using PruebaAPI.DAL.Contract;
using PruebaAPI.DTO;
using PruebaAPI.MODEL;
using System.Text;
using System.Security.Cryptography;

namespace PruebaAPI.BLL.Service
{
    public class UserService:IUserService
    {
        private readonly IGenericRepository<User> _userRepository;
        private readonly IMapper _mapper;

        public UserService(IGenericRepository<User> usuarioRepository, IMapper mapper)
        {
            _userRepository = usuarioRepository;
            _mapper = mapper;
        }

        public async Task<List<UserDTO>> GeAll()
        {
            try
            {
                var queyUser = await _userRepository.Getall();
                return _mapper.Map<List<UserDTO>>(queyUser).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<UserDTO> Get(int id)
        {
            try
            {
                User userFiltered = await _userRepository.Get(e => e.IdUser == id);

                if (userFiltered == null || userFiltered.IdUser == 0)
                    throw new TaskCanceledException("El usuario no existe");

                return _mapper.Map<UserDTO>(userFiltered);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<UserDTO> Create(UserDTO usuario)
        {
            try
            {
                User usuarioCreated = await _userRepository.Create(_mapper.Map<User>(usuario));

                if (usuarioCreated == null || usuarioCreated.IdUser == 0)
                    throw new TaskCanceledException("Error. No se pudo crear usuario");

                return _mapper.Map<UserDTO>(usuarioCreated);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> Update(UserDTO usuario)
        {
            try
            {
                User userFiltered = await _userRepository.Get(e => e.IdUser == usuario.IdUser);

                if (userFiltered == null || userFiltered.IdUser == 0)
                    throw new TaskCanceledException("El usuario no existe");

                userFiltered.Name = usuario.Name;
                userFiltered.Email = usuario.Email;
                userFiltered.isActive = usuario.isActive;

                if (userFiltered.Password != usuario.Password)
                {
                    string newPassword = EncryptPasswordSHA256(usuario.Password);
                    userFiltered.Password = newPassword;
                }

                bool response = await _userRepository.Update(userFiltered);
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
                User userFiltered = await _userRepository.Get(e => e.IdUser == id);

                if (userFiltered == null || userFiltered.IdUser == 0)
                    throw new TaskCanceledException("El usuario no existe");

                bool response = await _userRepository.Delete(userFiltered);

                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }


        private string EncryptPasswordSHA256(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                byte[] hashBytes = sha256.ComputeHash(passwordBytes);
                StringBuilder hashStringBuilder = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    hashStringBuilder.Append(b.ToString("x2"));
                }

                return hashStringBuilder.ToString();
            }
        }
    }
}
