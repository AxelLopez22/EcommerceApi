using AutoMapper;
using DTOs;
using ecommerceApi.Context;
using ecommerceApi.Models;
using Microsoft.EntityFrameworkCore;

namespace Services
{
    public class VentaServices
    {
        private readonly RepositoryContext _context;
        private readonly IMapper _mapper;

        public VentaServices(RepositoryContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<VentasDTO> CrearVenta(VentasDTO model)
        {
            var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                Ventum venta = new Ventum();
                //venta.IdUsuario = model.IdUsuario;
                venta.FechaVenta = DateTime.Now;
                venta.Estado = true;
                venta.Total = model.Detalle.Sum(x => x.Precio * x.Cantidad);
                await _context.Venta.AddAsync(venta);
                await _context.SaveChangesAsync();

                foreach(var item in model.Detalle)
                {
                    DetalleVentum detalle = new DetalleVentum();
                    detalle.Cantidad = item.Cantidad;
                    detalle.Precio = item.Precio;
                    detalle.IdProducto = item.IdProducto;
                    detalle.IdVenta = venta.IdVenta;
     
                    if(item.Compra == true)
                    {
                        var producto = await _context.Productos
                            .Where(x => x.IdProductos == item.IdProducto).FirstOrDefaultAsync();
                        producto.Stock = producto.Stock - item.Cantidad;
                        _context.Productos.Update(producto);
                        await _context.SaveChangesAsync();  
                    }
                    await _context.DetalleVenta.AddAsync(detalle);
                    await _context.SaveChangesAsync();
                }
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
                var venta = await _context.Venta.Where(x => x.IdVenta == id && x.Estado == true)
                    .FirstOrDefaultAsync();
                venta.Estado = false;
                _context.Venta.Update(venta);
                await _context.SaveChangesAsync();

                foreach(var item in venta.DetalleVenta)
                {
                    var producto = await _context.Productos
                        .Where(x => x.IdProductos == item.IdProducto && x.Estado == true).FirstOrDefaultAsync();
                    if(producto.IdCategoriaNavigation.IdCategoria == 1)
                    {
                        producto.Stock = producto.Stock + item.Cantidad;
                        _context.Productos.Update(producto);
                        await _context.SaveChangesAsync();
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