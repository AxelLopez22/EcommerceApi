using System;
using System.Collections.Generic;

namespace ecommerceApi.Models
{
    public partial class Carrito
    {
        public int IdCarrito { get; set; }
        public int IdProducto { get; set; }
        public string IdUsuario { get; set; }
    }
}
