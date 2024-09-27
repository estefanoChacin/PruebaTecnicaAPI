using AutoMapper;
using PruebaAPI.DTO;
using PruebaAPI.MODEL;
using System.Globalization;

namespace PruebaAPI.UTILITY.Mapper
{
    public class MapperProfile: Profile
    {
        public MapperProfile()
        {
            CreateMap<Rol, RolDTO>().ReverseMap();

            CreateMap<Category, CategoryDTO>().ReverseMap();

            CreateMap<Product, ProductDTO>()
                .ForMember(destino => destino.Price,
                opt => opt.MapFrom(origen => Convert.ToString(origen.Price, new CultureInfo("es-CO"))));

            CreateMap<ProductDTO, Product>()
                .ForMember(destino => destino.Price,
                opt => opt.MapFrom(origen => Convert.ToDecimal(origen.Price)));

            CreateMap<User, UserDTO>().ReverseMap();
        }
    }
}
