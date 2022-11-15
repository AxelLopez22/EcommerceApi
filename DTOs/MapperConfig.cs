using AutoMapper;
using ecommerceApi.Models;

namespace DTOs
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<Categorium, CategoriaDTO>().ReverseMap();
            CreateMap<Categorium, CreateCategoriaDTO>().ReverseMap();
            CreateMap<CreateProductoDTO, Producto>()
                .ForMember(x => x.ImagenUrl, options => options.Ignore());
            CreateMap<Proveedore,ProveedoresDTO>().ReverseMap();
            CreateMap<Proveedore,AgregarProveedoresDTO>().ReverseMap();
            CreateMap<Compra, ComprasDTO>().ReverseMap();
        }
    }
}