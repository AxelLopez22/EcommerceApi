using System;
using System.Collections.Generic;

namespace ecommerceApi.Models
{
    public partial class DetalleVentum
    {
        public int IdDetalle { get; set; }
        public double? Precio { get; set; }
        public int? Cantidad { get; set; }
        public int? IdVenta { get; set; }
        public int? IdProducto { get; set; }

        public virtual Producto IdProductoNavigation { get; set; }
        public virtual Ventum IdVentaNavigation { get; set; }
    }
}
