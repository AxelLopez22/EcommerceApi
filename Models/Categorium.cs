using System;
using System.Collections.Generic;

namespace ecommerceApi.Models
{
    public partial class Categorium
    {
        public Categorium()
        {
            Productos = new HashSet<Producto>();
        }

        public int IdCategoria { get; set; }
        public string Nombre { get; set; } = null!;
        public bool? TieneCompra { get; set; }
        public bool? Estado { get; set; }

        public virtual ICollection<Producto> Productos { get; set; }
    }
}
