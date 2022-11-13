using System;
using System.Collections.Generic;

namespace ecommerceApi.Models
{
    public partial class Inventario
    {
        public string Categoria { get; set; } = null!;
        public int IdProductos { get; set; }
        public string NombreProducto { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public string? ImagenUrl { get; set; }
        public int? Stock { get; set; }
        public double? Precio { get; set; }
    }
}
