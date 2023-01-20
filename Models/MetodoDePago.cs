using System;
using System.Collections.Generic;

namespace ecommerceApi.Models
{
    public partial class MetodoDePago
    {
        public MetodoDePago()
        {
            Compras = new HashSet<Compra>();
            Venta = new HashSet<Ventum>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool? Estado { get; set; }

        public virtual ICollection<Compra> Compras { get; set; }
        public virtual ICollection<Ventum> Venta { get; set; }
    }
}
