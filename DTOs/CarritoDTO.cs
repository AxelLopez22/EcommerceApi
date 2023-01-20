using DTOs;

namespace ecommerceApi.DTOs
{
    public class CarritoDTO
    {
        public List<ProductoDTO> Productos { get; set; }

        public CarritoDTO()
        {
            this.Productos = new List<ProductoDTO>();
        }
    }

    public class CarritoDetalleDTO
    {
        public int idProducto { get; set; }
        public int cantidad { get; set; }
        public double precio { get; set; }
        public bool compra { get; set; } = true;
    }
}
