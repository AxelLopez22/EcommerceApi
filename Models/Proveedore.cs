using System;
using System.Collections.Generic;

namespace ecommerceApi.Models
{
    public partial class Proveedore
    {
        public Proveedore()
        {
            Compras = new HashSet<Compra>();
        }

        public int IdProveedor { get; set; }
        public string Nombre { get; set; } = null!;
        public string RazonSocial { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Ruc { get; set; }
        public bool? Estado { get; set; }

        public virtual ICollection<Compra> Compras { get; set; }
    }
}
