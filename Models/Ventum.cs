using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace ecommerceApi.Models
{
    public partial class Ventum
    {
        public Ventum()
        {
            DetalleVenta = new HashSet<DetalleVentum>();
        }

        public int IdVenta { get; set; }
        public DateTime? FechaVenta { get; set; }
        public double? Total { get; set; }
        public bool? Estado { get; set; }
        public string IdUsuario { get; set; }
        public IdentityUser Usuario {get; set;}
        public virtual ICollection<DetalleVentum> DetalleVenta { get; set; }
    }
}
