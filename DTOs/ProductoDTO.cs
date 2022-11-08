using System.ComponentModel.DataAnnotations;
using Common.Validaciones;

namespace DTOs
{
    public class ProductoDTO
    {
        public int IdProductos { get; set; }
        public string NombreProducto { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public int? Stock { get; set; }
        public string? ImagenUrl { get; set; }
        public int? IdCategoria { get; set; }
        public string? NombreCategoria {get; set;}
    }

    public class CreateProductoDTO
    {
        [Required]
        public string NombreProducto { get; set; } = null!;
        [Required]
        public string Descripcion { get; set; } = null!; 
        [PesoImagenValidacion(PesoMaximo:4)]
        [TipoArchivoValidacion(grupoTipoArchivos: GrupoTipoArchivos.Imagen)]
        public IFormFile? Foto {get; set;}
        [Required]
        public int IdCategoria {get; set;}
    }
}