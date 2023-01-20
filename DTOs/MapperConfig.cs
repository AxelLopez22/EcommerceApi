using AutoMapper;
using ecommerceApi.DTOs;
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
            CreateMap<Ventum, VentasDTO>().ReverseMap();
            CreateMap<MetodoDePago, MetodosPagosDTO>().ReverseMap();
            CreateMap<Producto, ProductoDTO>()
                .ForMember(x => x.NombreCategoria, options => options.Ignore());
           // CreateMap<Producto, ProductoDTO>()
        }
    }
}