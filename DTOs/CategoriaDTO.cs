namespace DTOs
{
    public class CategoriaDTO
    {
        public int IdCategoria { get; set; }
        public string Nombre { get; set; } = null!;
    }

    public class CreateCategoriaDTO
    {
        public string Nombre { get; set; } = null!;
        public bool? TieneCompra { get; set; }
    }
}