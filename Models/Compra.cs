using System;
using System.Collections.Generic;

namespace ecommerceApi.Models
{
    public partial class Compra
    {
        public Compra()
        {
            DetalleCompras = new HashSet<DetalleCompra>();
        }

        public int IdCompra { get; set; }
        public DateTime FechaCompra { get; set; }
        public double? Total { get; set; }
        public bool? Estado { get; set; }
        public int? IdUsuario { get; set; }
        public int? IdProveedor { get; set; }

        public virtual Proveedore? IdProveedorNavigation { get; set; }
        public virtual ICollection<DetalleCompra> DetalleCompras { get; set; }
    }
}
