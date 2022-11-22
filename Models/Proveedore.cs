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
        public string Nombre { get; set; }
        public string RazonSocial { get; set; }
        public string Email { get; set; }
        public string Ruc { get; set; }
        public bool? Estado { get; set; }

        public virtual ICollection<Compra> Compras { get; set; }
    }
}
