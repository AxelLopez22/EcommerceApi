namespace DTOs
{
    public class VentasDTO
    {
        public List<DetalleVentasDTO> Detalle {get; set;}

        public VentasDTO()
        {
            this.Detalle = new List<DetalleVentasDTO>();
        }
    }

    public class DetalleVentasDTO
    {
        public int IdProducto {get; set;}
        public int Cantidad {get; set;}
        public double Precio {get; set;}
        public bool Compra {get; set;}
    }
}