using AutoMapper;
using PruebaAPI.BLL.Contract;
using PruebaAPI.DAL.Contract;
using PruebaAPI.DTO;
using PruebaAPI.MODEL;
using PruebaAPI.UTILITY.Security;

namespace PruebaAPI.BLL.Service
{
    public class UserService:IUserService
    {
        private readonly IGenericRepository<User> _userRepository;
        private readonly IGenericRepository<Rol> _rolRepository;
        private readonly JWTService _jwtService;
        private readonly IMapper _mapper;



        public UserService(IGenericRepository<User> usuarioRepository, IMapper mapper , IGenericRepository<Rol> rolRepository, JWTService jwtService)
        {
            _userRepository = usuarioRepository;
            _mapper = mapper;
            _rolRepository = rolRepository;
            _jwtService = jwtService;
        }


        //obtener la lista de todos los usuarios
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


        //obtener el usuario por un id en especifico
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


        //Crear el usuario
        public async Task<UserDTO> Create(UserDTO usuarioDTO)
        {
            try
            {
                User usuario = _mapper.Map<User>(usuarioDTO);

                //----validar rol y en caso de no existir retornar
                var rol = await _rolRepository.Get(e => e.IdRol == usuario.IdRol);
                if(rol == null)
                    throw new TaskCanceledException("El id del rol no existe");

                //-----validar que no exista otro usuario con el mismo correo
                var usuarioEmailRepeat = await _userRepository.Get(e => e.Email == usuario.Email);
                if (usuarioEmailRepeat != null)
                    throw new TaskCanceledException("Error. email ya esta en uso por otro usuario");

                //-----encriptar la clave
                string newPassword = EncriptPassword.EncryptSHA256(usuario.Password);
                usuario.Password = newPassword;
                //-----establecer fecha de creacion
                usuario.CreatedDate = DateTime.Now;
               
                //-----crear usuario y validar si de verdad a sido creado y retornar
                User usuarioCreated = await _userRepository.Create(usuario);
                if (usuarioCreated == null || usuarioCreated.IdUser == 0)
                    throw new TaskCanceledException("Error. No se pudo crear usuario");

                return _mapper.Map<UserDTO>(usuarioCreated);
            }
            catch (Exception)
            {
                throw;
            }
        }


        //acatulizar el usuario
        public async Task<bool> Update(UserDTO usuario)
        {
            try
            {
                //--validar que exista el usuario en caso de que no exista retornar
                User userFiltered = await _userRepository.Get(e => e.IdUser == usuario.IdUser);
                if (userFiltered == null || userFiltered.IdUser == 0)
                    throw new TaskCanceledException("El usuario no existe");

                //-----validar que no exista otro usuario con el mismo correo
                var usuarioEmailRepeat = await _userRepository.Get(e => e.Email == usuario.Email && e.IdUser != usuario.IdUser);
                if (usuarioEmailRepeat != null)
                    throw new TaskCanceledException("Error. email ya esta en uso por otro usuario");


                //--setear nuevas propiedades del usuario
                userFiltered.Name = usuario.Name;
                userFiltered.Email = usuario.Email;
                userFiltered.IsActive = usuario.isActive;
                //--validar que hayan actualizado la clave en caso de que si, encriptarla nuevamente.
                if (userFiltered.Password != usuario.Password)
                {
                    string newPassword = EncriptPassword.EncryptSHA256(usuario.Password);
                    userFiltered.Password = newPassword;
                }

                //----validar el rol antes de actualzar el usuario en caso de no existir retornar
                var rol = _rolRepository.Get(e => e.IdRol == usuario.IdRol);
                if (rol.Id == 0 || rol == null)
                    throw new TaskCanceledException("El id del rol no existe");

                bool response = await _userRepository.Update(userFiltered);
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }


        //eliminar el usuario
        public async Task<bool> Delete(int id)
        {
            try
            {
                //validar que exita el usuario
                User userFiltered = await _userRepository.Get(e => e.IdUser == id);
                if (userFiltered == null || userFiltered.IdUser == 0)
                    throw new TaskCanceledException("El usuario no existe");

                //eliminar
                bool response = await _userRepository.Delete(userFiltered);

                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<string> AuthenticateUser(LoginDTO login)
        {
            try
            {
                //--encriptar clave para validar
                string passwordEncripted = EncriptPassword.EncryptSHA256(login.Password);
                //--filtrar el usuario por las credenciale en caso de no existir retornar
                var UserFiltered = await _userRepository.Get(e => e.Email == login.Email && e.Password == passwordEncripted);
                if (UserFiltered == null)
                    throw new TaskCanceledException("Credendiales invalidas.");

                //obtener data del rol del usuario
                var rol = await _rolRepository.Get(e => e.IdRol == UserFiltered.IdRol);
                UserFiltered.Rol = rol;
                //--metodo que genera el bearer token con la informacion del usuario filtrado.
                string token = _jwtService.GenerateJwtToken(UserFiltered);

                return token;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
