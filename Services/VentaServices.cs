using AutoMapper;
using DTOs;
using ecommerceApi.Context;
using ecommerceApi.Models;
using Microsoft.EntityFrameworkCore;
using Common.Extensions;

namespace Services
{
    public class VentaServices
    {
        //private readonly RepositoryContext _context;
        private readonly ContextDb _contextDb;
        private readonly IMapper _mapper;

        public VentaServices(IMapper mapper, ContextDb contextDb)
        {
            _mapper = mapper;
            _contextDb = contextDb;
        }

        public async Task<VentasDTO> CrearVenta(string IdUsuario, VentasDTO model)
        {
            var transaction = await _contextDb.Database.BeginTransactionAsync();
            try
            {
                Ventum venta = new Ventum();
                venta.UsuarioId = IdUsuario;
                venta.FechaVenta = DateTime.Now;
                venta.Estado = true;
                venta.IdMetodoPago = model.IdMetodoPago;
                venta.Total = model.Detalle.Sum(x => x.Precio * x.Cantidad);
                _contextDb.Venta.Add(venta);
                await _contextDb.SaveChangesAsync();

                foreach(var item in model.Detalle)
                {
                    DetalleVentum detalle = new DetalleVentum();
                    detalle.Cantidad = item.Cantidad;
                    detalle.Precio = item.Precio;
                    detalle.IdProducto = item.IdProducto;
                    detalle.IdVenta = venta.IdVenta;
     
                    if(item.Compra == true)
                    {
                        var producto = await _contextDb.Productos
                            .Where(x => x.IdProductos == item.IdProducto).FirstOrDefaultAsync();
                        producto.Stock = producto.Stock - item.Cantidad;
                        _contextDb.Productos.Update(producto);
                        await _contextDb.SaveChangesAsync();  
                    }
                    await _contextDb.DetalleVenta.AddAsync(detalle);
                    await _contextDb.SaveChangesAsync();
                }
                await _contextDb.Database.ExecuteSqlInterpolatedAsync(@$"EXEC sp_limpiarCarrito @idUsuario = {IdUsuario}");

                await transaction.CommitAsync();
                return _mapper.Map<VentasDTO>(model);
            }
            catch
            {
                await transaction.RollbackAsync();
                return null;
            }
        }

        public async Task<bool> AnularVenta(int id)
        {
            try
            {
                var venta = await _contextDb.Venta.Where(x => x.IdVenta == id && x.Estado == true)
                    .FirstOrDefaultAsync();
                venta.Estado = false;
                _contextDb.Venta.Update(venta);
                await _contextDb.SaveChangesAsync();

                foreach(var item in venta.DetalleVenta)
                {
                    var producto = await _contextDb.Productos
                        .Where(x => x.IdProductos == item.IdProducto && x.Estado == true).FirstOrDefaultAsync();
                    if(producto.IdCategoriaNavigation.IdCategoria == 1)
                    {
                        producto.Stock = producto.Stock + item.Cantidad;
                        _contextDb.Productos.Update(producto);
                        await _contextDb.SaveChangesAsync();
                    }                    
                }
                return true;
            } 
            catch 
            {
                return false;
            }
        }

    }
}