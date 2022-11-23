using System.ComponentModel.DataAnnotations;

namespace DTOs
{
    public class ComprasDTO
    {
        [Required]
        public int? IdProveedor { get; set; }
        [Required]
        public List<DetalleCompraDTO> Detalle {get; set;}

        public ComprasDTO()
        {
            this.Detalle = new List<DetalleCompraDTO>();
        }
    }

    public class DetalleCompraDTO
    {
        [Required]
        public double? Precio { get; set; }
        [Required]
        public int? Cantidad { get; set; }
        [Required]
        public int? IdProducto { get; set; }
    }
}