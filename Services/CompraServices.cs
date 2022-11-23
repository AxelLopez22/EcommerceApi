using AutoMapper;
using DTOs;
using ecommerceApi.Context;
using ecommerceApi.Models;
using Microsoft.EntityFrameworkCore;

namespace Services
{
    public class CompraServices
    {
        private readonly RepositoryContext _context;
        private readonly IMapper _mapper;

        public CompraServices(RepositoryContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }   

        public async Task<ComprasDTO> CrearCompra(string IdUsuario, ComprasDTO model)
        {
            var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                Compra compra = new Compra();
                compra.IdProveedor = model.IdProveedor;
                compra.UsuarioId = IdUsuario;
                compra.Estado = true;
                compra.FechaCompra = DateTime.Now;
                compra.Total = model.Detalle.Sum(x => x.Precio * x.Cantidad);
                await _context.Compras.AddAsync(compra);
                await _context.SaveChangesAsync();

                foreach(var item in model.Detalle)
                {
                    var producto = await _context.Productos.Where(x => x.IdProductos == item.IdProducto)
                        .FirstOrDefaultAsync();
                    producto.Stock = producto.Stock + item.Cantidad;
                    _context.Productos.Update(producto);
                    await _context.SaveChangesAsync();

                    DetalleCompra detalle = new DetalleCompra();
                    detalle.Cantidad = item.Cantidad;
                    detalle.Precio = item.Precio;
                    detalle.IdProducto = item.IdProducto;
                    detalle.IdCompra = compra.IdCompra;
                    await _context.DetalleCompras.AddAsync(detalle);
                    await _context.SaveChangesAsync();
                }
                await transaction.CommitAsync();
                return _mapper.Map<ComprasDTO>(model);
            }
            catch
            {
                await transaction.RollbackAsync();
                return null;
            }
        }

        public async Task<bool> AnularCompra(int id)
        {
            try
            {
                var Compra = await _context.Compras.Where(x => x.IdCompra == id && x.Estado == true)
                    .FirstOrDefaultAsync();

                if(Compra == null){return false;}

                Compra.Estado = false;
                _context.Compras.Update(Compra);
                await _context.SaveChangesAsync();
                foreach(var item in Compra.DetalleCompras)
                {
                    var producto = await _context.Productos
                        .Where(x => x.IdProductos == item.IdProductoNavigation.IdProductos).FirstOrDefaultAsync();
                    producto.Stock = producto.Stock - item.Cantidad;
                    _context.Productos.Update(producto);
                    await _context.SaveChangesAsync();
                }
                return true;
            } catch
            {
                return false;
            }
        }  
    }
}