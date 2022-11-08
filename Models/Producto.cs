using System;
using System.Collections.Generic;

namespace ecommerceApi.Models
{
    public partial class Producto
    {
        public Producto()
        {
            DetalleCompras = new HashSet<DetalleCompra>();
            DetalleVenta = new HashSet<DetalleVentum>();
        }

        public int IdProductos { get; set; }
        public string NombreProducto { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public int? Stock { get; set; }
        public string? ImagenUrl { get; set; }
        public bool? Estado { get; set; }
        public int? IdCategoria { get; set; }

        public virtual Categorium? IdCategoriaNavigation { get; set; }
        public virtual ICollection<DetalleCompra> DetalleCompras { get; set; }
        public virtual ICollection<DetalleVentum> DetalleVenta { get; set; }
    }
}
