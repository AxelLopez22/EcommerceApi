namespace Common.Paginado
{
    public class PaginaCollection<T>
    {
        public bool TieneItems
        {
            get
            {
                return Items != null && Items.Any();
            }
        }
        public IEnumerable<T> Items { get; set; }
        public PaginaModel Paginado { get; set; } = new PaginaModel();
    }

    public class PaginaCollection
    {
        public bool TieneItems
        {
            get
            {
                return Items != null && Items.Length > 0;
            }
        }
        public Array Items { get; set; }
        public PaginaModel Paginado { get; set; } = new PaginaModel();
    }

    public class PaginaModel
    {
        public int Pagina { get; set; }
        public int TotaldePaginas { get; set; }
        public int RegistroPorPagina { get; set; }
        public int TotaldeRegistro { get; set; }
    }
}