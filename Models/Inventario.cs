using System;
using System.Collections.Generic;

namespace ecommerceApi.Models
{
    public partial class Inventario
    {
        public string Categoria { get; set; }
        public int IdProductos { get; set; }
        public string NombreProducto { get; set; }
        public string Descripcion { get; set; }
        public string ImagenUrl { get; set; }
        public int? Stock { get; set; }
        public double? Precio { get; set; }
    }
}
