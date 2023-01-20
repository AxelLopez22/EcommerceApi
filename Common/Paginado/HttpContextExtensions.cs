using Microsoft.EntityFrameworkCore;

namespace ecommerceApi.Common.Paginado
{
    public static class HttpContextExtensions
    {   
        public async static Task InsertarParametrosPaginacionEnCabecera<T>( IQueryable<T> queryable)
        {
            //(httpContext == null) { throw new ArgumentNullException(nameof(httpContext));}

            double cantidad = await queryable.CountAsync();
            //httpContext.Response.Headers.Add("cantidadTotalRegistros", cantidad.ToString());
        }
    }
}
