namespace Common.Paginado
{
    public static class PaginadoExtensions
    {
        //public static PaginaCollection<T> Paginar<T>(
        //    this IQueryable<T> query,
        //    int page,
        //    int take
        ////int skip = 0
        //) where T : class
        //{
        //    var result = new PaginaCollection<T>();
        //    result.Paginado.TotaldeRegistro = query.Count();
        //    result.Paginado.Pagina = page;
        //    result.Paginado.RegistroPorPagina = take;

        //    if (result.Paginado.TotaldeRegistro > 0)
        //    {
        //        result.Paginado.TotaldePaginas = Convert.ToInt32(
        //            Math.Ceiling(
        //                Convert.ToDecimal(result.Paginado.TotaldeRegistro) / result.Paginado.RegistroPorPagina
        //            )
        //        );
        //        result.Items = query.Skip((page - 1) * take)
        //                                              .Take(take)
        //                                              .ToList();
        //    }
        //    return result;
        //}

        //public static PaginaCollection<T> Paginar<T>(
        //    this IList<T> query,
        //    int page,
        //    int take
        ////int skip = 0
        //) where T : class
        //{
        //    var result = new PaginaCollection<T>();
        //    result.Paginado.TotaldeRegistro = query.Count();
        //    result.Paginado.Pagina = page;
        //    result.Paginado.RegistroPorPagina = take;

        //    if (result.Paginado.TotaldeRegistro > 0)
        //    {
        //        result.Paginado.TotaldePaginas = Convert.ToInt32(
        //            Math.Ceiling(
        //                Convert.ToDecimal(result.Paginado.TotaldeRegistro) / result.Paginado.RegistroPorPagina
        //            )
        //        );
        //        result.Items = query.Skip((page - 1) * take)
        //                                              .Take(take)
        //                                              .ToList();
        //    }
        //    return result;
        //}
    }
}